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
using CinemaManagement.CustomControls;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyKhachHangVM : BaseViewModel
    {
        public async Task SaveUpdateCustomer(Window w1)
        {
            if (CheckNonEmpty())
            {
                KhachHangDTO KhachHang = new KhachHangDTO
                {
                   MaKH = KHSelected.MaKH,
                   TenKH = KhTen,
                   GioiTinh = KhGioiTinh,
                   SDT_KH   = KhSDT,
                   email_KH = KhEmail,
                   NgaySinh = Convert.ToDateTime(KhNgaySinh),
                   HDTichLuy = Convert.ToDecimal(KhChiTieu),
                   NgayDK = Convert.ToDateTime(KhNgayDK),
                };
                (bool trangthai, string messages) = await KhachHangDAL.Instance.Updatecustomer(KhachHang);
                if (trangthai)
                {
                    MyMessageBox.Show(messages);
                    IsSaving = true;
                    for(int i = 0; i < dsKH.Count; i++)
                    {
                        if (dsKH[i].MaKH == KhachHang.MaKH) dsKH[i] = KhachHang;
                    }
                    for (int i = 0; i < tatcaKH.Count; i++)
                    {
                        if (tatcaKH[i].MaKH == KhachHang.MaKH) tatcaKH[i] = KhachHang;
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
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin khach hang");
            }
        }
    }
}
