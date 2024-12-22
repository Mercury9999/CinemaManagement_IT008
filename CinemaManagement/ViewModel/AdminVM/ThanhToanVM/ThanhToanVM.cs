using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{ 
    public partial class HoaDonVM : BaseViewModel
    {
        public BillService billService = BillService.Instance;
        public KhachHangDTO KHMuaHang { get; set; }
        public int MaKHMuaHang { get; set; }
        public ICommand GetDataCustomerCM { get; set; }
        private async Task SaveNewBill()
        {
            if (KHMuaHang is null)
            {
                MyMessageBox.Show("Chưa nhập thông tin khách hàng.");
                return;
            }
            HoaDonDTO hoadon = new HoaDonDTO()
            {
                MaKH = KHMuaHang.MaKH,
                MaNV = accountService.CurrentAccount.MaNV,
                ChietKhau = 0,
                GiamGia = 0,
                GiaTriHD = GiaTriHD,
                ThanhTien = ThanhTien
            };
            (bool trangthai, string messages, int newid) = await HoaDonDAL.Instance.AddNewBill(hoadon, dsSanPham, dsVe);
            if (trangthai)
            {
                MyMessageBox.Show(messages);
                dsHoaDon.Clear();
                var data = await HoaDonDAL.Instance.GetAllBill();
                dsHoaDon = new ObservableCollection<HoaDonDTO>(data);
                CurrentWindow.Close();
            }
            else
            {
               MyMessageBox.Show("Lỗi hệ thống: " + messages);
            }
        }
    }
}
