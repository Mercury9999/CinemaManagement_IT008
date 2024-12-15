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
        private List<SanPhamDTO> _dsSanPhamHD {  get; set; } = new List<SanPhamDTO>();
        public List<SanPhamDTO> dsSanPhamHD
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
        private List<BanVeDTO> _dsVeHD { get; set; } = new List<BanVeDTO>();
        public List<BanVeDTO> dsVeHD
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
