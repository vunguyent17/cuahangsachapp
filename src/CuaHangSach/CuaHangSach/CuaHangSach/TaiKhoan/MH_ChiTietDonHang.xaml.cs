using CuaHangSach.Model;
using CuaHangSach.ModelHienThi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach.TaiKhoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_ChiTietDonHang : ContentPage
    {
        public MH_ChiTietDonHang(int MaDonHang)
        {
            InitializeComponent();
            LayThongTinDonHang(MaDonHang);
            LayDSSachCTDonHang(MaDonHang);
        }

        public async void LayThongTinDonHang(int MaDonHang)
        {
            DonHang donHang;
            HttpClient http = new HttpClient();
            try
            {
                string kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/LayDonHang?MaDonHang={MaDonHang}");
                donHang = JsonConvert.DeserializeObject<List<DonHang>>(kq)[0];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            lblMaDonHang.Text = donHang.MaDonHang.ToString();
            lblDiaChi.Text = $"{donHang.DiaChiNha}, {donHang.TPQuan}, {donHang.Tinh}";
            lblSDT.Text = donHang.SoDienThoai;
            lblPTThanhToan.Text = (donHang.PTThanhToan == "TienMat") ? "Tiền mặt" : "Thẻ ngân hàng";
            lblThanhTien.Text = String.Format("{0:### ### ###} đ", donHang.ThanhTien);
            lblTrangThai.Text = donHang.DaThanhToan == 0 ? "Chưa thanh toán" : "Đã thanh toán";
            lblNgayDat.Text = donHang.NgayDat;
        }

        public async void LayDSSachCTDonHang(int MaDonHang)
        {
            List<CTDonHangView> listCTDonHangView;
            HttpClient http = new HttpClient();
            try
            {
                string kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/LayChiTietDonHang?MaDonHang={MaDonHang}");
                listCTDonHangView = JsonConvert.DeserializeObject<List<CTDonHangView>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach (var sach in listCTDonHangView)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }

            lstSachView.ItemsSource = listCTDonHangView;
        }
    }
}