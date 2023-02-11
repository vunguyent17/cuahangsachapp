using CuaHangSach.DanhMucSach;
using CuaHangSach.Model;
using MimeKit;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static CuaHangSach.GioHang.MH_GioHang;



namespace CuaHangSach.GioHang
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_ThanhToan : ContentPage
    {
        string ptThanhToan = "TheNganHang";
        List<string> listMaSachDatHang;
        List<SachGioHang> listSachSoLuong;
        DonHang ttDonHang;
        long thanhTien = 0;
        
        public MH_ThanhToan(List<string> _listMaSachDatHang)
        {
            InitializeComponent();
            listMaSachDatHang = _listMaSachDatHang;
            LayDSSachDatHang(listMaSachDatHang);
            
        }

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <param name="listMaSachDatHang"></param>
        public async void LayDSSachDatHang(List<string> listMaSachDatHang)
        {
            HttpClient http = new HttpClient();
            try
            {
                string masachs = String.Join(",", listMaSachDatHang);
                string kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/LayThongTinNhieuSachGioHang?MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung}&MaSach={masachs}");
                listSachSoLuong = JsonConvert.DeserializeObject<List<SachGioHang>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            int soLuongDatHang = 0;
            foreach (var sach in listSachSoLuong)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
                thanhTien += sach.DonGia * sach.SoLuong;
                soLuongDatHang += 1;
            }
            
            lstSachView.ItemsSource = listSachSoLuong;
            lblThanhTien.Text = String.Format("{0:### ### ###} đ", thanhTien);
            lblTongCong.Text = $"Tổng cộng ({soLuongDatHang})";
        }

        private void btnDatHang_Clicked(object sender, EventArgs e)
        {
            ThemDonHang();  
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string giatri = ((RadioButton)sender).Value.ToString();
            ptThanhToan = giatri;
            bool mode = (giatri == "TheNganHang");
            setModeEntry(txtCardNumber, mode);
            setModeEntry(txtExpiry, mode);
            setModeEntry(txtCvv, mode);
            setModeLabel(lblCvv, mode);
            setModeLabel(lblExpiry, mode);
            setModeLabel(lblCardNumber, mode);
        }

        void setModeEntry(Entry entry, bool mode)
        {
            entry.IsEnabled = mode;
            entry.IsVisible = mode;
        }

        void setModeLabel(Label label, bool mode)
        {
            label.IsVisible = mode;
        }

        async void ThemDonHang()
        {
            btnDatHang.Text = "Đang xử lý ...";
            btnDatHang.IsEnabled = false;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("s");
            DonHang dh = new DonHang()
            {
                MaDonHang = 0,
                MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung,
                Tinh = txtTinh.Text,
                TPQuan = txtTPQuan.Text,
                DiaChiNha = txtDiaChiNha.Text,
                SoDienThoai = txtSoDienThoai.Text,
                ThanhTien = thanhTien,
                PTThanhToan = ptThanhToan,
                DaThanhToan = 0,
                NgayDat = sqlFormattedDate

            };
            HttpClient http = new HttpClient();

            string jsondh = JsonConvert.SerializeObject(dh);
            StringContent httpcontent = new StringContent(jsondh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/ThemDonHang", httpcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();;
            dh = JsonConvert.DeserializeObject<DonHang>(kqtv);
            Debug.WriteLine(dh.MaDonHang);

            if (dh.MaDonHang > 0)
            {
                ttDonHang = dh;
                await DisplayAlert("Thông báo", "Hệ thống đã tiếp nhận đơn hàng.\nMã đơn hàng: " + dh.MaDonHang, "OK");
                await ThemCTDonHang();
                if (ptThanhToan == "TheNganHang")
                {
                    await ThanhToanBangThe();
                }
                await Navigation.PushAsync(new MH_DatThanhCong(ttDonHang));
            }   
            else
                await DisplayAlert("Thông báo", "Lỗi tiếp nhận đơn hàng", "OK");
        }

        async Task ThemCTDonHang()
        {
            foreach (var sach in listSachSoLuong)
            {
                int kqint;
                try 
                {
                    
                    CTDonHang ctdh = new CTDonHang()
                    {
                        MaDonHang = ttDonHang.MaDonHang,
                        MaSach = sach.MaSach,
                        SoLuong = sach.SoLuong

                    };
                    HttpClient http = new HttpClient();
                    string jsonctdh = JsonConvert.SerializeObject(ctdh);
                    StringContent httpcontent = new StringContent(jsonctdh, Encoding.UTF8, "application/json");
                    HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/ThemCTDonHang", httpcontent);
                    var kqtv = await kq.Content.ReadAsStringAsync();
                    kqint = int.Parse(kqtv);
                }
                catch (Exception)
                {

                    throw;
                }
                
                if (kqint > 0)
                {
                  Debug.WriteLine("Thành công thêm sách và chi tiết đơn hàng");
                }
                else
                    Debug.WriteLine("Lỗi thêm sách và chi tiết đơn hàng");
            }
            await XoaSachGioHang(listMaSachDatHang);
        }

        async Task XoaSachGioHang(List<string> listMaSachDatHang)
        {
            int kqint = 0;
            try
            {
                HttpClient client = new HttpClient();
                HttpContent postContent = new StringContent("", Encoding.UTF8, "application/json");
                string masachs = String.Join(",", listMaSachDatHang);
                var response = await client.PostAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/XoaNhieuSachGioHang?MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung}&MaSach={masachs}", postContent);
                var kqtv = await response.Content.ReadAsStringAsync();
                kqint = int.Parse(kqtv);
            }
            catch (Exception)
            {
                throw;
            }
            if (kqint > 0)
            {
                Debug.WriteLine("Thông báo: Đã cập nhật lại giỏ hàng");
                App.ThayDoiGioHang = true;
            }  
            else
                Debug.WriteLine("Thông báo: Cập nhật giỏ hàng thất bại");
        }

        /// <summary>
        /// Thanh toan bang the
        /// </summary>
        private async Task ThanhToanBangThe()
        {
            var token = await TaoTokenThanhToan(new CardModel
            {
                Number = txtCardNumber.Text.Replace(" ", string.Empty),
                ExpMonth = Convert.ToInt16(txtExpiry.Text.Substring(0, 2)),
                ExpYear = Convert.ToInt16(txtExpiry.Text.Substring(3, 2)),
                Cvc = txtCvv.Text,
                Name = NguoiDung.ttNguoiDung.HoTen,
                AddressCity = txtTinh.Text,
                AddressZip = "700000",
                AddressCountry = "Viet Nam",
                AddressLine1 = txtDiaChiNha.Text
            });
            var success = await PayWithCard(new PaymentModel
            {
                Amount = Convert.ToInt32(thanhTien),
                Token = token,
                Description = $"Cua hang sach Stripe tu MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung} cho don hang MaDonHang={ttDonHang.MaDonHang}"
            });
            if (success)
            {
                await CapNhatDaThanhToanDonHang();
                await DisplayAlert("Thông báo", "Đã thanh toán qua thẻ thành công", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Thanh toán không thành công", "OK");
            }

        }

        async Task CapNhatDaThanhToanDonHang()
        {
            int kqint = 0;
            try
            {
                HttpClient client = new HttpClient();
                HttpContent postContent = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/CapNhatDaThanhToanDonHang?MaDonHang={ttDonHang.MaDonHang}&DaThanhToan=1", postContent);
                var kqtv = await response.Content.ReadAsStringAsync();
                kqint = int.Parse(kqtv);
            }
            catch (Exception)
            {
                throw;
            }
            if (kqint > 0)
                Debug.WriteLine("Đã cập nhật lại đơn hàng");
            else
                Debug.WriteLine("Cập nhật đơn hàng thất bại");
        }


        public async Task<bool> PayWithCard(PaymentModel paymentModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = JsonConvert.SerializeObject(paymentModel);
                HttpContent postContent = new StringContent(content, Encoding.UTF8, "application/json");


                var response = await client.PostAsync("https://cuahangsach.azurewebsites.net/api/CHSachController/ThanhToanThe", postContent);

                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> TaoTokenThanhToan(CardModel cardModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = JsonConvert.SerializeObject(cardModel);
                HttpContent postContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/TaoTokenThanhToan", postContent);
                var token = await response.Content.ReadAsStringAsync();
                if (token == null)
                {
                    return "";
                }
                else
                {
                    return token;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}