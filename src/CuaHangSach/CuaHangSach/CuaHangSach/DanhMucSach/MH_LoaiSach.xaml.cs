using CuaHangSach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CuaHangSach.DanhMucSach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class LoaiSachView
    {
        public LoaiSach loaiSach { get; set; }
        public string mauSac { get; set; }

    }
    public partial class MH_LoaiSach : ContentPage
    {
        public MH_LoaiSach()
        {
            InitializeComponent();
            LayDSLoaiSachAPI();
        }

        public async void LayDSLoaiSachAPI()
        {
            List<LoaiSach> listLoaiSach;
            HttpClient http = new HttpClient();

            try
            {
                var kq = await http.GetStringAsync(CuaHangSach.App.CHSachURL + "api/CHSachController/LayDSLoaiSach");
                listLoaiSach = JsonConvert.DeserializeObject<List<LoaiSach>>(kq);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            List<string> colors = new List<string> { "DarkBlue", "Crimson", "Purple", "DeepPink", "Black", "Green", "SaddleBrown" };
            List<LoaiSachView> listLoaiSachView = new List<LoaiSachView>();
            int dem = 0;
            foreach (LoaiSach ls in listLoaiSach)
            {
                string colorText = dem < colors.Count ? colors[dem] : "Blue";
                LoaiSachView loaiSachView = new LoaiSachView() { loaiSach = ls, mauSac = colorText };
                dem += 1;
                listLoaiSachView.Add(loaiSachView);
            }
            lstMHLoaiSachView.ItemsSource = listLoaiSachView;
        }

        public void lstMHLoaiSachView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMHLoaiSachView.SelectedItem == null)
                return;
            LoaiSachView loaiSachView;
            
            if (e.CurrentSelection != null)
            {
                loaiSachView = (LoaiSachView)e.CurrentSelection.FirstOrDefault();
                LoaiSach loaiSach = loaiSachView.loaiSach;
                MH_HienThiSachTheoLoai mh = new MH_HienThiSachTheoLoai(loaiSachView);
                Navigation.PushAsync(mh);
                lstMHLoaiSachView.SelectedItem = null;
            }
        }

        private void searchSach_SearchButtonPressed(object sender, EventArgs e)
        {
            string tuTimKiem = ((SearchBar)sender).Text;
            Navigation.PushAsync(new MH_TimKiemSach(tuTimKiem));
        }
    }
}
