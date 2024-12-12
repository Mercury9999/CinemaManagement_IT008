using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.ViewModel.AdminVM.AccountVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM.ThanhToanVM
{
    public partial class BillService : BaseViewModel
    {
        private static BillService _ins;

        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        private BillService()
        {

        }

        private ObservableCollection<SanPhamDTO> _listFood;
        public ObservableCollection<SanPhamDTO> listFood
        {
            get => _listFood;
            set
            {
                _listFood = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VeDTO> _listVe;
        public ObservableCollection<VeDTO> listVe
        {
            get => _listVe;
            set
            {
                _listVe = value;
                OnPropertyChanged();
            }
        }

    }
}
