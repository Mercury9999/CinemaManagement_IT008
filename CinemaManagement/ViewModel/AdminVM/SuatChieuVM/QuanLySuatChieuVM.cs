using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CinemaManagement.View;
using CinemaManagement.CustomControls;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLySuatChieuVM : BaseViewModel
    {
        BillService billService = BillService.Instance;
        private string _masc { get; set; }
        public string MaSC
        {
            get { return _masc; }
            set { _masc = value; OnPropertyChanged(); }
        }
        private string _maphim;
        public string MaPhim
        {
            get { return _maphim; }
            set { _maphim = value; OnPropertyChanged(); }
        }
        private int _soPhong {  get; set; }
        public int SoPhong
        {
            get { return _soPhong; }
            set { _soPhong = value; OnPropertyChanged(); }
        }
        DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        private int _selectedRoom { get; set; }
        public int SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom = value; OnPropertyChanged(); }
        }
        private BanVeDTO _selectedTicket { get; set; }
        public BanVeDTO SelectedTicket
        {
            get { return _selectedTicket; }
            set { _selectedTicket = value; OnPropertyChanged(); }
        }
        private SuatChieuDTO _selectedShowtime {  get; set; }
        public SuatChieuDTO SelectedShowtime
        {
            get { return _selectedShowtime; }
            set { _selectedShowtime = value; OnPropertyChanged(); }
        }
        public PhimDTO Phim { get; set; }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public string BatDauStr { get; set; }
        public string KetThucStr { get; set; }
        public string NgayChieu { get; set; }
        public string GiaVe { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        private ObservableCollection<SuatChieuDTO> _dsSuatChieu = new ObservableCollection<SuatChieuDTO>();
        public ObservableCollection<SuatChieuDTO> dsSuatChieu
        {

            get { return _dsSuatChieu; }
            set { _dsSuatChieu = value; OnPropertyChanged(); }
        }
        private ObservableCollection<PhongChieuDTO> _dsPhong = new ObservableCollection<PhongChieuDTO>();
        public ObservableCollection<PhongChieuDTO> dsPhong
        {

            get { return _dsPhong; }
            set { _dsPhong = value; OnPropertyChanged(); }
        }
        private ObservableCollection<BanVeDTO> _dsBanVe = new ObservableCollection<BanVeDTO>();
        public ObservableCollection<BanVeDTO> dsBanVe
        {

            get { return _dsBanVe; }
            set { _dsBanVe = value; OnPropertyChanged(); }
        }
        public ICommand LoadDataShowTimeCM { get; set; }
        public ICommand ChangeRoomCM { get; set; }
        public ICommand ChangeDateTimeCM { get; set; }
        public ICommand AddShowtimeCM {  get; set; }
        public ICommand CloseWindowCM {  get; set; }
        public ICommand AddNewShowtimeCM { get; set; }
        public ICommand AddNewRoomCM {  get; set; }
        public ICommand GetRoomListCM {  get; set; }
        public ICommand ChangeInfoCM { get; set; }
        public ICommand ViewShowtimeCM { get; set; }
        public ICommand OrderTicketCM { get; set; }
        public QuanLySuatChieuVM()
        {
            SelectedDate = DateTime.Now;
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                p.Close();
            });
            GetRoomListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await PhongChieuDAL.Instance.GetAllRoom());
                    dsPhong = new ObservableCollection<PhongChieuDTO>(data);
                    if (dsPhong.Any()) SelectedRoom = dsPhong[0].SoPhong;
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    MyMessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            });
            AddNewRoomCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                PhongChieuDTO Phong = new PhongChieuDTO();
                (bool trangthai, string message, Phong) = await PhongChieuDAL.Instance.AddNewRoom();
                if (trangthai == true)
                {
                    dsPhong.Add(new PhongChieuDTO
                    {
                        SoPhong = Phong.SoPhong,
                        SLGhe = Phong.SLGhe,
                        Ghe = Phong.Ghe,
                    });
                    MyMessageBox.Show("Thêm phòng thành công");
                }
                else
                {
                    MyMessageBox.Show(message);
                }
            });
            ChangeInfoCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                GetNewData();
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
                    PhimDTO phim = PhimDAL.Instance.GetMovieById(Int32.Parse(MaPhim));
                    PhimDTO tmpphim = new PhimDTO()
                    {
                        TenPhim = phim.TenPhim,
                        TheLoai = phim.TheLoai,
                        ThoiLuong = phim.ThoiLuong,
                        Poster = phim.Poster
                    };
                    SuatChieuDTO suatChieu = new SuatChieuDTO()
                    {
                        MaPhim = Int32.Parse(MaPhim),
                        BatDau = BatDau,
                        KetThuc = KetThuc,
                        GiaVe = Decimal.Parse(MaPhim),
                        SoPhongChieu = SelectedRoom,
                        Phim = tmpphim
                    };
                    (bool trangthai, string messages, int newId) = await SuatChieuDAL.Instance.AddShowTime(suatChieu);
                    if (trangthai)
                    {
                        IsLoading = true;
                        suatChieu.MaSC = newId;
                        dsSuatChieu.Add(suatChieu);
                        MessageBox.Show(messages);
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
            ViewShowtimeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ClearData();
                if (SelectedShowtime != null)
                {
                    GetShowtimeData();
                    Window w1 = new ThongTinSuatChieu();
                    w1.ShowDialog();
                }
            });
            OrderTicketCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                billService.dsVeHD.Add(SelectedTicket);
                MyMessageBox.Show("Đã thêm vé vào hoá đơn");
            });
        }
        public async Task GetNewData()
        {
            if (!string.IsNullOrEmpty(SelectedRoom.ToString()))
            {
                IsLoading = true;
                await Task.Delay(20);
                dsBanVe.Clear();
                dsSuatChieu.Clear();
                var data = await SuatChieuDAL.Instance.GetShowTimeByRoom(SelectedRoom, SelectedDate);
                dsSuatChieu = new ObservableCollection<SuatChieuDTO>(data);
                IsLoading = false; 
            }
        }
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(BatDauStr) && !string.IsNullOrEmpty(KetThucStr)
            && MaPhim.ToString() != null && GiaVe.ToString() != null;
        }
        public void ClearData()
        {
            MaSC = null;
            MaPhim = null;
            SoPhong = SelectedRoom;
            BatDau = DateTime.Now;
            KetThuc = DateTime.Now;
            BatDauStr = null;
            KetThucStr = null;
            GiaVe = null;
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
        public async void GetShowtimeData()
        {
            MaSC = SelectedShowtime.MaSC.ToString();
            Phim = SelectedShowtime.Phim;
            GiaVe = SelectedShowtime.GiaVe.ToString();
            NgayChieu = SelectedShowtime.GioChieuStr;
            SoPhong = SelectedRoom;
            var data = await BanVeDAL.Instance.GetTicketSell(Int32.Parse(MaSC));
            dsBanVe = new ObservableCollection<BanVeDTO>(data);
        }
    }
}
