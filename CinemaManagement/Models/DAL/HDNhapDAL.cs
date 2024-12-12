using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class HDNhapDAL
    {
        private static HDNhapDAL _instance;
        public static HDNhapDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HDNhapDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<HDNhapHangDTO>> GetAllReceipt()
        {
            using(var context = new CinemaManagementEntities())
            {
                var dshdnhap = (from hdnhap in context.HDNhapHangs
                                join sanpham in context.SanPhams
                                on hdnhap.MaSPNhap equals sanpham.MaSP
                                join nhanvien in context.NhanViens
                                on hdnhap.MaNVNhap equals nhanvien.MaNV
                                select new HDNhapHangDTO
                                {
                                    SoHDNhap = hdnhap.SoHDNhap,
                                    MaNVNhap = hdnhap.MaNVNhap,
                                    NgayNhap = hdnhap.NgayNhap,
                                    MaSPNhap = (int)hdnhap.MaSPNhap,
                                    DonGiaNhap = (decimal)hdnhap.DonGiaNhap,
                                    TenSPNhap = sanpham.TenSP, 
                                    TenNVNhap = nhanvien.TenNV,
                                    ThanhTien = hdnhap.ThanhTien
                                }).ToListAsync();

                return await dshdnhap;

            }
        }
    }
}
