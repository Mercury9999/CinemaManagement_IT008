using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.Ultis;
using CinemaManagement.View;
using CinemaManagement.View.AdminView.SanPhamView;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLySanPhamVM : BaseViewModel
    {
        BillService billService = BillService.Instance;
        #region Icommand
        public ICommand ViewProductCM { get; set; }
        public ICommand AddProductCM { get; set; }
        public ICommand UpdateProductCM { get; set; }
        public ICommand DeleteProductCM { get; set; }

        public ICommand LoadDataProductCM { get; set; }
        public ICommand CloseCM { get; set; }
        public ICommand GetQLSPWindowCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand SaveNewProductCM { get; set; }
        public ICommand SaveProductCM { get; set; }
        public ICommand GetCurrentWindowCM { get; set; }
        public ICommand NameSearchProductCM { get; set; }
        public ICommand TypeSearchProductCM { get; set; }
        public ICommand SearchData {  get; set; }
        public ICommand OpenOrderProductCM { get; set; }
        public ICommand OrderProductCM { get; set; }
        public ICommand ImportProductCM { get; set; }
        public ICommand SaveImportProductCM { get; set; }


        #endregion
        #region thuộc tính lưu dữ liệu
        private int? _masp {  get; set; }
        public int? MaSP { get { return _masp; } set { _masp = value; OnPropertyChanged(); } }
        private string _maspstr { get; set; }
        public string MaSPStr { get { return _maspstr; } set { _maspstr = value; OnPropertyChanged(); } }
        private string _loaisp {  get; set; }
        public string LoaiSP { get { return _loaisp; } set { _loaisp = value; OnPropertyChanged(); } }
        private string _tensp { get; set; }
        public string TenSP { get { return _tensp; } set { _tensp = value; OnPropertyChanged(); } }
        private int _soluong { get; set; }
        public int SoLuong { get { return _soluong; } set { _soluong = value; OnPropertyChanged(); } }
        private decimal? _giasp { get; set; }
        public decimal? GiaSP { get { return _giasp; } set { _giasp = value; OnPropertyChanged(); } }
        private byte[] _hinhanhsp { get; set; }
        public byte[] HinhAnhSP { get { return _hinhanhsp; } set { _hinhanhsp = value; OnPropertyChanged(); } }
        #endregion
        #region biến khác
        private string _searchText {  get; set; }
        public string SearchText { get { return _searchText; } set {_searchText = value; OnPropertyChanged(); } }
        private Window CurrentWindow { get; set; }
        private SanPhamDTO _SPSelected { get; set; }
        public SanPhamDTO SPSelected { get { return _SPSelected; } set { _SPSelected = value; OnPropertyChanged(); } }
        private ObservableCollection<SanPhamDTO> _dsSP { get; set; }
        public ObservableCollection<SanPhamDTO> dsSP { get { return _dsSP; } set { _dsSP = value; OnPropertyChanged(); } }
        private ObservableCollection<SanPhamDTO> _tatcaSP { get; set; }
        public ObservableCollection<SanPhamDTO> tatcaSP { get { return _tatcaSP; } set { _tatcaSP = value; OnPropertyChanged(); } }
        private ObservableCollection<SanPhamDTO> _SpForSearch { get; set; }
        public ObservableCollection<SanPhamDTO> SpForSearch { get { return _SpForSearch; } set { _SpForSearch = value; OnPropertyChanged(); } }
        public ObservableCollection<string> dsLoaiSP { get; set; }
        public ObservableCollection<string> dsLoaiSPTimKiem { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        public string TimLoaiSP { get; set; }
        public string TimTenSP {  get; set; }
        public int SoLuongMua {  get; set; }
        public int SoLuongNhap { get; set; }
        public decimal GiaNhap { get; set; }



        #endregion
        public QuanLySanPhamVM()
        {
            GetCurrentWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            CloseCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            LoadDataProductCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await SanPhamDAL.Instance.GetAllProduct());
                    tatcaSP = new ObservableCollection<SanPhamDTO>(data);
                    dsSP = new ObservableCollection<SanPhamDTO>(data);
                    SpForSearch = dsSP;
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    MyMessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            });
            UploadImageCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string imagePath = ImageUtility.ImagePath();
                if (imagePath != null) HinhAnhSP = ImageUtility.ConvertImageToByteArray(imagePath);
            });
            AddProductCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemSanPham w1 = new ThemSanPham();
                 w1.ShowDialog();
            });
            SaveNewProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewProduct(p);
                IsSaving = false;
            });
            DeleteProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (SPSelected != null)
                {
                    SanPhamDTO SelectedItem = SPSelected;
                    if (SelectedItem.SoLuong > 0)
                    {
                        MyMessageBox.Show("Sản phẩm còn hàng tồn trong kho, không thể xoá");
                        return;
                    }
                    bool result = CustomControls.MyMessageBox.ShowYesNo("Xác nhận xoá sản phẩm?");
                    if (result == true)
                    {
                        IsLoading = true;
                        (bool trangthai, string message) = await SanPhamDAL.Instance.DeleteProduct(SelectedItem.MaSP);
                        if (trangthai)
                        {
                            for (int i = 0; i < dsSP.Count; i++)
                            {
                                if (dsSP[i].MaSP == SelectedItem.MaSP)
                                {
                                    dsSP.Remove(dsSP[i]);
                                    break;
                                }
                            }
                            for (int i = 0; i < tatcaSP.Count; i++)
                            {
                                if (tatcaSP[i].MaSP == SelectedItem.MaSP)
                                {
                                    tatcaSP.Remove(tatcaSP[i]);
                                    break;
                                }
                            }
                            SpForSearch = dsSP;
                        }
                        CustomControls.MyMessageBox.Show(message);
                        IsLoading = false;
                    }
                }
            });
            ViewProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (SPSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    ThongTinSanPham w1 = new ThongTinSanPham();
                    GetDataProduct();
                    w1.ShowDialog();
                    IsLoading = false;
                }
            });
            UpdateProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaSanPham w1 = new SuaSanPham();
                GetDataProduct();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdatedProduct(p);
                IsSaving = false;
            });
            ImportProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {

                GetDataProduct();
                NhapHang w1 = new NhapHang();
                w1.ShowDialog();

            });
            SaveImportProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await ImportProduct(p);
                IsSaving = false;
            });
            dsLoaiSP = new ObservableCollection<string>
            {
                "Thức ăn",
                "Nước uống",
                "Đồ chơi",
                "Khăn giấy"
            };
            dsLoaiSPTimKiem = new ObservableCollection<string>
            {
                "Tất cả",
                "Thức ăn",
                "Nước uống",
                "Đồ chơi",
                "Khăn giấy"
            };
            TypeSearchProductCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                try
                {
                    dsSP = new ObservableCollection<SanPhamDTO>();
                    if (TimLoaiSP == "Tất cả")
                    {
                        dsSP = new ObservableCollection<SanPhamDTO>(tatcaSP);
                    }
                    else
                    {
                        for (int i = 0; i < tatcaSP.Count; i++)
                        {
                            if (tatcaSP[i].LoaiSP == TimLoaiSP)
                            {
                                dsSP.Add(tatcaSP[i]);
                            }
                        }
                    }
                    SpForSearch = dsSP;
                }
                catch (System.Data.Entity.Core.EntityException e)
                {
                    Console.WriteLine(e);
                    MyMessageBox.Show("Lỗi: Mất kết nối cơ sở dữ liệu");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    MyMessageBox.Show("Lỗi hệ thống");
                }

            });
            SearchData = new RelayCommand<object>((p) => { return true;}, (p) =>
            {
              dsSP = new ObservableCollection<SanPhamDTO>();
              for(int i = 0;i < SpForSearch.Count; i++)
              {
                  bool check = SpForSearch[i].TenSP.ToLower().Contains(SearchText.ToLower());
                  if(check) dsSP.Add(SpForSearch[i]);
              }
            });
            OpenOrderProductCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SoLuongMua = 0;
                MaSP = SPSelected.MaSP;
                TenSP = SPSelected.TenSP;
                GiaSP = SPSelected.GiaSP;
                Window w1 = new MuaHang();
                MaSP = SPSelected.MaSP;
                w1.ShowDialog();
            });
            OrderProductCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                billService.dsSanPhamHD.Add(new SanPhamDTO
                {
                    MaSP = MaSP ?? 0,
                    TenSP = TenSP,
                    GiaSP = GiaSP ?? 0,
                    SoLuong = SoLuongMua
                });
                CurrentWindow.Close();
                MyMessageBox.Show("Đã thêm sản phẩm vào hoá đơn");
            });
        }
        private void ClearData()
        {
            MaSP = null;
            TenSP = null;
            GiaSP = null;
            HinhAnhSP = null;
            SoLuong = 0;
            LoaiSP = null;
        }
        private void GetDataProduct()
        {
            if(SPSelected != null)
            {
                MaSPStr = SPSelected.MaSPStr;
                MaSP = SPSelected.MaSP;
                TenSP = SPSelected.TenSP;
                GiaSP = SPSelected.GiaSP;
                HinhAnhSP = SPSelected.HinhAnhSP;
                SoLuong = SPSelected.SoLuong;
                LoaiSP = SPSelected.LoaiSP;
            }
        }
        private bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(TenSP)
            && !(GiaSP is null) && !(HinhAnhSP is null) && !string.IsNullOrEmpty(LoaiSP);
        }
    }
}
