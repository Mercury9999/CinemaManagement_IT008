using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.Ultis;
using CinemaManagement.View;
using CinemaManagement.View.AdminView.HoaDonView;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class HoaDonVM : BaseViewModel
    {
        public AccountService accountService { get; set; } = AccountService.Instance;
        private string tenkh {  get; set; }
        public string TenKH
        {
            get { return tenkh; }
            set { tenkh = value; OnPropertyChanged(); }
        }
        private string sdtkh { get; set; }
        public string SDTKH
        {
            get { return sdtkh; }
            set { sdtkh = value; OnPropertyChanged(); }
        }
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
        public string GiamGiaStr { get; set; }
        private decimal _giatrihd { get; set; }
        public decimal GiaTriHD
        {
            get { return _giatrihd; }
            set { _giatrihd = value; OnPropertyChanged(); }
        }
        public string GiaTriHDStr { get; set; }
        private decimal _thanhtien { get; set; }
        public decimal ThanhTien
        {
            get { return _thanhtien; }
            set { _thanhtien = value; OnPropertyChanged(); }
        }
        public string ThanhTienStr { get; set; }
        private ObservableCollection<HoaDonDTO> _dshd { get; set; } = new ObservableCollection<HoaDonDTO>();
        public ObservableCollection<HoaDonDTO> dsHoaDon 
        {
            get { return _dshd; }
            set { _dshd = value; OnPropertyChanged(); }
        } 
        private ObservableCollection<SanPhamDTO> _dsSanPham { get; set; } = new ObservableCollection<SanPhamDTO>();
        public ObservableCollection<SanPhamDTO> dsSanPham
        {
            get
            {
                return _dsSanPham;
            }
            set
            {
                _dsSanPham = value; OnPropertyChanged();
            }
        }
        private ObservableCollection<BanVeDTO> _dsVe { get; set; } = new ObservableCollection<BanVeDTO>();
        public ObservableCollection<BanVeDTO> dsVe
        {
            get
            {
                return _dsVe;
            }
            set
            {
                _dsVe = value; OnPropertyChanged();
            }
        }
        public Window CurrentWindow { get; set; }
        public ICommand GetCurrentWindowCM { get; set; }
        public ICommand SetDafaultDataCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand GetAllBillCM { get; set; }
        public ICommand OpenPayBillCM { get; set; }
        public ICommand DeleteCurrentBillCM { get; set; }
        public ICommand PayCM {  get; set; }
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
            SetDafaultDataCM = new RelayCommand<Window>(p => { return true; }, async(p) =>
            {
                MaKHMuaHang = 0;
                KHMuaHang = await Task.Run(async () => await KhachHangDAL.Instance.GetCustomerById(MaKHMuaHang));
                TenKH = KHMuaHang.TenKH;
            });
            GetAllBillCM = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                var data = await Task.Run(async () => await HoaDonDAL.Instance.GetAllBill());
                dsHoaDon = new ObservableCollection<HoaDonDTO>(data);
            });
            DeleteCurrentBillCM = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                ClearData();
                billService.dsVeHD.Clear();
                billService.dsSanPhamHD.Clear();
                MyMessageBox.Show("Đã xoá");
            });
            OpenPayBillCM = new RelayCommand<Window>(p => { return true; }, p =>
            {
                ClearData();
                for (int i = 0; i < billService.dsSanPhamHD.Count; i++)
                {
                    dsSanPham.Add(billService.dsSanPhamHD[i]);
                }
                for (int i = 0; i < billService.dsVeHD.Count; i++)
                {
                    dsVe.Add(billService.dsVeHD[i]);
                }
                for (int i = 0; i < dsSanPham.Count; i++)
                {
                    GiaTriHD += dsSanPham[i].SoLuong * dsSanPham[i].GiaSP;
                }
                for(int i = 0; i < dsVe.Count; i++)
                {
                    GiaTriHD += dsVe[i].SuatChieu.GiaVe;
                }
                GiamGia = 0;
                ThanhTien = GiaTriHD - GiamGia;
                GiaTriHDStr = MoneyFormat.FormatToVND(GiaTriHD);
                ThanhTienStr = MoneyFormat.FormatToVND(ThanhTien);
                GiamGiaStr = MoneyFormat.FormatToVND(GiamGia);
                Window w1 = new ThanhToan();
                w1.ShowDialog();
            });
            GetDataCustomerCM = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                KHMuaHang = await Task.Run(async () => await KhachHangDAL.Instance.GetCustomerById(MaKHMuaHang));
                TenKH = KHMuaHang.TenKH;
                SDTKH = KHMuaHang.SDT_KH;
            });
            PayCM = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                await SaveNewBill();
                ClearData();
                billService.dsVeHD.Clear();
                billService.dsSanPhamHD.Clear();
            });
        }
        public void ClearData()
        {
            dsSanPham.Clear();
            dsVe.Clear();
            GiaTriHD = 0;
            ThanhTien = 0;
            GiamGia = 0;
            ChietKhau = 0;
            KHMuaHang = null;
        }
    }
}
