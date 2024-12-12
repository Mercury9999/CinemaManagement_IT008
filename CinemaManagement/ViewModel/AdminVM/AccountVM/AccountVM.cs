using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM.AccountVM
{ 
    public class AccountVM : BaseViewModel
    {

        #region biến lưu trữ dữ liệu
        public AccountService accountService { get; set; } = AccountService.Instance;
        public NhanVienDTO CurrentStaff
        {
            get { return accountService.CurrentAccount; }
        }
        #endregion
        #region Icommand
        #endregion
        public AccountVM()
        {

        }
    }
}
