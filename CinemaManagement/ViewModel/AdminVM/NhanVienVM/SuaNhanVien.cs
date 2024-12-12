using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CinemaManagement.View;
using System.Reflection;
using System.Windows;
using System.Collections.ObjectModel;
using CinemaManagement.Ultis;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyNhanVienVM : BaseViewModel
    {
        public async Task SaveUpdateStaff(Window w1)
        {
            if (CheckNonEmpty())
            {
                NhanVienDTO NhanVien = new NhanVienDTO
                {
                    MaNV = NVSelected.MaNV,
                    TenNV = TenNV,
                    acc_username = AccUsername,
                    acc_password = AccPassword,
                    SDT_NV = SDT,
                    email_NV = Email,
                    NgaySinh = Convert.ToDateTime(NgaySinh),
                    GioiTinh = GioiTinh,
                    ChucVu = ChucVu
                };
                (bool trangthai, string messages) = await NhanVienDAL.Instance.UpdateStaff(NhanVien);
                if (trangthai)
                {
                    MessageBox.Show(messages);
                    IsSaving = true;
                    var NhanVienUpdated = dsNV.FirstOrDefault(s => s.MaNV == NhanVien.MaNV);
                    if (NhanVienUpdated != null)
                    {
                        NhanVienUpdated = NhanVien;
                    }
                    IsSaving = false;
                    CurrentWindow.Close();
                }
                else
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin nhân viên");
            }
        }
    }
}
