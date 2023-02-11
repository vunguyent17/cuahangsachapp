using CuaHangSach.DanhMucSach;
using CuaHangSach.Model;
using CuaHangSach.TaiKhoan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_CuaHangSach : ContentPage
    {
        public MH_CuaHangSach()
        {
            InitializeComponent();
            LayDSSachMoiAPI();
            LayDSSachGiaUuDaiAPI();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SettingViews();
            CreateTitle(); 
        }
        void SettingViews()
        {
            bool daDangNhap = (NguoiDung.ttNguoiDung.MaNguoiDung != 0);
            frmDangNhap.IsVisible = frmDangNhap.IsEnabled = !daDangNhap;
            lblDeXuat.IsVisible = lstSachDeXuat.IsVisible = lstSachDeXuat.IsEnabled = daDangNhap;
            if (daDangNhap)
            {
                LayDSSachDeXuatAPI();
            }
            else
            {
                lstSachDeXuat.ItemsSource = new List<Sach>();
            }
            
        }
        public void CreateTitle()
        {
            TimeSpan interval = new TimeSpan();
            interval = DateTime.Now.TimeOfDay;
            int hour = interval.Hours;

            string tenNguoiDung = "";
            if (NguoiDung.ttNguoiDung.MaNguoiDung != 0)
            {
                tenNguoiDung = ", " + NguoiDung.ttNguoiDung.HoTen.Split(' ').LastOrDefault();
            }

            string hienthi;
            if (hour >= 4 && hour < 11)
                hienthi = "Chào buổi sáng";
            else if (hour >= 11 && hour < 14)
                hienthi = "Buổi trưa vui vẻ";
            else if (hour >= 14 && hour < 18)
                hienthi = "Chào buổi chiều";
            else if (hour >= 18 && hour < 23)
                hienthi = "Buổi tối vui vẻ";
            else
                hienthi = "Chúc ngủ ngon";

            Title = hienthi + tenNguoiDung;
        }
        public async void LayDSSachMoiAPI()
        {
            List<Sach> listSachMoi;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LaySachMoi");
                listSachMoi = JsonConvert.DeserializeObject<List<Sach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach (var sach in listSachMoi)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }

            lstSachMoi.ItemsSource = listSachMoi;
        }

        public async void LayDSSachGiaUuDaiAPI()
        {
            List<Sach> listSachUuDai;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LaySachGiaUuDai");
                listSachUuDai = JsonConvert.DeserializeObject<List<Sach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach (var sach in listSachUuDai)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }

            lstSachUuDai.ItemsSource = listSachUuDai;
        }

        public async void LayDSSachDeXuatAPI()
        {
            List<Sach> listSachDeXuat;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/LaySachDeXuat?MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung}");
                listSachDeXuat = JsonConvert.DeserializeObject<List<Sach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach (var sach in listSachDeXuat)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }

            lstSachDeXuat.ItemsSource = listSachDeXuat;
        }

        private void lstSachUuDai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSachUuDai.SelectedItem == null)
                return;

            if (e.CurrentSelection != null)
            {
                Sach sach = (Sach)e.CurrentSelection.FirstOrDefault();
                MH_ChiTietSach mh = new MH_ChiTietSach(sach);
                Navigation.PushAsync(mh);
                lstSachUuDai.SelectedItem = null;
            }
        }

        private void lstSachMoi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSachMoi.SelectedItem == null)
                return;

            if (e.CurrentSelection != null)
            {
                Sach sach = (Sach)e.CurrentSelection.FirstOrDefault();
                MH_ChiTietSach mh = new MH_ChiTietSach(sach);
                Navigation.PushAsync(mh);
                lstSachMoi.SelectedItem = null;
            }
        }

        private void lstSachDeXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSachDeXuat.SelectedItem == null)
                return;

            if (e.CurrentSelection != null)
            {
                Sach sach = (Sach)e.CurrentSelection.FirstOrDefault();
                MH_ChiTietSach mh = new MH_ChiTietSach(sach);
                Navigation.PushAsync(mh);
                lstSachDeXuat.SelectedItem = null;
            }
        }

        private void btnDangNhap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MH_DangNhap());
        }

        private async void btnCollection1_Clicked(object sender, EventArgs e)
        {
            List<Sach> listSach;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/TimKiemSach?TuTimKiem=lord%20of%20the%20ring");
                listSach = JsonConvert.DeserializeObject<List<Sach>>(kq);
                var kq2 = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/TimKiemSach?TuTimKiem=hobbit");
                listSach.AddRange(JsonConvert.DeserializeObject<List<Sach>>(kq2));
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach (var sach in listSach)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }

            await Navigation.PushAsync(new MH_HienThiSachTheoLoai(listSach, "BST Lord of the Rings"));
        }
    }
}