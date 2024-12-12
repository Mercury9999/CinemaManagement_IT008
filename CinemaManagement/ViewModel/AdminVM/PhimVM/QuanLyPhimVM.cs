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
using CinemaManagement.View;
using CinemaManagement.CustomControls;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM : BaseViewModel
    {
        #region Icommand

        public ICommand ViewFilmCM { get; set; }
        public ICommand AddFilmCM { get; set; }
        public ICommand UpdateFilmCM { get; set; }
        public ICommand DeleteFilmCM { get; set; }

        public ICommand LoadDataFilmCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand GetQLPPageCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand SaveNewFilmCM { get; set; }
        public ICommand SaveFilmCM { get; set; }
        public ICommand GetCurrentWindow { get; set; }
        public ICommand GetQLPPage { get; set; }
        public ICommand SearchData { get; set; }
        #endregion
        #region Thuộc tính
        //Dùng lưu trữ dữ liệu của Phim

        private string _maPhim {  get; set; }
        public string MaPhim { get { return _maPhim; } set { _maPhim = value; OnPropertyChanged(); } }
        private string _tenPhim { get; set; }
        public string TenPhim { get { return _tenPhim; } set { _tenPhim = value; OnPropertyChanged(); } }
        private string _theLoai {  get; set; }
        public string TheLoai { get { return _theLoai; } set { _theLoai = value; OnPropertyChanged(); } }   
        private int? _thoiLuong { get; set; }
        public int? ThoiLuong { get { return _thoiLuong; } set { _thoiLuong = value; OnPropertyChanged(); } }
        private string _nuocSX {  get; set; }
        public string NuocSX { get { return _nuocSX; } set { _nuocSX = value; OnPropertyChanged(); } }  
        private string _ngayPH { get; set; }
        public string NgayPH { get { return _ngayPH; } set { _ngayPH = value; OnPropertyChanged(); } }
        private string _daoDien { get; set; }
        public string DaoDien { get { return _daoDien; } set { _daoDien = value; OnPropertyChanged(); } }
        private string _noiDung {  get; set; }
        public string NoiDung { get { return _noiDung; } set { _noiDung = value; OnPropertyChanged(); } }
        private string _gioiHanTuoi {  get; set; }
        public string GioiHanTuoi { get { return _gioiHanTuoi; } set { _gioiHanTuoi = value; OnPropertyChanged(); } }
        private byte[] _poster {  get; set; }
        public byte[] Poster { get { return _poster; } set { _poster = value; OnPropertyChanged(); } }
        private decimal _doanhThu { get; set; }
        public decimal DoanhThu { get { return _doanhThu; } set { _doanhThu = value; OnPropertyChanged(); } }

        //Biến khác
        private string _searchText {  get; set; }
        public string SearchText { get { return _searchText; } set { _searchText = value; OnPropertyChanged(); } }
        private DataGrid dataGrid {  get; set; }
        private Window QuanLyPhimPage { get; set; }
        private Window CurrentWindow { get; set; }
        private PhimDTO _phimselected {  get; set; }
        public PhimDTO PhimSelected { get { return _phimselected; } set { _phimselected = value; OnPropertyChanged(); } }
        private ObservableCollection<PhimDTO> _dsphim { get; set; }
        public ObservableCollection<PhimDTO> dsPhim { get { return _dsphim; } set { _dsphim = value; OnPropertyChanged(); } }
        private ObservableCollection<PhimDTO> _allFilm { get; set; }
        public ObservableCollection<PhimDTO> allFilm { get { return _allFilm; } set { _allFilm = value; OnPropertyChanged(); } }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        private ObservableCollection<string> _dstheloai { get; set; }
        public ObservableCollection<string> dsTheLoai { get { return _dstheloai; } set { _dstheloai = value; OnPropertyChanged(); } }
        #endregion
        public QuanLyPhimVM()
        {
            GetQLPPage = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                QuanLyPhimPage = p;
            });
            GetCurrentWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            LoadDataFilmCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await PhimDAL.Instance.GetAllMovie());
                    dsPhim = new ObservableCollection<PhimDTO>(data);
                    allFilm = new ObservableCollection<PhimDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                    return;
                }
            });
            UploadImageCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string imagePath = ImageUtility.ImagePath();
                if(imagePath != null) Poster = ImageUtility.ConvertImageToByteArray(imagePath);
            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddFilmCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemPhim w1 = new ThemPhim();
                w1.ShowDialog();
            });
            SaveNewFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewFilm(p);
                IsSaving = false;
            });
            DeleteFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Xoá sẽ mất hết dữ liệu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    IsLoading = true;
                    GetDataFilm();
                    int MaPhimDeleted = PhimSelected.MaPhim;
                    (bool trangthai, string message) = await PhimDAL.Instance.DeleteMovie(MaPhimDeleted);
                    if(trangthai)
                    {
                        for (int i = 0; i < dsPhim.Count; i++)
                        {
                            if (dsPhim[i].MaPhim == MaPhimDeleted)
                            {
                                dsPhim.Remove(dsPhim[i]);
                                break;
                            }
                        }
                    }
                    

                    CustomControls.MyMessageBox.Show(message);
                }
            });
            ViewFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (PhimSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    ThongTinPhim w1 = new ThongTinPhim();
                    GetDataFilm();
                    w1.ShowDialog();
                    IsLoading = false;
                }
            });
            UpdateFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaPhim w1 = new SuaPhim();
                GetDataFilm();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdateFilm(p);
                IsSaving = false;
            });
            dsTheLoai = new ObservableCollection<string>
            { 
                "Kinh dị",
                "Hành động",
                "Anime",
                "Giật gân",
                "Hài hước",
                "Lãng mạn",
            };
            SearchData = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SearchText != null)
                {
                    dsPhim = new ObservableCollection<PhimDTO>();
                    for (int i = 0; i < allFilm.Count; i++)
                    { 
                        bool check = allFilm[i].TenPhim.ToLower().Contains(SearchText.ToLower());
                        if (check)
                            dsPhim.Add(allFilm[i]);
                    }
                }
            });

        }
        private void ClearData()
        {
            TenPhim = null;
            TheLoai = null;
            ThoiLuong = null;
            NuocSX = null;
            NgayPH = null;
            DaoDien = null;
            NoiDung = null;
            Poster = null;
            GioiHanTuoi = null;
        }
    }
}
