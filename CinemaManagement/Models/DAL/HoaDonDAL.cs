using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;


namespace CinemaManagement.Models.DAL
{
    public class HoaDonDAL
    {
        private static HoaDonDAL _instance;
        public static HoaDonDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new HoaDonDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<(bool, string, int)> AddNewBill(HoaDonDTO hoadon, ObservableCollection<SanPhamDTO> dssp, ObservableCollection<BanVeDTO> dsve)
        {
            int newBillId = -1;
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    int maxBillId;
                    if (await context.HoaDons.AnyAsync()) maxBillId = await context.HoaDons.MaxAsync(s => s.SoHD);
                    else maxBillId = 0;
                    newBillId = maxBillId + 1;
                    var hd = new HoaDon()
                    {
                        SoHD = newBillId,
                        MaKH = hoadon.MaKH,
                        MaNV = hoadon.MaNV,
                        NgayHD = DateTime.Now,
                        ChietKhau = hoadon.ChietKhau,
                        GiamGia = hoadon.GiamGia,
                        GiaTriHD = hoadon.GiaTriHD,
                        ThanhTien = hoadon.ThanhTien
                    };
                    for(int i = 0; i < dssp.Count; i++)
                    {
                        var sp = await context.SanPhams.FindAsync(dssp[i].MaSP);
                        sp.SoLuong -= dssp[i].SoLuong;
                    }
                    for(int i = 0; i < dsve.Count; i++)
                    {
                        var ve = await context.BanVes.FindAsync(dsve[i].MaSC, dsve[i].MaGhe);
                        ve.DaBan = true;
                    }
                    context.HoaDons.Add(hd);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL", newBillId);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString(), newBillId);
            }
            return (true, "Thêm sản phẩm thành công", newBillId);
        }
        public async Task<List<HoaDonDTO>> GetAllBill()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dshoadon = (from hoadon in context.HoaDons
                                    orderby hoadon.NgayHD descending
                                    select new HoaDonDTO
                                    {
                                        SoHD = hoadon.SoHD,
                                        MaKH = hoadon.MaKH,
                                        MaNV = hoadon.MaNV,
                                        NgayHD = hoadon.NgayHD,
                                        ChietKhau = hoadon.ChietKhau,
                                        GiamGia = hoadon.GiamGia,
                                        GiaTriHD = hoadon.GiaTriHD,
                                        ThanhTien = hoadon.ThanhTien
                                    }).ToListAsync();
                    return await dshoadon;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HoaDonDTO> GetBillDetail(int soHD)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var hoadon = await context.HoaDons.FindAsync(soHD);

                    HoaDonDTO CTHD = new HoaDonDTO
                    {
                        SoHD = hoadon.SoHD,
                        MaNV = hoadon.MaNV,
                        GiamGia = hoadon.GiamGia,
                        ChietKhau = hoadon.ChietKhau,
                        GiaTriHD = hoadon.GiaTriHD,
                        NgayHD = hoadon.NgayHD,
                        MaKH = hoadon.MaKH,
                        CTSP = (from ct in hoadon.CTHDSanPhams
                                select new CTHDSanPhamDTO
                                {
                                    SoHD = ct.SoHD,
                                    MaSP = ct.MaSP,
                                    TenSP = ct.SanPham.TenSP,
                                    DonGia = ct.DonGia,
                                    SoLuong = ct.SoLuong,
                                }).ToList(),
                        CTVe = (from ct in hoadon.Ves
                                select new VeDTO
                                {
                                   SoHD = ct.SoHD,
                                   MaVe = ct.MaVe,
                                   MaSC = ct.MaSC,
                                   GiaVe = ct.GiaVe,
                                   MaGhe = ct.MaGhe,
                                   SoGhe = ct.SoGhe,
                                }).ToList()
                    };
                    return CTHD;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

    }
}
