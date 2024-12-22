using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using LiveCharts.Wpf;
using LiveCharts;


namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class ThongKeVM :BaseViewModel, INotifyPropertyChanged
    {
        public ICommand GetNavigationFrameCM { get; set; }
        public ICommand ThongKePhimCM { get; set; }
        public ICommand ThongKeSanPhamCM { get; set; }
        public ICommand ThongKeDoanhThuCM { get; set; }
        public ICommand ThongKeKHCM { get; set; }
        public Frame NavigationFrame { get; set; }

        private SeriesCollection _Top5CustomerData;
        public SeriesCollection Top5CustomerData
        {
            get { return _Top5CustomerData; }
            set { _Top5CustomerData = value; OnPropertyChanged(); }

        }

        //regionKH
        private List<KhachHangDTO> top5Customer;
        public List<KhachHangDTO> Top5Customer
        {
            get { return top5Customer; }
            set { top5Customer = value; OnPropertyChanged(); }
        }

        private async Task LoadBestExpenseCustomer()
        {
            try
            {
                Top5Customer = await Task.Run(() => ThongKeDAL.Instance.GetTop5CustomerByBenefit());
            }
            catch (Exception ex)
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                return;
            }

            List<decimal> chartdata = new List<decimal>();
            chartdata.Add(0);
            for (int i = 0; i < Top5Customer.Count; i++)
            {
                chartdata.Add(Top5Customer[i].HDTichLuy / 1000000);
            }

            Top5CustomerData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<decimal>(chartdata),
                    Title = "Doanh thu"
                }
            };
        }
        //endregion

        //region Movie
        private SeriesCollection _Top5MovieData;
        public SeriesCollection Top5MovieData
        {
            get { return _Top5MovieData; }
            set { _Top5MovieData = value; OnPropertyChanged(); }

        }

        private List<PhimDTO> top5Movie;
        public List<PhimDTO> Top5Movie
        {
            get { return top5Movie; }
            set { top5Movie = value; OnPropertyChanged(); }
        }

        private async Task LoadBestSellMovie()
        {
            try
            {
                Top5Movie = await Task.Run(() => ThongKeDAL.Instance.GetTop5FilmByBenefit());
                CustomControls.MyMessageBox.Show(Top5Movie[0].DoanhThuStr);
            }
            catch (Exception ex)
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                return;
            }

            List<decimal> chartdata = new List<decimal>();
            chartdata.Add(0);
            for (int i = 0; i < Top5Movie.Count; i++)
            {
                chartdata.Add(Top5Movie[i].DoanhThu / 1000000);
            }

            Top5MovieData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<decimal>(chartdata),
                    Title = "Doanh thu"
                }
            };
        }
        //endregion

        //region SP
        private SeriesCollection _Top5ProductData;
        public SeriesCollection Top5ProductData
        {
            get { return _Top5ProductData; }
            set { _Top5ProductData = value; OnPropertyChanged(); }

        }

        private List<SanPhamDTO> top5Product;
        public List<SanPhamDTO> Top5Product
        {
            get { return top5Product; }
            set { top5Product = value; OnPropertyChanged(); }
        }

        private async Task LoadBestSellProduct()
        {
            try
            {
                Top5Product = await Task.Run(() => ThongKeDAL.Instance.GetTop5ProductBySales());
            }
            catch (Exception ex)
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                return;
            }

            List<decimal> chartdata = new List<decimal>();
            chartdata.Add(0);
            for (int i = 0; i < Top5Product.Count; i++)
            {
                chartdata.Add(Top5Product[i].SoLuong);
            }

            Top5ProductData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<decimal>(chartdata),
                    Title = "Doanh thu"
                }
            };
        }
        //end region
        public ThongKeVM()
        {
            GetNavigationFrameCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                NavigationFrame = p;
            });

            ThongKePhimCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                NavigationFrame.Navigate(new ThongKePhimView());
//                await LoadBestSellMovie();
            });

            ThongKeSanPhamCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                NavigationFrame.Navigate(new ThongKeSanPhamView());
                await LoadBestSellProduct();
            });

            ThongKeDoanhThuCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKeDoanhThuView());
            });

            ThongKeKHCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                NavigationFrame.Navigate(new ThongKeKHView());
                await LoadBestExpenseCustomer();
            });

        }
    }
}
