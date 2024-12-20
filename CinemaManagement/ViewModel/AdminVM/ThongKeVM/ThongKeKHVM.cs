using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using LiveCharts;
using LiveCharts.Wpf;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class ThongKeKHVM : BaseViewModel
    {
        private SeriesCollection _Top5CustomerData;
        public SeriesCollection Top5CustomerData
        {
            get { return _Top5CustomerData; }
            set { _Top5CustomerData = value; OnPropertyChanged(); }

        }

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

    }
}
