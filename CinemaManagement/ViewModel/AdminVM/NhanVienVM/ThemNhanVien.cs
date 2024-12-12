using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.Models;
using CinemaManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyNhanVienVM : BaseViewModel
    {
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(TenNV) && !string.IsNullOrEmpty(AccUsername)
            && !string.IsNullOrEmpty(AccPassword) && !string.IsNullOrEmpty(SDT)
            && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(NgaySinh)
            && !string.IsNullOrEmpty(GioiTinh) && !string.IsNullOrEmpty(ChucVu);
        }
        public async Task SaveNewStaff(Window w1)
        {
            if (CheckNonEmpty())
            {
                NhanVienDTO NhanVien = new NhanVienDTO
                {
                    TenNV = TenNV,
                    acc_username = AccUsername,
                    acc_password = AccPassword,
                    SDT_NV = SDT,
                    email_NV = Email,
                    NgaySinh = Convert.ToDateTime(NgaySinh),
                    GioiTinh = GioiTinh,
                    ChucVu = ChucVu
                };
                (bool trangthai, string messages, int newStaffId) = await NhanVienDAL.Instance.AddStaff(NhanVien);
                if (trangthai)
                {
                    MessageBox.Show(messages);
                    IsLoading = true;
                    NhanVien.MaNV = newStaffId;
                    dsNV.Add(NhanVien);
                    IsLoading = false;
                    CurrentWindow.Close();
                    return;
                }
                else
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                    return;
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin nhân viên");
            }
        }
    }
}
