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
    
    public partial class SuatChieu
    {
        public SuatChieu()
        {
            this.BanVes = new HashSet<BanVe>();
            this.Ves = new HashSet<Ve>();
        }
    
        public int MaSC { get; set; }
        public int MaPhim { get; set; }
        public int SoPhongChieu { get; set; }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public decimal GiaVe { get; set; }
    
        public virtual ICollection<BanVe> BanVes { get; set; }
        public virtual Phim Phim { get; set; }
        public virtual PhongChieu PhongChieu { get; set; }
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
