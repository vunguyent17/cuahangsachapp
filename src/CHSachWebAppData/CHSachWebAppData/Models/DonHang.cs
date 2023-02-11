using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSachWebAppData.Models
{
    public class DonHang
    {
        public int MaDonHang { get; set; }
        public int MaNguoiDung { get; set; }
        public string Tinh { get; set; }
        public string TPQuan { get; set; }
        public string DiaChiNha { get; set; }
        public string SoDienThoai { get; set; }
        public long ThanhTien { get; set; }
        public string PTThanhToan { get; set; }
        public int DaThanhToan { get; set; }

        public string NgayDat { get; set; }
    }
}