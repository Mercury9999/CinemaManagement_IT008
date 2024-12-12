using CinemaManagement.Ultis;
using CinemaManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyNhanVienVM : BaseViewModel
    {
        public void GetDataStaff()
        {
            MaNV = NVSelected.MaNVStr;
            TenNV = NVSelected.TenNV;
            AccUsername = NVSelected.acc_username;
            AccPassword = NVSelected.acc_password;
            SDT = NVSelected.SDT_NV;
            Email = NVSelected.email_NV;
            NgaySinh = NVSelected.NgaySinh;
            GioiTinh = NVSelected.GioiTinh;
            NgayVaoLam = ConvertDateTime.Day(NVSelected.NgayVaoLam);
            ChucVu = NVSelected.ChucVu;
        }
    }
}
