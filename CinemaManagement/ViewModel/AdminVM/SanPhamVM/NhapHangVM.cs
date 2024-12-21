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
        private async Task ImportProduct(Window w1)
        {

            HDNhapHangDTO hdnhaphang = new HDNhapHangDTO()
            {
                MaSPNhap = (int)MaSP,
                DonGiaNhap = GiaNhap,
                SoLuong = SoLuongNhap,
            };
            (bool trangthai, string messages) = await SanPhamDAL.Instance.ImportProduct(hdnhaphang);
            if (trangthai)
            {
                CustomControls.MyMessageBox.Show(messages);
                IsLoading = true;
                var sanPham = dsSP.FirstOrDefault(sp => sp.MaSP == MaSP);
                if (sanPham != null)
                {
                    int newSoLuong = SoLuongNhap + sanPham.SoLuong;
                    var updatedSanPham = new SanPhamDTO
                    {
                        MaSP = sanPham.MaSP,
                        TenSP = sanPham.TenSP,
                        GiaSP = sanPham.GiaSP,
                        LoaiSP = sanPham.LoaiSP,
                        HinhAnhSP = sanPham.HinhAnhSP,
                        SoLuong = newSoLuong
                    };

                    int index = dsSP.IndexOf(sanPham);
                    dsSP[index] = updatedSanPham;
                    tatcaSP[index] = updatedSanPham;
                    SpForSearch[index] = updatedSanPham;
                }

                IsLoading = false;
                CurrentWindow.Close();
            }
            else
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
            }
        }


    }
}