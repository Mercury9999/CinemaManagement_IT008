using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CinemaManagement.DTOs;
using CinemaManagement.View;
using System.Windows;
using CinemaManagement.ViewModel.AdminVM;


using CinemaManagement.Models.DAL;
using CinemaManagement.Models;
using System.Collections.ObjectModel;
using CinemaManagement.Ultis;


namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM
    {
        public async Task SaveUpdateFilm(Window w1)
        {
            if (CheckNonEmpty())
            {
                PhimDTO Phim = new PhimDTO
                {
                    MaPhim = PhimSelected.MaPhim,
                    TenPhim = TenPhim,
                    ThoiLuong = Convert.ToInt32(ThoiLuong),
                    TheLoai = TheLoai,
                    NuocSX = NuocSX,
                    DaoDien = DaoDien,
                    NoiDung = NoiDung,
                    GioiHanTuoi = Convert.ToByte(GioiHanTuoi),
                    Poster = Poster,
                    NgayPH = Convert.ToDateTime(NgayPH)
                };
                (bool trangthai, string messages) = await PhimDAL.Instance.UpdateMovie(Phim);
                if (trangthai)
                {
                    MessageBox.Show(messages);
                    CurrentWindow.Close();
                    IsLoading = true;
                    for (int i = 0; i < dsPhim.Count; i++)
                    {
                        if (dsPhim[i].MaPhim == Phim.MaPhim)
                        {
                            dsPhim[i] = Phim;
                            break;
                        }
                    }
                    for (int i = 0; i < allFilm.Count; i++)
                    {
                        if (allFilm[i].MaPhim == Phim.MaPhim)
                        {
                            allFilm[i] = Phim;
                            break;
                        }
                    }
                    IsLoading = false;
                    return;
                }
                else
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin");
            }
        }
    }
}
