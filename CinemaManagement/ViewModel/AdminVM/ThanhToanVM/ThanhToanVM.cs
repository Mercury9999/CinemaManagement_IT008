using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
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
        public ICommand GetDataCustomer { get; set; }
        private async Task SaveNewBill()
        {
            if (KHMuaHang is null)
            {
                MyMessageBox.Show("Chưa nhập thông tin khách hàng.");
//                return false;
            }
            HoaDonDTO hoadon = new HoaDonDTO()
            {
                MaKH = KHMuaHang.MaKH,
                MaNV = 1,
                ChietKhau = 0,
                GiamGia = 0,
                GiaTriHD = GiaTriHD,
                ThanhTien = ThanhTien
            };
            (bool trangthai, string messages, int newid) = await HoaDonDAL.Instance.AddNewBill(hoadon, dsSanPham, dsVe);
            if (trangthai)
            {
                MyMessageBox.Show(messages);
//                IsLoading = true;
                hoadon.SoHD = newid;
                dsHoaDon.Add(hoadon);
  //              IsLoading = false;
                CurrentWindow.Close();
            }
            else
            {
               MyMessageBox.Show("Lỗi hệ thống: " + messages);
            }
        }
    }
}
