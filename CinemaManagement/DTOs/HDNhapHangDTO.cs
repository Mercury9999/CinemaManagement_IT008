using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class HDNhapHangDTO
    {
        public HDNhapHangDTO() { }
        public int SoHDNhap { get; set; }
        public System.DateTime NgayNhap { get; set; }
        public decimal ThanhTien { get; set; }
        public int MaNVNhap { get; set; }
        public string TenNVNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public int MaSPNhap { get; set; }
        public int SoLuong {  get; set; }
        public string TenSPNhap { get; set; }
        public string SoHDNhapStr
        {
            get
            {
                return $"Nhap{SoHDNhapStr:D6}";
            }
        }

        public string NgayNhapStr
        {
            get
            {
                return ConvertDateTime.Full(NgayNhap);
            }
        }
    }
}
