using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
namespace CinemaManagement.DTOs
{
    public class HoaDonDTO
    {
        public HoaDonDTO()
        {
            MaKH = 0;
            ChietKhau = 0;
            GiamGia = 0;
        }
        public int SoHD { get; set; }
        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public System.DateTime NgayHD { get; set; }
        public int ChietKhau { get; set; }
        public decimal GiamGia { get; set; }
        public decimal GiaTriHD { get; set; }
        public decimal ThanhTien { get; set; }
        public string ThanhTienStr
        {
            get
            {
                return MoneyFormat.FormatToVND(ThanhTien);
            }
        }
        public string ChietKhauStr
        {
            get
            {
                return Convert.ToString(ChietKhau) + "%";
            }
        }
        public string GiamGiaStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiamGia);
            }
        }
        public string MaKHStr
        {
            get
            {
                return $"KH{MaKH:D4}";
            }
        }
        public string SoHDStr
        {
            get
            {
                return $"HD{SoHD:D6}";
            }
        }
        public string MaNVStr
        {
            get
            {
                return $"NV{MaNVStr:D4}";
            }
        }
        public KhachHangDTO KhachHang { get; set; }
        public NhanVienDTO NhanVien { get; set; }
        public int TuoiKH
        {
            get
            {
                return NgayHD.Year - KhachHang.NgaySinh.Year;
            }
        }
        public string NgayHDStr
        { 
            get
            {
                return ConvertDateTime.Full(NgayHD);
            }
        }
        public List<CTHDSanPhamDTO> CTSP { get; set; }
        public List<VeDTO> CTVe { get; set; }
    }
}
