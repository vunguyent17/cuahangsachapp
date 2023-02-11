using CuaHangSach.Model;
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
    public partial class MH_DangNhap : ContentPage
    {
        public MH_DangNhap()
        {
            InitializeComponent();
        }
        private async void btnDangNhap_Clicked(object sender, EventArgs e)
        {
            btnDangNhap.IsEnabled = false;
            btnDangNhap.Text = "Đang đăng nhập ...";
            NguoiDung nd;
            HttpClient http = new HttpClient();

            string tenDN = txtTenDN.Text;
            string matkhau = txtMatKhau.Text;

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/DangNhap?TenDangNhap={tenDN}&MatKhau={matkhau}");
                nd = JsonConvert.DeserializeObject<NguoiDung>(kq);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            if (nd.MaNguoiDung > 0)
            {
                NguoiDung.ttNguoiDung = nd;
                await DisplayAlert("Đăng nhập thành công", "Chào bạn " + nd.HoTen, "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Thông báo", "Đăng nhập thất bại", "OK");
            }
        }

        private void btnDangKy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_DangKy());
        }
    }
}