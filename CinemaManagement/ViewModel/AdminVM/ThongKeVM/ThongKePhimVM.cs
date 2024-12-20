using CinemaManagement.DTOs;
using LiveCharts;
using LiveCharts.Wpf;
using CinemaManagement.Models.DAL;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class ThongKePhimVM: BaseViewModel
    {
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
                chartdata.Add(Top5Movie[i].DoanhThu/ 1000000);
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

    }
}
