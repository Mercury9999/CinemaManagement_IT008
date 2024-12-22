using CinemaManagement.DTOs;
using LiveCharts;
using LiveCharts.Wpf;
using CinemaManagement.Models.DAL;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class ThongKeSanPhamVM: BaseViewModel
    {
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
    }
}