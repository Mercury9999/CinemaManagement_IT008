using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using CinemaManagement.View.AdminView.KhachHangView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyNhapHangVM : BaseViewModel
    {
        private ObservableCollection<HDNhapHangDTO> _dsNhapHang;
        public ObservableCollection<HDNhapHangDTO> dsNhapHang {  get { return _dsNhapHang; } set { _dsNhapHang = value; OnPropertyChanged(); } }

        public QuanLyNhapHangVM()
        {
            //LoadDSNhapHang();
        }

        private async void LoadDSNhapHang()
        {
            //var data = await NhapHangDAL.GetAllHDNhapHang();
            //dsNhapHang = new ObservableCollection<HDNhapHangDTO>(data);
        }
    }
}