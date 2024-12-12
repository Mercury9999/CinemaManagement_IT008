using CinemaManagement.DTOs;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CinemaManagement.CustomControls;
using LiveCharts.Wpf;

namespace CinemaManagement.ViewModel.AdminVM.ThongKeVM
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

        private ComboBoxItem _SelectedBestSellPeriod;
        public ComboBoxItem SelectedBestSellPeriod
        {
            get { return _SelectedBestSellPeriod; }
            set { _SelectedBestSellPeriod = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _SelectedBestSellTime;
        public ComboBoxItem SelectedBestSellTime
        {
            get { return _SelectedBestSellTime; }
            set { _SelectedBestSellTime = value; OnPropertyChanged(); }
        }

        public async Task ChangeSellPeriod()
        {
            if (SelectedBestSellPeriod != null)
            {
                switch (SelectedBestSellPeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedBestSellTime != null)
                            {
                                await LoadBestSellByYear();
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedBestSellTime != null)
                            {
                                await LoadBestSellByMonth();
                            }
                            return;
                        }
                }
            }
        }

        private async Task LoadBestSellByMonth()
        {
            //try
            //{
            //    Top5Movie = await Task.Run(() => ThongKe.Ins.GetTop5BestMovie(int.Parse(SelectedBestSellPeriod), int.Parse(SelectedBestSellTime)));
            //}
            //catch (Exception ex)
            //{
            //    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
            //    return;
            //}
        }

        private async Task LoadBestSellByYear()
        {
            //try
            //{
            //    Top5Movie = await Task.Run(() => ThongKe.Ins.GetTop5BestMovie(int.Parse(SelectedBestSellPeriod), int.Parse(SelectedBestSellTime)));
            //}
            //catch (Exception ex)
            //{
            //    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
            //    return;
            //}

            //List<decimal> chartData = new List<decimal>();
            //chartData.Add(0);
            //for (int i = 0; i < Top5Movie.Count; i++)
            //{
            //    chartData.Add(Top5Movie[i].DoanhThu / 1000000);
            //}

            //Top5MovieData = new SeriesCollection
            //{
            //    new ColumnSeries
            //    {
            //        Values = new ChartValues<decimal>(chartData),
            //        Title = "Doanh thu"
            //    },
            //};
        }
    }
}
