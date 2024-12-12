using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using CinemaManagement.Models;
using CinemaManagement.CustomControls;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyKhachHangVM : BaseViewModel
    {
        // Phương thức kiểm tra các thuộc tính cần thiết có hợp lệ hay không
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(KhEmail)
                && !string.IsNullOrEmpty(KhSDT)
                && !string.IsNullOrEmpty(KhGioiTinh)
                && !string.IsNullOrEmpty(KhNgaySinh)
                && !string.IsNullOrEmpty(KhTen);
        }

        // Phương thức lưu khách hàng mới
        public async Task SaveNewCustomer(Window currentWindow)
        {
            if (CheckNonEmpty())
            {
                // Tạo đối tượng khách hàng DTO
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    TenKH = KhTen,
                    GioiTinh = KhGioiTinh,
                    SDT_KH = KhSDT,
                    email_KH = KhEmail,
                    NgaySinh = Convert.ToDateTime(KhNgaySinh),
                };

                // Gọi phương thức thêm khách hàng vào cơ sở dữ liệu
                (bool trangthai, string message, int newMaKH) = await KhachHangDAL.Instance.Addcustomer(khachHang);

                if (trangthai)
                {
                    MyMessageBox.Show("Thêm khách hàng thành công!");

                    // Cập nhật danh sách khách hàng sau khi thêm
                    khachHang.MaKH = newMaKH;
                    dsKH.Add(khachHang);
                    tatcaKH.Add(khachHang);
                    currentWindow.Close();
                }
                else
                {
                    // Thông báo lỗi khi thêm khách hàng không thành công
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống: " + message);
                }
            }
            else
            {
                // Thông báo nếu các trường thông tin chưa được nhập đầy đủ
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin khách hàng");
            }
        }
    }
}
