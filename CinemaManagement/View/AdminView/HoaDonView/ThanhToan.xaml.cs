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
        public class testData
        {
            public string TenPhim { get; set; }
            public string NgayChieu { get; set; }
            public string ThoiGian { get; set; }
            public string Phong { get; set; }
            public string Ghe { get; set; }
            public string GiaVe { get; set; }
        }

        public ObservableCollection<testData> TestData { get; set; }
        public ThanhToan()
        {
            InitializeComponent();
        }
    }
}
