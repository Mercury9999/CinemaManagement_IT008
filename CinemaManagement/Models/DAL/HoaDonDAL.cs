using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<HoaDonDTO>> GetAllBill()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dshoadon = (from hoadon in context.HoaDons
                                    select new HoaDonDTO
                                    {
                                        SoHD = hoadon.SoHD,
                                        MaKH = hoadon.MaKH,
                                        TenKH = hoadon.KhachHang.TenKH,
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
                        TenNV = hoadon.NhanVien.TenNV,
                        GiamGia = hoadon.GiamGia,
                        ChietKhau = hoadon.ChietKhau,
                        GiaTriHD = hoadon.GiaTriHD,
                        NgayHD = hoadon.NgayHD,
                        MaKH = hoadon.MaKH,
                        TenKH = hoadon.KhachHang.TenKH,
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
                throw ex;
            }
        }
    }
}
