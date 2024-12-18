using CinemaManagement.Models;
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    
    public class SanPhamDTO
    {
        public SanPhamDTO()
        {
            
        }
        public int MaSP { get; set; }
        public string LoaiSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaSP { get; set; }
        public byte[] HinhAnhSP { get; set; }
        public Nullable<bool> IsDeleted { get; set; } = false;
        public string GiaSPStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiaSP);
            }
        }
        public string TongTienStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiaSP * SoLuong);
            }
        }
        public string MaSPStr
        {
            get
            {
                return $"SP{MaSP:D4}";
            }
        }

    }
}
