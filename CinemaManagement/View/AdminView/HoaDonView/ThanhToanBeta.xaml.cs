using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaManagement.View.AdminView.HoaDonView
{
    /// <summary>
    /// Interaction logic for ThanhToanBeta.xaml
    /// </summary>
    public partial class ThanhToanBeta : Window
    {
        public class TestData
        {
            public string TenPhim { get; set; }
            public string NgayChieu { get; set; }
            public string GioChieu { get; set; }
            public string Phong { get; set; }
            public string Ghe { get; set; }
            public string LoaiChoNgoi { get; set; }
            public string GiaVe { get; set; }
        }
        public ObservableCollection<TestData> data { get; set; }

        public ThanhToanBeta()
        {
            InitializeComponent();
            DataContext = this;

            data = new ObservableCollection<TestData>
            {
                new TestData { TenPhim = "Meo con", NgayChieu = "16/12/2024", GioChieu = "9:00", Phong = "2", Ghe = "A01, A02, A03", LoaiChoNgoi = "VIP", GiaVe = "100000"},
                new TestData { TenPhim = "Nguoi cha cam", NgayChieu = "10/11/2024", GioChieu = "12:00", Phong = "3", Ghe = "A01", LoaiChoNgoi = "Normal", GiaVe = "70000"},
                new TestData { TenPhim = "Kimetsu No Yaiba", NgayChieu = "20/12/2024", GioChieu = "14:00", Phong = "3", Ghe = "A03, B01, B06", LoaiChoNgoi = "VIP", GiaVe = "70000"}
            };
            
            filmListBox.ItemsSource = data;

        }
    }
}
