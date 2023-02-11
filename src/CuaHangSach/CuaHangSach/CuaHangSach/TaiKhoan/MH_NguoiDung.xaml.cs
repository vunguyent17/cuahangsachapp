using CuaHangSach.DanhMucSach;
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
using static System.Net.Mime.MediaTypeNames;

namespace CuaHangSach.TaiKhoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_NguoiDung : ContentPage
    {
        Button btnDangNhap = new Button()
        {
            Text = "Đăng nhập",
            FontSize = 16,
            BorderWidth = 0.5,
        };
        
        public MH_NguoiDung()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            bool daDangNhap = NguoiDung.ttNguoiDung.MaNguoiDung != 0;
            btnDangXuat.IsEnabled = btnDangXuat.IsVisible = daDangNhap;
            lstDHView.IsEnabled = daDangNhap;
            btnNguoiDungSettings.IsEnabled = btnNguoiDungSettings.IsVisible = daDangNhap;
            if (daDangNhap)
            {
                lblTenNguoiDung.Text = $"{NguoiDung.ttNguoiDung.HoTen}";
                btnDangNhap.IsVisible = btnDangNhap.IsEnabled = false;
                LayLichSuDonHangAPI();
            }
            else
            {
                lblTenNguoiDung.Text = "Khách";
                if (!gridTTNguoiDung.Children.Contains(btnDangNhap))
                {
                    btnDangNhap.Clicked += BtnDangNhap_Clicked;
                    Grid.SetColumn(btnDangNhap, 0);
                    Grid.SetRow(btnDangNhap, 1);
                    Grid.SetColumnSpan(btnDangNhap, 2);
                    gridTTNguoiDung.Children.Add(btnDangNhap);
                }
                btnDangNhap.IsEnabled = btnDangNhap.IsVisible = true;
                lstDHView.ItemsSource = new List<DonHangView>();
            }
        }

        private void BtnDangNhap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_DangNhap());
        }

        public async void LayLichSuDonHangAPI()
        {
            List<DonHang> listDonHang;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/LayLichSuDonHang?MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung}");
                listDonHang = JsonConvert.DeserializeObject<List<DonHang>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            List<DonHangView> listDonHangView = new List<DonHangView>();
            foreach (DonHang dh in listDonHang)
            {
                string daThanhToan = (dh.DaThanhToan == 0) ? "" : "OK";
                DonHangView donHangView = new DonHangView() { DataDonHang = dh , DaThanhToanView = daThanhToan };
                listDonHangView.Add(donHangView);
            }
            lstDHView.ItemsSource = listDonHangView;
        }

        private void btnDangXuat_Clicked(object sender, EventArgs e)
        {
            NguoiDung.ttNguoiDung = new NguoiDung() { MaNguoiDung = 0 };
            Navigation.PushAsync(new MH_DangNhap());
        }

        private void btnDangKy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_DangKy());
        }

        private void lstDHView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDHView.SelectedItem == null)
                return;
            DonHangView donHangView;

            if (e.CurrentSelection != null)
            {
                donHangView =(DonHangView)e.CurrentSelection.FirstOrDefault();
                
                MH_ChiTietDonHang mh = new MH_ChiTietDonHang(donHangView.DataDonHang.MaDonHang);
                Navigation.PushAsync(mh);
                lstDHView.SelectedItem = null;
            }
            
        }

        private void btnNguoiDungSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_ChinhSuaTaiKhoan());
        }
    }
}