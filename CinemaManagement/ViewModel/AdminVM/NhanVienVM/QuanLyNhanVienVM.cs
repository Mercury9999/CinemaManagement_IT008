using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using System.Windows;
using CinemaManagement.Ultis;
using System.Windows.Input;
using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.View.AdminView;
using CinemaManagement.CustomControls;
using System.Web.UI;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyNhanVienVM : BaseViewModel
    {
        #region Icommand

        public ICommand ViewStaffCM { get; set; }
        public ICommand AddStaffCM { get; set; }
        public ICommand UpdateStaffCM { get; set; }
        public ICommand DeleteStaffCM { get; set; }

        public ICommand LoadDataStaffCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand GetQLNVPageCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand SaveNewStaffCM { get; set; }
        public ICommand SaveStaffCM { get; set; }
        public ICommand GetCurrentWindow { get; set; }
        #endregion
        #region Thuộc tính lưu trữ dữ liệu cho nhân viên

        private string _maNV { get; set; }
        public string MaNV { get { return _maNV; } set { _maNV = value; OnPropertyChanged(); } }
        private string _tenNV { get; set; }
        public string TenNV { get { return _tenNV; } set { _tenNV = value; OnPropertyChanged(); } }
        private string _accusername { get; set; }
        public string AccUsername { get { return _accusername; } set { _accusername = value; OnPropertyChanged(); } }
        private string _accpassword { get; set; }
        public string AccPassword { get { return _accpassword; } set { _accpassword = value; OnPropertyChanged(); } }
        private string _sdt { get; set; }
        public string SDT { get { return _sdt; } set { _sdt = value; OnPropertyChanged(); } }
        private string _email { get; set; }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        private string _ngaysinh { get; set; }
        public string NgaySinh { get { return _ngaysinh; } set { _ngaysinh = value; OnPropertyChanged(); } }
        private string _gioitinh { get; set; }
        public string GioiTinh { get { return _gioitinh; } set { _gioitinh = value; OnPropertyChanged(); } }
        private string _ngayvaolam { get; set; }
        public string NgayVaoLam { get { return _ngayvaolam; } set { _ngayvaolam = value; OnPropertyChanged(); } }
        private string _chucvu { get; set; }
        public string ChucVu { get { return _chucvu; } set { _chucvu = value; OnPropertyChanged(); } }
        #endregion

        #region  Biến khác
        private Page QuanLyNVPage { get; set; }
        private Window CurrentWindow { get; set; }
        private NhanVienDTO _NVselected { get; set; }
        public NhanVienDTO NVSelected { get { return _NVselected; } set { _NVselected = value; OnPropertyChanged(); } }
        private ObservableCollection<NhanVienDTO> _dsNV { get; set; }
        public ObservableCollection<NhanVienDTO> dsNV { get { return _dsNV; } set { _dsNV = value; OnPropertyChanged(); } }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        private ObservableCollection<string> _dschucvu { get; set; }
        public ObservableCollection<string> dsChucVu { get { return _dschucvu; } set { _dschucvu = value; OnPropertyChanged(); } }
        #endregion
        public QuanLyNhanVienVM()
        {
            GetQLNVPageCM = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                QuanLyNVPage = p;
            });
            GetCurrentWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            LoadDataStaffCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await NhanVienDAL.Instance.GetAllStaff());
                    dsNV = new ObservableCollection<NhanVienDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                    return;
                }
            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddStaffCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemNhanVien w1 = new ThemNhanVien();
                w1.ShowDialog();
            });
            SaveNewStaffCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewStaff(p);
                IsSaving = false;
            });
            DeleteStaffCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận muốn xoá?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    IsLoading = true;
                    (bool trangthai, string message) = await NhanVienDAL.Instance.DeleteStaff(NVSelected.MaNV);
                    if (trangthai)
                    {
                        for (int i = 0; i < dsNV.Count; i++)
                        {
                            if (dsNV[i].MaNV == NVSelected.MaNV)
                            {
                                dsNV.Remove(dsNV[i]);
                                break;
                            }
                        }
                    }
                    CustomControls.MyMessageBox.Show(message);
                }
            });
            ViewStaffCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (NVSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    ThongTinNhanVien w1 = new ThongTinNhanVien();
                    GetDataStaff();
                    w1.ShowDialog();
                    IsLoading = false;
                }
            });
            UpdateStaffCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaNhanVien w1 = new SuaNhanVien();
                GetDataStaff();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveStaffCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdateStaff(p);
                IsSaving = false;
            });
            dsChucVu = new ObservableCollection<string>
            {
                "Quản lý",
                "Thu ngân",
                "Bảo vệ",
                "Lao công",
                "Phục vụ"
            };
            
        }
        private void ClearData()
        {
            MaNV = null;
            TenNV = null;
            AccUsername = null;
            AccPassword = null;
            SDT = null;
            Email = null;
            NgaySinh = null;
            GioiTinh = null;
            NgayVaoLam = null;
            ChucVu = null;
        }
    }
}
