using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSachWebAppData.Models
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
    }
}