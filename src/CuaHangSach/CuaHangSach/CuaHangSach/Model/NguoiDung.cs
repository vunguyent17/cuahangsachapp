using System;
using System.Collections.Generic;
using System.Text;

namespace CuaHangSach.Model
{
    public class NguoiDung
    {
        public static NguoiDung ttNguoiDung = null;
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
    }
}
