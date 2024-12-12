using CinemaManagement.Ultis;
using System;

namespace CinemaManagement.DTOs
{

    public partial class VeDTO
    {
        public VeDTO() { }
        public int SoHD { get; set; }
        public int MaVe { get; set; }
        public int MaSC { get; set; }
        public int MaGhe { get; set; }
        public int SoGhe { get; set; }
        public decimal GiaVe { get; set; }
        public HoaDonDTO HoaDon { get; set; }
        public PhimDTO Phim { get; set; }
        public SuatChieuDTO SuatChieu { get; set; }
        public bool KiemTraTuoi
        {
            get
            {
                if (HoaDon.TuoiKH >= Phim.GioiHanTuoi) return true;
                else return false;
            }
        }
        //Dùng để in thông tin vé
        public string MaVeStr
        {
            get
            {
                return $"V{MaVe:D6}";
            }
        }
        public string TenPhimStr
        {
            get
            {
                return Phim.TenPhim;
            }
        }
        public string GiaVeStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiaVe);
            }
        }
        public string SoGheStr
        {
            get
            {
                return Convert.ToString(SoGhe);
            }
        }
        public string PhongChieuStr
        {
            get
            {
                return $"P{SuatChieu.SoPhongChieu:D2}";
            }
        }
        public string NgayChieuStr
        {
            get
            {
                return ConvertDateTime.Day(SuatChieu.BatDau);
            }
        }
        public string ThoiGianStr
        {
            get
            {
                return ConvertDateTime.Clock(SuatChieu.BatDau) + "->" + ConvertDateTime.Clock(SuatChieu.KetThuc);
            }
        }
    }
}
