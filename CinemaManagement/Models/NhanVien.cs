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
    
    public partial class NhanVien
    {
        public NhanVien()
        {
            this.HoaDons = new HashSet<HoaDon>();
            this.SuCoes = new HashSet<SuCo>();
        }
    
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string acc_username { get; set; }
        public string acc_password { get; set; }
        public string SDT_NV { get; set; }
        public string email_NV { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public System.DateTime NgayVaoLam { get; set; }
        public string ChucVu { get; set; }
        public byte Staff_Level { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<HDNhapHang> HDNhapHangs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<SuCo> SuCoes { get; set; }
    }
}
