using CuaHangSach.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach.GioHang
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_DatThanhCong : ContentPage
    {
        public MH_DatThanhCong(DonHang ttDonHang)
        {
            InitializeComponent();
            GuiMailThongTinDonHang(ttDonHang);
        }

        public async void GuiMailThongTinDonHang(DonHang ttDonHang)
        {
            int kqint = 0;
            try
            {
                HttpClient client = new HttpClient();
                HttpContent postContent = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/GuiEmail?TenDangNhap={NguoiDung.ttNguoiDung.TenDangNhap}&MatKhau={NguoiDung.ttNguoiDung.MatKhau}&MaDonHang={ttDonHang.MaDonHang}", postContent);
                var kqtv = await response.Content.ReadAsStringAsync();
                kqint = int.Parse(kqtv);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}