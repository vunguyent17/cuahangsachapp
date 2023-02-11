using CuaHangSach.DanhMucSach;
using CuaHangSach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace CuaHangSach.GioHang
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_GioHang : ContentPage
    {
        public class SachGioHangView
        {
            public SachGioHang SachData { get; set; }
            public bool isChecked { get; set; }
        }


        List<SachGioHangView> listSachGioHangView;
        List<SachGioHang> listSachGioHang;
        int MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung;
        public MH_GioHang()
        {
            InitializeComponent();
            lblThanhTien.Text = "0 đ";
            lblTongCong.Text = "Tổng cộng (0)";
            if (NguoiDung.ttNguoiDung.MaNguoiDung == 0)
            {
                chkTatCa.IsEnabled = false;
                btnThanhToan.IsEnabled = false;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (NguoiDung.ttNguoiDung.MaNguoiDung != MaNguoiDung || App.ThayDoiGioHang == true)
            {
                MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung;
                LayDSSachGioHang();
                App.ThayDoiGioHang = false;
            }
            
        }

        public async void LayDSSachGioHang()
        {
            btnThanhToan.IsEnabled = true;
            chkTatCa.IsEnabled = true;

            HttpClient http = new HttpClient();
            try
            
            {
                string kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LayGioHangChiTiet?MaNguoiDung=" + NguoiDung.ttNguoiDung.MaNguoiDung);

                listSachGioHang = JsonConvert.DeserializeObject<List<SachGioHang>>(kq);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            foreach (var sach in listSachGioHang)
            {
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
            }
            HienThiSachGioHang(listSachGioHang, false);
            
        }

        void HienThiSachGioHang(List<SachGioHang> listSachGioHang, bool checkedValue)
        {
            listSachGioHangView = new List<SachGioHangView>();
            foreach (var sach in listSachGioHang)
            {
                SachGioHangView sachgh = new SachGioHangView() { SachData = sach, isChecked = checkedValue };
                listSachGioHangView.Add(sachgh);
            }
            lstSachView.ItemsSource = listSachGioHangView;
        }

        private async void stpSoLuong_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            Stepper stepperSach = sender as Stepper;
            SachGioHangView sghv = stepperSach.BindingContext as SachGioHangView;
            string dataMaSach = sghv.SachData.MaSach;
            if (value <= 0)
            {
                bool kqxoa = await DisplayAlert("Thông báo", "Bạn có muốn xóa sách ra khỏi giỏ hàng", "Đồng ý xóa", "Hủy");
                if (kqxoa)
                {
                    XoaSachGioHang(dataMaSach);
                }
                else
                {
                    List<SachGioHangView> listMoi = new List<SachGioHangView>();
                    foreach (var sach in listSachGioHangView)
                    {
                            listMoi.Add(sach);
                    }

                    listSachGioHangView = listMoi;
                    lstSachView.ItemsSource = listMoi;
                }
                return;
            }

            
            int kqint = 1;
            foreach (var sach in listSachGioHang)
            {
                if (sach.MaSach == dataMaSach)
                {
                    sach.SoLuong = (int)value;
                    SachGioHang sgh = new SachGioHang
                    {
                        MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung,
                        MaSach = dataMaSach,
                        SoLuong = sach.SoLuong,
                    };
                    HttpClient http = new HttpClient();

                    string jsonnd = JsonConvert.SerializeObject(sgh);
                    StringContent httpcontent = new StringContent(jsonnd, Encoding.UTF8, "application/json");
                    HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/ThemSachVaoGioHang", httpcontent);
                    var kqtv = await kq.Content.ReadAsStringAsync();
                    kqint = int.Parse(kqtv);
                    break;
                }
            }
            TinhToanThanhTien();  
            //if (kqint == 1)
            //    await DisplayAlert("Thông báo", "Cập nhật giỏ hàng thành công", "OK");
            //else
            //    await DisplayAlert("Thông báo", "Không thể cập nhật giỏ hàng", "OK");
        }

        private void btnThanhToan_Clicked(object sender, EventArgs e)
        {
            List<string> listMaSachDatHang = new List<string>();
            
            foreach (var sach in listSachGioHangView)
            {
                if (sach.isChecked)
                {
                    listMaSachDatHang.Add(sach.SachData.MaSach);
                }
                
            }
            Navigation.PushAsync(new MH_ThanhToan(listMaSachDatHang));
        }

        private void chkThanhToan_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool checkAll = true;
            foreach (var sach in listSachGioHangView)
            {
                if (sach.isChecked == false)
                {
                   checkAll = false;
                   break;
                }
            }
            chkTatCa.IsChecked = checkAll;
            TinhToanThanhTien();
        }

        void TinhToanThanhTien()
        {
            long tongTien = 0;
            int soLuongDatHang = 0;
            foreach (var sach in listSachGioHangView)
            {
                if (sach.isChecked)
                {
                    tongTien += sach.SachData.DonGia * sach.SachData.SoLuong;
                    soLuongDatHang += 1;
                }
            }
            if (tongTien == 0)
            {
                lblThanhTien.Text = "0 đ";
            }
            else
            {
                lblThanhTien.Text = String.Format("{0:### ### ###} đ", tongTien);
            }
            lblTongCong.Text = $"Tổng cộng ({soLuongDatHang})";
        }

        private void chkTatCa_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
            bool isAllChecked = ((CheckBox)sender).IsChecked;
            int so_sach_checked = 0;
            foreach (var sach in listSachGioHangView)
            {
                if (sach.isChecked)
                {
                    so_sach_checked += 1;
                }
            }
            
            if (!(isAllChecked == false && so_sach_checked < listSachGioHangView.Count))
            {
                HienThiSachGioHang(listSachGioHang, isAllChecked);
            }
            TinhToanThanhTien();
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var swipeview = sender as SwipeItem;
            var item = (string)swipeview.BindingContext;
            XoaSachGioHang(item);
        }



        async void XoaSachGioHang(string MaSach)
        {
            HttpClient http = new HttpClient();
            StringContent httpcontent = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + $"api/CHSachController/XoaSachGioHang?MaNguoiDung={NguoiDung.ttNguoiDung.MaNguoiDung}&MaSach={MaSach}", httpcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            int kqint = int.Parse(kqtv);
            if (kqint == 1)
            {
                var sachGioHangRemove = listSachGioHang.Find(sach => sach.MaSach == MaSach);
                if (sachGioHangRemove != null)
                    listSachGioHang.Remove(sachGioHangRemove);


                List<SachGioHangView> listMoi = new List<SachGioHangView>();
                foreach (var sach in listSachGioHangView)
                {
                    if (sach.SachData.MaSach != MaSach)
                    {
                        listMoi.Add(sach);
                    }
                }

                listSachGioHangView = listMoi;
                lstSachView.ItemsSource = listMoi;
                await DisplayAlert("Thông báo", "Xóa sách khỏi giỏ hàng thành công", "OK");
            }
               
            else
                await DisplayAlert("Thông báo", "Không thể xóa sách khỏi giỏ hàng", "OK");
        }
    }
}