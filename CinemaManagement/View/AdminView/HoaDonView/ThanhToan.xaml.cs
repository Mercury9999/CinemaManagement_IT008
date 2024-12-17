using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for ThanhToan.xaml
    /// </summary>
    public partial class ThanhToan : Window
    {
        //public class testData
        //{
        //    public string TenPhim { get; set; }
        //    public string NgayChieu { get; set; }
        //    public string ThoiGian { get; set; }
        //    public string Phong { get; set; }
        //    public string Ghe { get; set; }
        //    public string GiaVe { get; set; }
        //}

        //public ObservableCollection<testData> TestData { get; set; }
        public ThanhToan()
        {
            InitializeComponent();

            //TestData = new ObservableCollection<testData>()
            //{
            //    new testData { TenPhim = "Chu Meo con biet di", NgayChieu = "16/12/2024", ThoiGian = "9:00", Phong = "2", Ghe = "A01, A02, A03, A07", GiaVe = "100000" },
            //    new testData { TenPhim = "Aquaman", NgayChieu = "16/12/2024", ThoiGian = "9:00", Phong = "2", Ghe = "A01, A02, A03, A07", GiaVe = "100000" },
            //    new testData { TenPhim = "Doreamon va chu khung long", NgayChieu = "16/12/2024", ThoiGian = "9:00", Phong = "2", Ghe = "A01, A02, A03, A07", GiaVe = "100000" }
            //};
            //filmListBox.ItemsSource = TestData;
        }
    }
}
