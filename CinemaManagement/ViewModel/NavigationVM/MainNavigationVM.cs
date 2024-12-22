using CinemaManagement.View;
using CinemaManagement.View.LoginWindow;
using CinemaManagement.View.AdminView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaManagement.View.AdminView.AccountView;

namespace CinemaManagement.ViewModel.NavigationVM
{
    public class MainNavigationVM : BaseViewModel
    {
        public ICommand QuanLyPhimCM { get; set; }
        public ICommand QuanLySuatChieuCM { get; set; }
        public ICommand QuanLySanPhamCM { get; set; }
        public ICommand QuanLyKhachHangCM { get; set; }
        public ICommand QuanLyHoaDonCM { get; set; }
        public ICommand QuanLyNhanVienCM { get; set; }
        public ICommand AccountCM {  get; set; }
        public ICommand GetNavigationFrameCM {  get; set; }

        public Frame NavigationFrame { get; set; }
        
        public MainNavigationVM()
        {
            GetNavigationFrameCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                NavigationFrame = p;
            });
            QuanLyPhimCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new QuanLyPhim());
            });
            QuanLySuatChieuCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new QuanLySuatChieu());
            });
            QuanLySanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new QuanLySanPham());
            });
            QuanLyKhachHangCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new QuanLyKhachHang());
            });
            QuanLyNhanVienCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new QuanLyNhanVien());
            });
            QuanLyHoaDonCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ChiTietHoaDon());
            });
            AccountCM = new RelayCommand<object> ((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new AccountView());
            }
            );
        }
    }
}
