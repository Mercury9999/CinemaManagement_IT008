using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class BillService : BaseViewModel
    {
        private static BillService _instance;
        private static readonly object _lock = new object();
        private BillService()
        {

        }
        public static BillService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new BillService();
                    }
                    return _instance;
                }
            }
        }
        private ObservableCollection<SanPhamDTO> _dsSanPhamHD {  get; set; } = new ObservableCollection<SanPhamDTO>();
        public ObservableCollection<SanPhamDTO> dsSanPhamHD
        {
            get
            {
                return _dsSanPhamHD;
            }
            set
            {
                dsSanPhamHD = value; OnPropertyChanged();
            }
        }
        private ObservableCollection<BanVeDTO> _dsVeHD { get; set; } = new ObservableCollection<BanVeDTO>();
        public ObservableCollection<BanVeDTO> dsVeHD
        {
            get
            {
                return _dsVeHD;
            }
            set
            {
                dsVeHD = value; OnPropertyChanged();
            }
        }

    }

}
