using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using CinemaManagement.View.AdminView.KhachHangView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyKhachHangVM : BaseViewModel
    {
        #region Thuộc tính

        private KhachHangDTO _khSelected { get; set; }

        public KhachHangDTO KHSelected
        {
            get { return _khSelected; }
            set { _khSelected = value; OnPropertyChanged(); }
        }
        private string _khEmail;
        public string KhEmail
        {
            get { return _khEmail; }
            set { _khEmail = value; OnPropertyChanged(); }
        }
        
       
        private string _khSDT;
        public string KhSDT
        {
            get { return _khSDT; }
            set { _khSDT = value; OnPropertyChanged(); }
        }
        private string _khTen;
        public string KhTen
        {
            get { return _khTen; }
            set { _khTen = value; OnPropertyChanged(); }
        }
        private string _khGioiTinh;
        public string KhGioiTinh
        {
            get { return _khGioiTinh; }
            set { _khGioiTinh = value; OnPropertyChanged(); }
        }
        
        private string _khNgaySinh;
        public string KhNgaySinh
        {
            get { return _khNgaySinh; }
            set
            {
                if (_khNgaySinh != value) // Kiểm tra thay đổi giá trị
                {
                    _khNgaySinh = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _khChiTieu;
        public string KhChiTieu
        {
            get { return _khChiTieu; }
            set { _khChiTieu = value; OnPropertyChanged(); }
        }
        private DateTime? _khNgayDK;
        public DateTime? KhNgayDK
        {
            get { return _khNgayDK; }
            set
            {
                if (_khNgayDK != value) // Kiểm tra thay đổi giá trị
                {
                    _khNgayDK = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> GenderOptions { get; set; }
        private string _khMaKH;
        public string KhMaKH
        {
            get { return _khMaKH; }
            set { _khMaKH = value; OnPropertyChanged(); }
        }
        private ObservableCollection<KhachHangDTO> _dskh;
        public ObservableCollection<KhachHangDTO> dsKH
        {

            get { return _dskh; }
            set { _dskh = value; OnPropertyChanged(); }
        }
        private ObservableCollection<KhachHangDTO> _tatcakh;
        public ObservableCollection<KhachHangDTO> tatcaKH
        {

            get { return _tatcakh; }
            set { _tatcakh = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> SearchList { get; set; }
        private string _searchtext { get; set; }
        public string SearchText { get { return _searchtext; } set { _searchtext = value; OnPropertyChanged(); } }
        private string _searchproperties {  get; set; }
        public string SearchProperties { get { return _searchproperties; } set { _searchproperties = value; OnPropertyChanged(); } }
        private Window CurrentWindow { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }

        #endregion

        #region ICommand
        public ICommand GetListViewCM { get; set; }

        public ICommand AddCustomerCM { get; set; }
        public ICommand UpdateCustomerCM { get; set; }
        public ICommand DeleteCustomerCM { get; set; }

        public ICommand OpenAddCustomerCM { get; set; }
        public ICommand OpenUpdateCustomerCM { get; set; }
        public ICommand OpenDeleteCustomerCM { get; set; }
        public ICommand SaveNewCustomerCM { get; set; }
        public ICommand LoadDataCustomerCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand ViewCustomerCM { get; set; }
        public ICommand SaveCustomerCM { get; set; }
        public ICommand GetCurrentWindow { get; set; }
        public ICommand SearchData {  get; set; }
        public ICommand ResetData { get; set; }
        #endregion
        public QuanLyKhachHangVM()
        {
            GenderOptions = new ObservableCollection<string> { "Nam", "Nữ", "Khác" };
            SearchList = new ObservableCollection<string> { "Tên", "Email", "SĐT", "Tất cả" };
            KhGioiTinh = GenderOptions.First(); // Gán giá trị mặc định

            LoadDataCustomerCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await KhachHangDAL.Instance.GetAllcustomer());
                    dsKH = new ObservableCollection<KhachHangDTO>(data);
                    tatcaKH = new ObservableCollection<KhachHangDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                    return;
                }
            });
            GetCurrentWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            AddCustomerCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemKH w1 = new ThemKH();
                w1.ShowDialog();
            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            SaveNewCustomerCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewCustomer(p);
                IsSaving = false;
            });
            ViewCustomerCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (KHSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    ThongTinKH w1 = new ThongTinKH();
                    GetDataCustomer();
                    w1.ShowDialog();
                    IsLoading = false;
                }
            });
            DeleteCustomerCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Xoá sẽ mất hết dữ liệu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    IsLoading = true;
                    (bool trangthai, string message) = await KhachHangDAL.Instance.Deletecustomer(KHSelected.MaKH);
                    if (trangthai)
                    {
                        for (int i = 0; i < tatcaKH.Count; i++)
                        {
                            if (tatcaKH[i].MaKH == KHSelected.MaKH)
                            {
                                tatcaKH.Remove(tatcaKH[i]);
                                break;
                            }
                        }
                        for (int i = 0; i < dsKH.Count; i++)
                        {
                            if (dsKH[i].MaKH == KHSelected.MaKH)
                            {
                                dsKH.Remove(dsKH[i]);
                                break;
                            }
                        }
                    }
                    CustomControls.MyMessageBox.Show(message);
                }
            });
            UpdateCustomerCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaKhachHang w1 = new SuaKhachHang();
                GetDataCustomer();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveCustomerCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdateCustomer(p);
                IsSaving = false;
            });
            SearchData = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SearchText != null)
                {
                    if (SearchProperties == "Tất cả") dsKH = tatcaKH;
                    if (SearchProperties == "Tên")
                    {
                        dsKH = new ObservableCollection<KhachHangDTO>();
                        for (int i = 0; i < tatcaKH.Count; i++)
                        {
                            bool check = tatcaKH[i].TenKH.ToLower().Contains(SearchText.ToLower());
                            if (check) dsKH.Add(tatcaKH[i]);
                        }
                    }
                    if (SearchProperties == "Email")
                    {
                        dsKH = new ObservableCollection<KhachHangDTO>();
                        for (int i = 0; i < tatcaKH.Count; i++)
                        {
                            bool check = tatcaKH[i].email_KH.ToLower().Contains(SearchText.ToLower());
                            if (check) dsKH.Add(tatcaKH[i]);
                        }
                    }
                    if (SearchProperties == "SĐT")
                    {
                        dsKH = new ObservableCollection<KhachHangDTO>();
                        for (int i = 0; i < tatcaKH.Count; i++)
                        {
                            bool check = tatcaKH[i].SDT_KH.ToLower().Contains(SearchText.ToLower());
                            if (check) dsKH.Add(tatcaKH[i]);
                        }
                    }
                }
                else dsKH = tatcaKH;
            });
            ResetData = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                dsKH = new ObservableCollection<KhachHangDTO>(tatcaKH);
                SearchText = null;
            });
        }
        private void ClearData()
        {
            KhMaKH = null;
            KhEmail = null;
            KhGioiTinh = null;
            KhNgaySinh = null;
            KhSDT = null;
            KhTen = null;
        }
    }
}