using CHSachWebAppData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Configuration;
using System.Web;
using Stripe;
using System.Text;
using System.Threading.Tasks;

namespace CHSachWebAppData.Controllers
{
    public class CHSachController : ApiController
    {
        [Route("api/CHSachController/LayDSLoaiSach")]
        [HttpGet]
        public IHttpActionResult LayDSLoaiSach()
        {
            DataTable dataLoaiSach;
            dataLoaiSach = Database.LayDSLoaiSach();
            if (dataLoaiSach != null && dataLoaiSach.Rows.Count > 0)
            {
                return Ok(dataLoaiSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LayDSSach")]
        [HttpGet]
        public IHttpActionResult LayDSSach()
        {
            DataTable dataSach;
            dataSach = Database.LayDSSach();
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LayDSSachTheoLoai")]
        [HttpGet]
        public IHttpActionResult LayDSSachTheoLoai(int MaLoai)
        {
            DataTable dataSach;
            dataSach = Database.LayDSSachTheoLoai(MaLoai);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LaySach")]
        [HttpGet]
        public IHttpActionResult LaySach(string MaSach)
        {
            DataTable dataSach;
            dataSach = Database.LaySach(MaSach);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LayLoaiSach")]
        [HttpGet]
        public IHttpActionResult LayLoaiSach(int MaLoai)
        {
            DataTable dataSach;
            dataSach = Database.LayLoaiSach(MaLoai);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LaySachMoi")]
        [HttpGet]
        public IHttpActionResult LaySachMoi()
        {
            DataTable dataSach;
            dataSach = Database.LaySachMoi();
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LaySachGiaUuDai")]
        [HttpGet]
        public IHttpActionResult LaySachGiaUuDai()
        {
            DataTable dataSach;
            dataSach = Database.LaySachGiaUuDai();
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LaySachDeXuat")]
        [HttpGet]
        public IHttpActionResult LaySachDeXuat(int MaNguoiDung)
        {
            DataTable dataSach;
            dataSach = Database.LaySachDeXuat(MaNguoiDung);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/TimKiemSach")]
        [HttpGet]
        public IHttpActionResult TimKiemSach(string TuTimKiem)
        {
            DataTable dataSach;
            dataSach = Database.TimKiemSach(TuTimKiem);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/DangNhap")]
        [HttpGet]
        public IHttpActionResult DangNhap(string TenDangNhap, string MatKhau)
        {
            NguoiDung kq;
            kq = Database.DangNhap(TenDangNhap, MatKhau);
            if (kq != null)
            {
                return Ok(kq);
            }
            return NotFound();
        }

        [Route("api/CHSachController/DangKy")]
        [HttpPost]
        public IHttpActionResult DangKy(NguoiDung nd)
        {
            try
            {
                NguoiDung kq = Database.ThemNguoiDung(nd);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/CapNhatThongTinNguoiDung")]
        [HttpPost]
        public IHttpActionResult CapNhatThongTinNguoiDung(NguoiDung nd)
        {
            try
            {
                int kq = Database.CapNhatThongTinNguoiDung(nd);
                return Ok(kq);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("api/CHSachController/CapNhatMatKhauNguoiDung")]
        [HttpPost]
        public IHttpActionResult CapNhatMatKhauNguoiDung(NguoiDung ndmoi, string matKhauCu)
        {
            try
            {
                int kq = Database.CapNhatMatKhauNguoiDung(ndmoi, matKhauCu);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        [Route("api/CHSachController/ThemSachVaoGioHang")]
        [HttpPost]
        public IHttpActionResult ThemSachVaoGioHang(SachGioHang sgh)
        {
            try
            {
                int kq = Database.ThemSachVaoGioHang(sgh);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/XoaSachGioHang")]
        [HttpPost]
        public IHttpActionResult XoaSachGioHang(int MaNguoiDung, string MaSach)
        {
            try
            {
                int kq = Database.XoaSachGioHang(MaNguoiDung, MaSach);
                return Ok(kq);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("api/CHSachController/XoaNhieuSachGioHang")]
        [HttpPost]
        public IHttpActionResult XoaNhieuSachGioHang(int MaNguoiDung, string MaSach)
        {
            try
            {
                int kq = Database.XoaNhieuSachGioHang(MaNguoiDung, MaSach);
                return Ok(kq);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("api/CHSachController/ThemDonHang")]
        [HttpPost]
        public IHttpActionResult ThemDonHang(DonHang dh)
        {
            try
            {
                DonHang kq = Database.ThemDonHang(dh);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/ThemCTDonHang")]
        [HttpPost]
        public IHttpActionResult ThemCTDonHang(CTDonHang ctdh)
        {
            try
            {
                int kq = Database.ThemCTDonHang(ctdh);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayChiTietDonHang")]
        [HttpGet]
        public IHttpActionResult LayChiTietDonHang(int MaDonHang)
        {
            DataTable dataSach;
            dataSach = Database.LayChiTietDonHang(MaDonHang);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LayDonHang")]
        [HttpGet]
        public IHttpActionResult LayDonHang(int MaDonHang)
        {
            DataTable dataSach;
            dataSach = Database.LayDonHang(MaDonHang);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/LayLichSuDonHang")]
        [HttpGet]
        public IHttpActionResult LayLichSuDonHang(int MaNguoiDung)
        {
            DataTable dataSach;
            dataSach = Database.LayLichSuDonHang(MaNguoiDung);
            if (dataSach != null && dataSach.Rows.Count > 0)
            {
                return Ok(dataSach);
            }
            return NotFound();
        }

        [Route("api/CHSachController/CapNhatDaThanhToanDonHang")]
        [HttpPost]
        public IHttpActionResult CapNhatDaThanhToanDonHang(int MaDonHang, int DaThanhToan)
        {
            try
            {
                int kq = Database.CapNhatDaThanhToanDonHang(MaDonHang, DaThanhToan);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayNhieuSach")]
        [HttpGet]
        public IHttpActionResult LayNhieuSach(string DSMaSach)
        {
            try
            {
                DataTable kq = Database.LayNhieuSach(DSMaSach);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayGioHang")]
        [HttpGet]
        public IHttpActionResult LayGioHang(int MaNguoiDung)
        {
            try
            {
                DataTable kq = Database.LayGioHang(MaNguoiDung);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayGioHangChiTiet")]
        [HttpGet]
        public IHttpActionResult LayGioHangChiTiet(int MaNguoiDung)
        {
            try
            {
                DataTable kq = Database.LayGioHangChiTiet(MaNguoiDung);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayThongTinSachGioHang")]
        [HttpGet]
        public IHttpActionResult LayThongTinSachGioHang(int MaNguoiDung, string MaSach)
        {
            try
            {
                DataTable kq = Database.LayThongTinSachGioHang(MaNguoiDung, MaSach);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/LayThongTinNhieuSachGioHang")]
        [HttpGet]
        public IHttpActionResult LayThongTinNhieuSachGioHang(int MaNguoiDung, string MaSach)
        {
            try
            {
                DataTable kq = Database.LayThongTinNhieuSachGioHang(MaNguoiDung, MaSach);
                return Ok(kq);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [Route("api/CHSachController/GuiEmail")]
        [HttpPost]
        public IHttpActionResult GuiEmail(string TenDangNhap, string MatKhau, int MaDonHang)
        {
            var settingsConf = ConfigurationManager.AppSettings;
            var client = new SmtpClient(settingsConf["SmtpClientHost"], int.Parse(settingsConf["SmtpClientPort"]))
            {
                Credentials = new NetworkCredential(settingsConf["usernameEmailService"], settingsConf["passwordEmailService"]),
                EnableSsl = true
            };

            NguoiDung ttNguoiDung = Database.DangNhap(TenDangNhap, MatKhau);

            DataTable dataDonHang = Database.LayDonHang(MaDonHang);
            string strDonHang = JsonConvert.SerializeObject(dataDonHang);
            DonHang ttDonHang = JsonConvert.DeserializeObject<List<DonHang>>(strDonHang)[0];

            DataTable dataSachDonHang = Database.LayChiTietDonHang(MaDonHang);
            string strSachDonHang = JsonConvert.SerializeObject(dataSachDonHang);
            List<CTDonHangSL> listCTDonHang = JsonConvert.DeserializeObject<List<CTDonHangSL>>(strSachDonHang);


            String from = "from@example.com";
            String to = ttNguoiDung.Email;
            String subject = "Xác nhận đơn hàng từ Cửa Hàng Sách";
            int TongSL = 0;

            String danhSachSachhtml = "";
            foreach (var ctdh in listCTDonHang)
            {
                TongSL += ctdh.SoLuong;
                danhSachSachhtml += $@"
<tr>
       <td style=""text-align: center;""><img src=""{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/images/{ctdh.Hinh}"" width=""60px"" style=""float: center;; border-radius: 10px; ""/>
      </td>
      <td>
        <p><strong>{ctdh.TenSach}</strong> ({ctdh.MaSach})</p>
        <p><em>{ctdh.TacGia}</em></p>
      </td>
      <td style=""text-align: right;"">{ctdh.DonGia}</td>
      <td style=""text-align: right;"">
          {ctdh.SoLuong}
      </td>
        <td style=""text-align: right;"">
          {ctdh.SoLuong * ctdh.DonGia}
      </td>
</tr>
";
            }
            String messageBody = $@"
<!doctype html>
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
  <head>
    <style>
    body{{
      font-family: arial, sans-serif;
    }}
    table {{
      border-collapse: collapse;
      width: 100%;
    }}
    
    h2{{
      color: purple
    }}

    td, th {{
      border: 1px solid #dddddd;
      text-align: left;
      padding: 8px;
    }}
    
    th {{
      background-color: ghostwhite;
    }}
    </style>
  </head>
<body>
<h1 style=""background-color: purple; color: white; padding: 10px; font-weight: normal;"">Xác nhận đơn hàng tại Cửa Hàng Sách</h2>
<h2>Thông tin đơn hàng</h2>
<p>Cửa hàng Sách xác nhận đã nhận đơn hàng mã số <strong>{ttDonHang.MaDonHang}</strong> của tài khoản <strong>{ttNguoiDung.TenDangNhap}</strong></p>
    <table>
      <tr>
        <th>Mã đơn hàng</th>
        <td>{ttDonHang.MaDonHang}</td>
      </tr>
      <tr>
        <th>Địa chỉ</th>
        <td>{ttDonHang.DiaChiNha}, {ttDonHang.TPQuan}, {ttDonHang.Tinh}</td>
      </tr>
      <tr>
        <th>Số điện thoại</th>
        <td>{ttDonHang.SoDienThoai}</td>
      </tr>
      <tr>
        <th>Thành tiền</th>
        <td>{ttDonHang.ThanhTien} đồng</td>
      </tr>
      <tr>
        <th>Phương thức thanh toán</th>
        <td> {(ttDonHang.PTThanhToan == "TienMat" ? "Tiền mặt" : "Thẻ ngân hàng")}</td>
      </tr>
      <tr>
        <th>Trạng thái thanh toán</th>
        <td>{(ttDonHang.DaThanhToan == 0 ? "Chưa thanh toán" : "Đã thanh toán")}</td>
      </tr>
    </table>
<br> 
  <h2>Chi tiết đơn hàng</h2>
  <table>
    <tr>
      <th></th>
      <th>Thông tin sách</th>
      <th style=""text-align: right;"">Đơn giá (đồng)</th>
      <th style=""text-align: right;"">Số lượng</th>
      <th style=""text-align: right;"">Thành tiền (đồng)</th>
    </tr>
{danhSachSachhtml}
    <tr>
      <td colspan=""3"" style=""text-align: right; font-weight: bold;"">Tổng cộng</td>
      <td style=""text-align: right; font-weight: bold;"">{TongSL}</td>
      <td style=""text-align: right; font-weight: bold; color: purple;"""">{ttDonHang.ThanhTien}</td>
    </tr>
 </table>
  <p style=""color: purple; text-align: center;"">Cám ơn bạn đã mua sách tại <span style=""color: purple; font-weight: bold;"">Cửa Hàng sách</span>. Hẹn gặp bạn lần sau</p>
</body>
</html>";
            MailMessage message = new MailMessage(from, to, subject, messageBody);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
              $"{messageBody}",
              null,
              "text/html"
            );
            message.AlternateViews.Add(htmlView);

            try
            {
                client.Send(message);
                return Ok(1);
            }
            catch (SmtpException ex)
            {
                Debug.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [Route("api/CHSachController/ThanhToanThe")]
        [HttpPost]
        public IHttpActionResult PayWithCard([FromBody] PaymentModel paymentModel)
        {
            var success = PaymentService.PayWithCard(paymentModel);
            return Ok(success);
        }



        [Route("api/CHSachController/TaoTokenThanhToan")]
        [HttpPost]
        public IHttpActionResult TaoTokenThanhToan(CardModel cardModel)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["SecretKey"];
                var option = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = cardModel.Number,
                        ExpMonth = cardModel.ExpMonth.ToString(),
                        ExpYear = cardModel.ExpYear.ToString(),
                        Cvc = cardModel.Cvc,
                        Currency = "VND",
                        Name = cardModel.Name,
                        AddressCity = cardModel.AddressCity,
                        AddressZip = cardModel.AddressZip,
                        AddressLine1 = cardModel.AddressLine1,
                        AddressCountry = cardModel.AddressCountry
                    }
                };

                var service = new TokenService();
                var token = service.Create(option);
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(token.Id, System.Text.Encoding.UTF8, "text/plain")
                };
                return ResponseMessage(httpResponseMessage);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }

}
