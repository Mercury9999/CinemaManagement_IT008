using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLySanPhamVM : BaseViewModel
    {
        private async Task SaveUpdatedProduct(Window w1)
        {
            if (CheckNonEmpty())
            {
                try
                {
                    SanPhamDTO sanPham = new SanPhamDTO()
                    {
                        MaSP = MaSP ?? 0,
                        TenSP = TenSP,
                        GiaSP = GiaSP ?? 0m,
                        LoaiSP = LoaiSP,
                        HinhAnhSP = HinhAnhSP,
                        SoLuong = SoLuong
                    };
                    (bool trangthai, string messages) = await SanPhamDAL.Instance.UpdateProduct(sanPham);
                    if (trangthai)
                    {
                        MessageBox.Show(messages);
                        IsLoading = true;
                        for (int i = 0; i < dsSP.Count; i++)
                        if (dsSP[i].MaSP == sanPham.MaSP)
                        {
                            dsSP[i] = sanPham;
                            break;
                        }
                        SpForSearch = dsSP;
                        for (int i = 0; i < tatcaSP.Count; i++)
                        if (tatcaSP[i].MaSP == sanPham.MaSP)
                        {
                            tatcaSP[i] = sanPham;
                            break;
                        }
                        IsLoading = false;
                        CurrentWindow.Close();
                    }
                    else
                    {
                        CustomControls.MyMessageBox.Show("Lỗi hệ thống: " + messages);
                    }
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin sản phẩm");
            }
        }
    }
}
