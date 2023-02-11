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
    public partial class MH_ChinhSuaMatKhau : ContentPage
    {
        public MH_ChinhSuaMatKhau()
        {
            InitializeComponent();
        }

        private async void btnXacNhanDoiMatKhau_Clicked(object sender, EventArgs e)
        {
            btnXacNhanDoiMatKhau.Text = "Đang xử lý ...";
            btnXacNhanDoiMatKhau.IsEnabled = false;
            if (txtMatKhauMoi.Text != txtNhapLaiMatKhauMoi.Text)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng", "OK");
                return;
            }
            NguoiDung nd = new NguoiDung()
            {
                MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung,
                TenDangNhap = NguoiDung.ttNguoiDung.TenDangNhap,
                HoTen = NguoiDung.ttNguoiDung.HoTen,
                Email = NguoiDung.ttNguoiDung.Email,
                MatKhau = txtMatKhauMoi.Text
            };
            HttpClient http = new HttpClient();

            string jsonnd = JsonConvert.SerializeObject(nd);
            StringContent httpcontent = new StringContent(jsonnd, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/CapNhatMatKhauNguoiDung?matKhauCu={txtMatKhauCu.Text}", httpcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            int kqint = int.Parse(kqtv);
            if (kqint == 1)
            {
                NguoiDung.ttNguoiDung = nd;
                await DisplayAlert("Thông báo", "Thay đổi mật khẩu thành công", "OK");
                await Navigation.PopToRootAsync();
            }
            else
                await DisplayAlert("Thông báo", "Không thể thay đổi mật khẩu", "OK");
        }
    }
}