using CuaHangSach.Model;
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

namespace CuaHangSach.DanhMucSach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class SachView
    {
        public Sach SachData { get; set; }
        public int STT { get; set; }
    }
    public partial class MH_HienThiSachTheoLoai : ContentPage
    {
        public MH_HienThiSachTheoLoai()
        {
            InitializeComponent();
            LayDSSachTheoLoaiAPI();
        }

        public MH_HienThiSachTheoLoai(List<Sach> listSach, string pageTitle)
        {
            InitializeComponent();
            List<SachView> listSachViewTheoLoai = new List<SachView>();
            int dem = 0;
            foreach (var sach in listSach)
            {
                dem += 1;
                SachView sachView = new SachView() { SachData = sach, STT = dem };
                listSachViewTheoLoai.Add(sachView);

            }

            lstSachView.ItemsSource = listSachViewTheoLoai;
            Title = pageTitle;
        }
        public MH_HienThiSachTheoLoai(LoaiSachView loaisach)
        {
            InitializeComponent();
            LayDSSachTheoLoaiAPI(loaisach);
            BindingContext = loaisach;
                     
            Title = loaisach.loaiSach.TenLoai;
        }

        public async void LayDSSachTheoLoaiAPI(LoaiSachView loaisachview = null)
        {
            List<Sach> listSachTheoLoai;
            HttpClient http = new HttpClient();

            try
            {
                string kq;
                if (loaisachview == null)
                {
                    kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LayDSSach");
                }
                else
                {
                    kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LayDSSachTheoLoai?MaLoai=" + loaisachview.loaiSach.MaLoai.ToString());
                    Title = loaisachview.loaiSach.TenLoai;
                }
                listSachTheoLoai = JsonConvert.DeserializeObject<List<Sach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            List<SachView> listSachViewTheoLoai = new List<SachView>();

            int dem = 0;
            foreach (var sach in listSachTheoLoai)
            {
                dem += 1;
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
                SachView sachView = new SachView() { SachData = sach, STT = dem };
                listSachViewTheoLoai.Add(sachView);

            }

            lstSachView.ItemsSource = listSachViewTheoLoai;

        }


        private void lstSachView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSachView.SelectedItem == null)
                return;

            if (e.CurrentSelection != null)
            {
                SachView sach_view = (SachView)e.CurrentSelection.FirstOrDefault();
                MH_ChiTietSach mh = new MH_ChiTietSach(sach_view.SachData);
                Navigation.PushAsync(mh);
                lstSachView.SelectedItem = null;
            }
        }
    }
}