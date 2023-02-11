using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using CHSachWebAppData.Models;
using System.Text.RegularExpressions;
using Stripe;
using System.Web.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace CHSachWebAppData.Controllers
{
    public class Database
    {
        public static DataTable Read_Table(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            DataTable result = new DataTable("table1");
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(result);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return result;
        }
        public static object Exec_Command(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                cmd.Parameters.Add("@CurrentID", SqlDbType.Int).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@CurrentID"].Value;
                    // Attempt to commit the transaction.

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    result = null;
                }

            }
            return result;
        }

        public static DataTable LayDSLoaiSach()
        {
            return Read_Table("LayDSLoaiSach");
        }

        public static DataTable LayDSSach()
        {
            return Read_Table("LayDSSach");
        }

        public static DataTable LayDSSachTheoLoai(int MaLoai)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaLoai", MaLoai);
            DataTable result = Read_Table("LayDSSachTheoLoai", param);
            return result;
        }

        public static DataTable LaySach(string MaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaSach", MaSach);
            DataTable result = Read_Table("LaySach", param);
            return result;
        }

        public static DataTable LayLoaiSach(int MaLoai)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaLoai", MaLoai);
            DataTable result = Read_Table("LayLoaiSach", param);
            return result;
        }

        public static DataTable LaySachMoi()
        {
            return Read_Table("LaySachMoi");
        }

        public static DataTable LaySachGiaUuDai()
        {
            return Read_Table("LaySachGiaUuDai");
        }

        public static DataTable LayNhieuSach(string dsMaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("DSMaSach", dsMaSach);
            return Read_Table("LaySachGiaUuDai", param);
        }

        public static DataTable TimKiemSach(string TuTimKiem)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TuTimKiem", TuTimKiem);
            DataTable result = Read_Table("TimKiemSach", param);
            return result;
        }

        public static NguoiDung ThemNguoiDung(NguoiDung nd)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("HoTen", nd.HoTen);
            param.Add("TenDangNhap", nd.TenDangNhap);
            param.Add("MatKhau", BC.HashPassword(nd.MatKhau));
            param.Add("Email", nd.Email);
            int kq = int.Parse(Exec_Command("ThemNguoiDung", param).ToString());
            if (kq > 0)
            {
                nd.MaNguoiDung = kq;
            }
            return nd;
        }
        public static NguoiDung DangNhap(string TenDangNhap, string MatKhau)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TenDangNhap", TenDangNhap);
            DataTable tb = Read_Table("LayNguoiDung", param);
            NguoiDung kq = new NguoiDung();

            if (tb.Rows.Count > 0)
            {
                var row = tb.Rows[0];
                if (BC.Verify(MatKhau, row["MatKhau"].ToString()))
                {
                    kq.MaNguoiDung = int.Parse(row["MaNguoiDung"].ToString());
                    kq.HoTen = row["HoTen"].ToString();
                    kq.TenDangNhap = row["TenDangNhap"].ToString();
                    kq.Email = row["Email"].ToString();
                    kq.MatKhau = MatKhau;
                    return kq;
                }
            }
            kq.MaNguoiDung = 0;
            return kq;
        }

        public static DataTable LayGioHang(int MaNguoiDung)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            DataTable result = Read_Table("LayGioHang", param);
            return result;
        }

        public static DataTable LayGioHangChiTiet(int MaNguoiDung)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            DataTable result = Read_Table("LayGioHangChiTiet", param);
            return result;
        }

        public static DataTable LayThongTinSachGioHang(int MaNguoiDung, string MaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            param.Add("MaSach", MaSach);
            DataTable result = Read_Table("LayThongTinSachGioHang", param);
            return result;
        }

        public static DataTable LayThongTinNhieuSachGioHang(int MaNguoiDung, string MaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            param.Add("MaSach", MaSach);
            DataTable result = Read_Table("LayThongTinNhieuSachGioHang", param);
            return result;
        }

        public static DataTable LaySachDeXuat(int MaNguoiDung)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            DataTable result = Read_Table("LaySachDeXuat", param);
            return result;
        }

        public static int ThemSachVaoGioHang(SachGioHang sgh)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", sgh.MaNguoiDung);
            param.Add("MaSach", sgh.MaSach);
            param.Add("SoLuong", sgh.SoLuong);
            int kq = int.Parse(Exec_Command("ThemSachVaoGioHang", param).ToString());
            return kq;
        }



        public static int XoaSachGioHang(int MaNguoiDung, string MaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            param.Add("MaSach", MaSach);
            int kq = int.Parse(Exec_Command("XoaSachGioHang", param).ToString());
            return kq;
        }

        public static int XoaNhieuSachGioHang(int MaNguoiDung, string MaSach)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            param.Add("MaSach", MaSach);
            int kq = int.Parse(Exec_Command("XoaNhieuSachGioHang", param).ToString());
            return kq;
        }

        public static DonHang ThemDonHang(DonHang dh)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", dh.MaNguoiDung);
            param.Add("Tinh", dh.Tinh);
            param.Add("TPQuan", dh.TPQuan);
            param.Add("DiaChiNha", dh.DiaChiNha);
            param.Add("SoDienThoai", dh.SoDienThoai);
            param.Add("ThanhTien", dh.ThanhTien);
            param.Add("PTThanhToan", dh.PTThanhToan);
            param.Add("DaThanhToan", dh.DaThanhToan);
            param.Add("NgayDat", dh.NgayDat);
            int kq = int.Parse(Exec_Command("ThemDonHang", param).ToString());
            if (kq > 0)
            {
                dh.MaDonHang = kq;
            }
            return dh;
        }
        public static int XoaDonHang(int MaDonHang)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaDonHang", MaDonHang);
            int kq = int.Parse(Exec_Command("XoaDonHang", param).ToString());
            return kq;
        }

        public static DataTable LayLichSuDonHang(int MaNguoiDung)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", MaNguoiDung);
            DataTable result = Read_Table("LayLichSuDonHang", param);
            return result;
        }

        public static DataTable LayChiTietDonHang(int MaDonHang)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaDonHang", MaDonHang);
            DataTable result = Read_Table("LayChiTietDonHang", param);
            return result;
        }

        public static DataTable LayDonHang(int MaDonHang)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaDonHang", MaDonHang);
            DataTable result = Read_Table("LayDonHang", param);
            return result;
        }
        public static int ThemCTDonHang(CTDonHang ctdh)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaDonHang", ctdh.MaDonHang);
            param.Add("MaSach", ctdh.MaSach);
            param.Add("SoLuong", ctdh.SoLuong);
            int kq = int.Parse(Exec_Command("ThemCTDonHang", param).ToString());
            return kq;
        }

        public static int CapNhatDaThanhToanDonHang(int MaDonHang, int DaThanhToan)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaDonHang", MaDonHang);
            param.Add("DaThanhToan", DaThanhToan);
            int kq = int.Parse(Exec_Command("CapNhatDaThanhToanDonHang", param).ToString());
            return kq;
        }

        public static int CapNhatThongTinNguoiDung(NguoiDung nd)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("TenDangNhap", nd.TenDangNhap);
            DataTable tb = Read_Table("LayNguoiDung", param);
            int kq = -1;
            if (tb.Rows.Count > 0)
            {
                var row = tb.Rows[0];
                param = new Dictionary<string, object>();
                if (BC.Verify(nd.MatKhau, row["MatKhau"].ToString()))
                {
                    param.Add("MaNguoiDung", nd.MaNguoiDung);
                    param.Add("HoTen", nd.HoTen);
                    param.Add("Email", nd.Email);
                    kq = int.Parse(Exec_Command("CapNhatThongTinNguoiDung", param).ToString());
                }
            }
            return kq;
        }

        public static int CapNhatMatKhauNguoiDung(NguoiDung ndmoi, string MatKhauCu)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TenDangNhap", ndmoi.TenDangNhap);
            DataTable tb = Read_Table("LayNguoiDung", param);
            int kq = -1;
            if (tb.Rows.Count > 0)
            {
                var row = tb.Rows[0];
                param = new Dictionary<string, object>();
                if (BC.Verify(MatKhauCu, row["MatKhau"].ToString()))
                {
                    param.Add("MaNguoiDung", ndmoi.MaNguoiDung);
                    param.Add("MatKhauMoi", BC.HashPassword(ndmoi.MatKhau));
                    kq = int.Parse(Exec_Command("CapNhatMatKhauNguoiDung", param).ToString());
                }
            }
                
            return kq;
        }
    }
}