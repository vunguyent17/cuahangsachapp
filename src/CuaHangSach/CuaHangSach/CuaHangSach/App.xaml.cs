using CuaHangSach.Model;
using System;
using System.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CuaHangSach
{
    public partial class App : Application
    {
        public static string CHSachURL = API.APIKeys.CHSachURL;
        public static bool ThayDoiGioHang = false;
        public App()
        {
            InitializeComponent();
            NguoiDung.ttNguoiDung = new NguoiDung() { MaNguoiDung = 0 };
            MainPage = new CuaHangSachShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
