using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using CinemaManagement.Ultis;

namespace CinemaManagement.DTOs
{
    
    public class PhimDTO
    { 
        public PhimDTO()
        {
            NgayPH = null;
            GioiHanTuoi = 0;
        }
    
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } 
        public string TheLoai { get; set; }
        public int ThoiLuong { get; set; }
        public string NuocSX { get; set; }
        public Nullable<System.DateTime> NgayPH { get; set; }
        public string DaoDien { get; set; }
        public string NoiDung { get; set; }
        public byte GioiHanTuoi { get; set; }
        public byte[] Poster { get; set; }
        public decimal DoanhThu { get; set; }   
    
        public IList<SuatChieuDTO> SuatChieu { get; set; }

        public string MaPhimStr
        {
            get
            {
                return $"P{MaPhim:D4}";
            }
        }
        public string DoanhThuStr
        {
            get
            {
                return MoneyFormat.FormatToVND(DoanhThu);
            }
        }
    }
}
