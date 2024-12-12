using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM.AccountVM
{
    public class AccountService : BaseViewModel
    {
        private static AccountService _instance;
        private static readonly object _lock = new object();
        private AccountService() { }
        public static AccountService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountService();
                    }
                    return _instance;
                }
            }
        }
        private NhanVienDTO _currentaccount;
        public NhanVienDTO CurrentAccount
        {
            get { return _currentaccount; }
            set
            {
                _currentaccount = value;
                OnPropertyChanged();
            }
        }

    }

}
