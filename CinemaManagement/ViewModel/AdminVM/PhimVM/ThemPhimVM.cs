using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CinemaManagement.ViewModel.AdminVM;
using CinemaManagement.Ultis;
using System.Collections.ObjectModel;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM : BaseViewModel
    {
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(TenPhim) && !string.IsNullOrEmpty(TheLoai)
            && ThoiLuong != null && !string.IsNullOrEmpty(NuocSX)
            && !string.IsNullOrEmpty(DaoDien) && !string.IsNullOrEmpty(GioiHanTuoi)
            && !string.IsNullOrEmpty(NoiDung) && Poster != null && NgayPH != null;
        }
        public async Task SaveNewFilm(Window w1)
        {
            if (CheckNonEmpty())
            {
                PhimDTO Phim = new PhimDTO
                {
                    TenPhim = TenPhim,
                    ThoiLuong = Convert.ToInt32(ThoiLuong),
                    TheLoai = TheLoai,
                    NuocSX = NuocSX,
                    DaoDien = DaoDien,
                    NoiDung = NoiDung,
                    GioiHanTuoi = Convert.ToByte(GioiHanTuoi),
                    NgayPH = Convert.ToDateTime(NgayPH),
                    Poster = Poster
                };
                (bool trangthai, string messages, int newId) = await PhimDAL.Instance.AddMovie(Phim);
                if (trangthai)
                {
                    MessageBox.Show(messages);
                    IsLoading = true;
                    var data = await Task.Run(async () => await PhimDAL.Instance.GetAllMovie());
                    dsPhim = new ObservableCollection<PhimDTO>(data);
                    allFilm = new ObservableCollection<PhimDTO>(data);
                    CurrentWindow.Close();
                    IsLoading = false;
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
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin");
            }
        }
    }
}
