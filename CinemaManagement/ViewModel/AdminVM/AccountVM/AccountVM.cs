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
    public partial class AccountVM : BaseViewModel
    {

        #region biến lưu trữ dữ liệu
        public AccountService accountService { get; set; } = AccountService.Instance;
        public NhanVienDTO CurrentStaff
        {
            get { return accountService.CurrentAccount; }
        }
        #endregion
        #region 
        #endregion
        public AccountVM()
        {

        }
    }
}
