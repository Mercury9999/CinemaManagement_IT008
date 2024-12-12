using System;
using System.Collections.Generic;
using CinemaManagement.Ultis;

namespace CinemaManagement.DTOs
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVienDTO
    {
        public NhanVienDTO() { }
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
        public byte Staff_Level
        {
            get
            {
                if (ChucVu == "Quản lý") return 1;
                else return 0;
            }
        }
        public Nullable<bool> IsDeleted { get; set; } = false;
        public string MaNVStr
        {
            get
            {
                return $"NV{MaNV:D4}";
            }
        }
        public string NgaySinhStr
        {
            get
            {
                return ConvertDateTime.Day(NgaySinh);
            }
        }
        public string NgayVaoLamStr
        {
            get
            {
                return ConvertDateTime.Day(NgayVaoLam);
            }
        }
    }
}
