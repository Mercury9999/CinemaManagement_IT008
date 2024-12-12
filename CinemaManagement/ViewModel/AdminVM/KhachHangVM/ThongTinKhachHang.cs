using CinemaManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CinemaManagement.ViewModel.AdminVM;
using CinemaManagement.Ultis;
using MaterialDesignThemes.Wpf;
using CinemaManagement.Models;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyKhachHangVM : BaseViewModel
    {
        public void GetDataCustomer()
        {
            KhMaKH = KHSelected.MaKHStr;
            KhTen = KHSelected.TenKH;
            KhGioiTinh = KHSelected.GioiTinh;
            KhSDT = KHSelected.SDT_KH;
            KhEmail = KHSelected.email_KH;
            KhNgaySinh = Convert.ToString(KHSelected.NgaySinh);
            KhChiTieu = Convert.ToString(KHSelected.HDTichLuy);
            KhNgayDK = KHSelected.NgayDK;
        }
    }
}
