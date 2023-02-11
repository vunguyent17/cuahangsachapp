using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSachWebAppData.Models
{
    public class Sach
    {
        public string ISBN { get; set; }

        public string MaSach { get; set; }

        public string TenSach { get; set; }

        public string TacGia { get; set; }

        public int DonGia { get; set; }

        public int NamXuatBan { get; set; }

        public string NhaXuatBan { get; set; }

        public int MaLoai { get; set; }

        public string Hinh { get; set; }
    }
}