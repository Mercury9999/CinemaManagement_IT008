//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phim
    {
        public Phim()
        {
            this.SuatChieux = new HashSet<SuatChieu>();
        }
    
        public int MaPhim { get; set; }
        public string TheLoai { get; set; }
        public int ThoiLuong { get; set; }
        public string NuocSX { get; set; }
        public Nullable<System.DateTime> NgayPH { get; set; }
        public string DaoDien { get; set; }
        public string NoiDung { get; set; }
        public byte GioiHanTuoi { get; set; }
        public byte[] Poster { get; set; }
        public string TenPhim { get; set; }
        public decimal DoanhThu { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<SuatChieu> SuatChieux { get; set; }
    }
}
