using CuaHangSach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach.TaiKhoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_DangKy : ContentPage
    {
        public MH_DangKy()
        {
            InitializeComponent();
            if (NguoiDung.ttNguoiDung.MaNguoiDung > 0)
            {
                txtNguoiDung.Text = NguoiDung.ttNguoiDung.HoTen;
                txtEmail.Text = NguoiDung.ttNguoiDung.Email;
                txtTenDN.Text = NguoiDung.ttNguoiDung.TenDangNhap;
            }
        }

        private async void btnDangKy_Clicked(object sender, EventArgs e)
        {
            btnDangKy.Text = "Đang đăng ký ...";
            btnDangKy.IsEnabled = false;
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng", "OK");
                return;
            }
            NguoiDung nd = new NguoiDung
            {
                MaNguoiDung = 0,
                HoTen = txtNguoiDung.Text,
                TenDangNhap = txtTenDN.Text,
                Email = txtEmail.Text,
                MatKhau = txtMatKhau.Text
            };
            HttpClient http = new HttpClient();

            string jsonnd = JsonConvert.SerializeObject(nd);
            StringContent httpcontent = new StringContent(jsonnd, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/DangKy", httpcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            nd = JsonConvert.DeserializeObject<NguoiDung>(kqtv);
            if (nd.MaNguoiDung > 0)
            {
                await DisplayAlert("Thông báo", "Thêm người dùng thành công: \n" + nd.HoTen, "OK");
                NguoiDung.ttNguoiDung = nd;
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Thông báo", "Tên đăng nhập đã có", "OK");
        }
    }
}