using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{ 
    public partial class HoaDonVM : BaseViewModel
    {
        public BillService billService = BillService.Instance;
        public KhachHangDTO KHMuaHang { get; set; }
        public int MaKHMuaHang { get; set; }
        public ICommand GetDataCustomer { get; set; }
    }
}
