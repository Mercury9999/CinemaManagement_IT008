using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM.BillVM
{
    public class BillService : BaseViewModel
    {
        private static BillService _instance;
        private static readonly object _lock = new object();
        private BillService() { }
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
        private ObservableCollection<SanPhamDTO> _dsSanPhamHD {  get; set; }
        public ObservableCollection<SanPhamDTO> dsSanPhamHD
        {
            get
            {
                return _dsSanPhamHD;
            }
        }

    }

}
