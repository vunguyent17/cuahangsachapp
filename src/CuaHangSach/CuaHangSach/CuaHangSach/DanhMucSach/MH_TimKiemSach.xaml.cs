using CuaHangSach.Model;
using CuaHangSach.TaiKhoan;
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

namespace CuaHangSach.DanhMucSach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_TimKiemSach : ContentPage
    {
        public MH_TimKiemSach(string tuTimKiem)
        {
            InitializeComponent();
            searchSach.Text = tuTimKiem;
        }

        public async void LayDSSachTheoTuTimKiem(string TuTimKiem)
        {
            List<Sach> listSachKQ;
            HttpClient http = new HttpClient();

            try
            {
                string kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/TimKiemSach?TuTimKiem=" + TuTimKiem);
                listSachKQ = JsonConvert.DeserializeObject<List<Sach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            List<SachView> listSachKQView = new List<SachView>();

            int dem = 0;
            foreach (var sach in listSachKQ)
            {
                dem += 1;
                sach.Hinh = CuaHangSach.App.CHSachURL + "images/" + sach.Hinh;
                SachView sachView = new SachView() { SachData = sach, STT = dem };
                listSachKQView.Add(sachView);

            }

            lstSachView.ItemsSource = listSachKQView;

        }

        private void searchSach_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tuTimKiem = ((SearchBar)sender).Text;
            if (tuTimKiem!= "")
            {
                LayDSSachTheoTuTimKiem(tuTimKiem);
            }
            else
            {
                List<Sach> listSachKQ = new List<Sach>();
                lstSachView.ItemsSource = listSachKQ;
            }
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