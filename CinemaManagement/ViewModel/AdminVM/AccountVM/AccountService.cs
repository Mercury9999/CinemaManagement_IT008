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
