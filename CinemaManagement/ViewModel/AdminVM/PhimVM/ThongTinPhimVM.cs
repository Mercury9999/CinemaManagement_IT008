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

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM : BaseViewModel
    {
        public void GetDataFilm()
        { 
            MaPhim = PhimSelected.MaPhimStr;
            TheLoai = PhimSelected.TheLoai;
            NgayPH = ConvertDateTime.Day(PhimSelected.NgayPH);
            TenPhim = PhimSelected.TenPhim;
            NuocSX = PhimSelected.NuocSX;
            ThoiLuong = PhimSelected.ThoiLuong;
            GioiHanTuoi = Convert.ToString(PhimSelected.GioiHanTuoi);
            NoiDung = PhimSelected.NoiDung;
            Poster = PhimSelected.Poster;
            DaoDien = PhimSelected.DaoDien;
        }
    }
}
