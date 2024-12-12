using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveCharts;
using LiveCharts.Wpf;
using System.Threading.Tasks;
using CinemaManagement.DTOs;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CinemaManagement.View.AdminView.ThongKeView;
using CinemaManagement.View;


namespace CinemaManagement.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeVM :BaseViewModel
    {
        public ICommand GetNavigationFrameCM { get; set; }
        public ICommand ThongKePhimCM { get; set; }
        public ICommand ThongKeSanPhamCM { get; set; }
        public ICommand ThongKeDoanhThuCM { get; set; }
        public ICommand ThongKeKHCM { get; set; }
        public Frame NavigationFrame { get; set; }
        public ThongKeVM()
        {
            GetNavigationFrameCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                NavigationFrame = p;
            });

            ThongKePhimCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKePhimView());
            });

            ThongKeSanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKeSanPhamView());
            });

            ThongKeDoanhThuCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKeDoanhThuView());
            });

            ThongKeKHCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NavigationFrame.Navigate(new ThongKeKHView());
            });

        }

    }
}
