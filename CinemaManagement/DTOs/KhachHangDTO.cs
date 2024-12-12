using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    using CinemaManagement.Ultis;
    using MaterialDesignThemes.Wpf.Converters;
    using System;
    using System.Collections.Generic;

    public class KhachHangDTO
    {

        public KhachHangDTO()
        {
            HDTichLuy = 0;
        }

        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string GioiTinh { get; set; }
        public string SDT_KH { get; set; }
        public string email_KH { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public decimal HDTichLuy { get; set; }
        public System.DateTime NgayDK { get; set; }

        public string HDTichLuyStr
        {
            get
            {
                return MoneyFormat.FormatToVND(HDTichLuy);
            }
        }
        public string MaKHStr
        {
            get
            {
                return $"KH{MaKH:D4}";
            }
        }
    }
}
