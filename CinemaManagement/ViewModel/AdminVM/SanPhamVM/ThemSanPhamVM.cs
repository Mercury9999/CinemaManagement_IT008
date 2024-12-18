using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLySanPhamVM : BaseViewModel
    {
        private async Task SaveNewProduct(Window w1)
        {
            if (CheckNonEmpty())
            {
                SanPhamDTO sanPham = new SanPhamDTO()
                {
                    TenSP = TenSP,
                    GiaSP = GiaSP ?? 0m,
                    LoaiSP = LoaiSP,
                    HinhAnhSP = HinhAnhSP,
                    SoLuong = 0
                };
                (bool trangthai, string messages, int newid) = await SanPhamDAL.Instance.AddProduct(sanPham);
                if (trangthai)
                {
                    CustomControls.MyMessageBox.Show(messages);
                    IsLoading = true;
                    sanPham.MaSP = newid;
                    dsSP.Add(sanPham);
                    SpForSearch = dsSP;
                    tatcaSP.Add(sanPham);
                    IsLoading = false;
                    CurrentWindow.Close();
                }
                else
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin sản phẩm");
            }
        }
    }
}
