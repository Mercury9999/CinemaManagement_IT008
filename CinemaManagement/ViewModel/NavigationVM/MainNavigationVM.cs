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
using CinemaManagement.ViewModel.AdminVM;
using CinemaManagement.DTOs;
using System.Collections.ObjectModel;

namespace CinemaManagement.ViewModel.NavigationVM
{
    public class MainNavigationVM : BaseViewModel
    {
        public BillService billService = BillService.Instance;
        public AccountService accountService = AccountService.Instance;
        public ICommand QuanLyPhimCM { get; set; }
        public ICommand QuanLySuatChieuCM { get; set; }
        public ICommand QuanLySanPhamCM { get; set; }
        public ICommand QuanLyKhachHangCM { get; set; }
        public ICommand QuanLyHoaDonCM { get; set; }
        public ICommand QuanLyNhanVienCM { get; set; }
        public ICommand AccountCM { get; set; }
        public ICommand ThongKeCM { get; set; }
        public ICommand GetNavigationFrameCM {  get; set; }
        public ICommand LogoutCM { get; set; }

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
            ThongKeCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKeView());
            });
            AccountCM = new RelayCommand<object> ((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new AccountView());
            });
            LogoutCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                billService.dsVeHD = new ObservableCollection<BanVeDTO>();
                billService.dsSanPhamHD = new ObservableCollection<SanPhamDTO>();
                accountService.CurrentAccount = null;
                Window w1 = new LoginWindow();
                p.Close();
                w1.ShowDialog();
            });
        }
    }
}
