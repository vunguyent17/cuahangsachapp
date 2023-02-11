using CuaHangSach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach.TaiKhoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_ChinhSuaTaiKhoan : ContentPage
    {
        public MH_ChinhSuaTaiKhoan()
        {
            InitializeComponent();
            txtTenNguoiDung.Text = NguoiDung.ttNguoiDung.HoTen;
            txtEmail.Text = NguoiDung.ttNguoiDung.Email;
        }

        private async void btnLuu_Clicked(object sender, EventArgs e)
        {
            btnLuu.Text = "Đang lưu ...";
            btnLuu.IsEnabled = false;
            NguoiDung nd = new NguoiDung()
            {
                MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung,
                TenDangNhap= NguoiDung.ttNguoiDung.TenDangNhap,
                HoTen= txtTenNguoiDung.Text,
                Email=txtEmail.Text,
                MatKhau=txtMatKhau.Text
            };
            HttpClient http = new HttpClient();

            string jsonnd = JsonConvert.SerializeObject(nd);
            Debug.WriteLine(jsonnd);
            StringContent httpcontent = new StringContent(jsonnd, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/CapNhatThongTinNguoiDung", httpcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            int kqint = int.Parse(kqtv);
            if (kqint == 1)
            {
                NguoiDung.ttNguoiDung = nd;
                await DisplayAlert("Thông báo", "Cập nhật thông tin người dùng thành công", "OK");
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Thông báo", "Không thể cập nhật thông tin người dùng", "OK");
        }

        private void btnDenTrangDoiMatKhau_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_ChinhSuaMatKhau());
        }
    }
}