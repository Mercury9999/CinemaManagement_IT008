using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{ 
    public partial class HoaDonVM : BaseViewModel
    {
        private int _sohd { get; set; }
        public int SoHD
        {
            get { return _sohd; }
            set { _sohd = value; OnPropertyChanged(); }
        }
        private int _makh { get; set; }
        public int MaKH
        {
            get { return _makh; }
            set { _makh = value; OnPropertyChanged(); }
        }
        private int _manv { get; set; }
        public int MaNV
        {
            get { return _manv; }
            set { _manv = value; OnPropertyChanged(); }
        }
        private DateTime _ngayhd { get; set; }

        public DateTime NgayHD
        {
            get { return _ngayhd; }
            set { _ngayhd = value; OnPropertyChanged(); }
        }
        private int _chietkhau { get; set; }
        public int ChietKhau
        {
            get { return _chietkhau; }
            set { _chietkhau = value; OnPropertyChanged(); }
        }
        private decimal _giamgia { get; set; }
        public decimal GiamGia
        {
            get { return _giamgia; }
            set { _giamgia = value; OnPropertyChanged(); }
        }
        private decimal _giatrihd { get; set; }
        public decimal GiaTriHD
        {
            get { return _giatrihd; }
            set { _giatrihd = value; OnPropertyChanged(); }
        }
        private decimal _thanhtien { get; set; }
        public decimal ThanhTien
        {
            get { return _thanhtien; }
            set { _thanhtien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<HoaDonDTO> _dshd { get; set; }
        public ObservableCollection<HoaDonDTO> dsHoaDon
        {
            get { return _dshd; }
            set { _dshd = value; OnPropertyChanged(); }
        }
        public Window CurrentWindow { get; set; }
        public ICommand GetCurrentWindowCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand GetAllBillCM { get; set; }
        public ICommand OpenPayBillCM { get; set; }
        public ICommand DeleteCurrentBillCM { get; set; }
        public HoaDonVM()
        {
            GetCurrentWindowCM = new RelayCommand<Window>(p => { return true; }, p =>
            {
                CurrentWindow = p;
            });
            CloseWindowCM = new RelayCommand<Window>(p => { return true; }, p =>
            {
                CurrentWindow.Close();
            });
            OpenPayBillCM = new RelayCommand<Window>(p => { return true; }, p =>
            {
                Window w1 = new ThanhToan();
                w1.ShowDialog();
            });
            GetDataCustomer = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                KHMuaHang = await Task.Run(async () => await KhachHangDAL.Instance.GetCustomerById(MaKHMuaHang));
            });
        }
    }
}
