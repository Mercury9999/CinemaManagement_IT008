using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using CinemaManagement.View;
using System.Reflection;
using CinemaManagement.Models;
using System.Runtime.Remoting.Messaging;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLySuatChieuVM : BaseViewModel
    {
        private int _masc { get; set; }
        public int MaSC
        {
            get { return _masc; }
            set { _masc = value; OnPropertyChanged(); }
        }
        private int _maphim {  get; set; }
        public int MaPhim
        {
            get { return _maphim; }
            set { _maphim = value; OnPropertyChanged() ; }
        }
        public int SoPhongChieu { get; set; }

        private int _soPhong {  get; set; }
        public int SoPhong
        {
            get { return _soPhong; }
            set { _soPhong = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDate { get; set; }
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public string BatDauStr { get; set; }
        public string KetThucStr { get; set; }
        public decimal GiaVe { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        private ObservableCollection<SuatChieuDTO> _dsSuatChieu;
        public ObservableCollection<SuatChieuDTO> dsSuatChieu
        {

            get { return _dsSuatChieu; }
            set { _dsSuatChieu = value; OnPropertyChanged(); }
        }
        public ICommand LoadDataShowTimeCM { get; set; }
        public ICommand ChangeRoomCM { get; set; }
        public ICommand ChangeDateTimeCM { get; set; }
        public ICommand AddShowtimeCM {  get; set; }
        public ICommand CloseWindowCM {  get; set; }
        public ICommand AddNewShowtimeCM { get; set; }
        public QuanLySuatChieuVM()
        {
            PhongChieuDAL.Instance.GenerateChair();
            SelectedDate = DateTime.Now;
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                p.Close();
            });
            ChangeRoomCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                GetShowTimeData();
            });
            ChangeDateTimeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                GetShowTimeData();
            });
            AddShowtimeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ClearData();
                Window w1 = new ThemSuatChieu();
                w1.ShowDialog();
            });
            AddNewShowtimeCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (CheckNonEmpty())
                {
                    BatDau = ConvertDateTimeToString(BatDauStr);
                    KetThuc = ConvertDateTimeToString(KetThucStr);
                    PhimDTO phim = PhimDAL.Instance.GetMovieById(MaPhim);
                    PhimDTO tmpphim = new PhimDTO()
                    {
                        TenPhim = phim.TenPhim,
                        TheLoai = phim.TheLoai,
                        ThoiLuong = phim.ThoiLuong
                    };
                    SuatChieuDTO suatChieu = new SuatChieuDTO()
                    {
                        MaPhim = MaPhim,
                        BatDau = BatDau,
                        KetThuc = KetThuc,
                        GiaVe = GiaVe,
                        SoPhongChieu = SoPhongChieu,
                        Phim = tmpphim
                    };
                    (bool trangthai, string messages, int newId) = await SuatChieuDAL.Instance.AddShowTime(suatChieu);
                    if (trangthai)
                    {
                        MessageBox.Show(messages);
                        IsLoading = true;
                        suatChieu.MaSC = newId;
                        dsSuatChieu.Add(suatChieu);
                        p.Close();
                        IsLoading = false;
                        return;
                    }
                    else
                    {
                        CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                        return;
                    }
                }
                else
                {
                    CustomControls.MyMessageBox.Show("Thiếu thông tin");
                    return;
                }
            });
        }
        public async Task GetShowTimeData()
        {
            try
            {
                SelectedDate.CompareTo(DateTime.Now);
                var data = await SuatChieuDAL.Instance.GetShowTimeByRoom(SoPhong, SelectedDate);
                dsSuatChieu = new ObservableCollection<SuatChieuDTO>(data);
                IsLoading = false;
            }
            catch (Exception ex)
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống: " + ex.Message );
            }
        }
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(BatDauStr) && !string.IsNullOrEmpty(KetThucStr)
            && MaPhim != null && GiaVe.ToString() != null;
        }
        public void ClearData()
        {
            MaSC = 0;
            MaPhim = 0;
            SoPhongChieu = SoPhong;
            BatDau = DateTime.Now;
            KetThuc = DateTime.Now;
            BatDauStr = null;
            KetThucStr = null;
            GiaVe = 0;
        }
        public static DateTime ConvertDateTimeToString(string timeString)
        {
            DateTime currentDate = DateTime.Now;
            string[] timeParts = timeString.Split(':');
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            DateTime combinedDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hours, minutes, 0);
            return combinedDateTime;
        }
    }
}
