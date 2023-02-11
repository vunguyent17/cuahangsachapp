using CuaHangSach.Model;
using CuaHangSach.TaiKhoan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach.DanhMucSach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ChiTietSachView : Sach
    {
        public string TenLoai { get; set; }
    }
    public partial class MH_ChiTietSach : ContentPage
    {
        public MH_ChiTietSach(Sach sach)
        {
            InitializeComponent();
            HienThiChiThietSach(sach);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (NguoiDung.ttNguoiDung.MaNguoiDung == 0)
            {
                btnThemVaoGioHang.IsEnabled = false;
            }
            else
            {
                btnThemVaoGioHang.IsEnabled = true;
            }

        }

        public async void HienThiChiThietSach(Sach sach)
        {
            ChiTietSachView ct = new ChiTietSachView()
            {
                MaLoai = sach.MaLoai,
                MaSach = sach.MaSach,
                DonGia = sach.DonGia,
                ISBN = sach.ISBN,
                TenSach = sach.TenSach,
                NamXuatBan = sach.NamXuatBan,
                NhaXuatBan = sach.NhaXuatBan,
                Hinh = sach.Hinh,
                TacGia = sach.TacGia,
            };
            var kq = await LayLoaiSachAPI(ct.MaLoai);
            ct.TenLoai = kq.TenLoai;
            BindingContext = ct;
        }
        public async Task<LoaiSach> LayLoaiSachAPI(int maloai)
        {
            LoaiSach loaiSach;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LayLoaiSach?MaLoai=" + maloai.ToString());
                loaiSach = JsonConvert.DeserializeObject<List<LoaiSach>>(kq)[0];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            return loaiSach;
        }

        private async void btnThemVaoGioHang_Clicked(object sender, EventArgs e)
        {
            string maSach = ((Button)sender).BindingContext as string;
            SachGioHang sgh = new SachGioHang
            {
                MaNguoiDung = NguoiDung.ttNguoiDung.MaNguoiDung,
                MaSach = maSach,
                SoLuong = 1,
            };
            HttpClient http = new HttpClient();

            string jsonsgh = JsonConvert.SerializeObject(sgh);
            Debug.WriteLine(jsonsgh);
            StringContent httpcontent = new StringContent(jsonsgh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/ThemSachVaoGioHang", httpcontent);
            var kqtv = await  kq.Content.ReadAsStringAsync();
            int kqint = int.Parse(kqtv);
            if (kqint == 1)
            {
                await DisplayAlert("Thông báo", "Thêm sách vào giỏ hàng thành công", "OK");
                App.ThayDoiGioHang = true;

            }  
            else
                await DisplayAlert("Thông báo", "Không thể thêm sách vào giỏ hàng", "OK");
        }
    }
}