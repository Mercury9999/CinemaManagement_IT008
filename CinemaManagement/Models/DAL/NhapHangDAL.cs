using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class NhapHangDAL
    {
        private static NhapHangDAL _instace;
        public static NhapHangDAL Instace
        {
            get
            {
                if( _instace == null )
                {
                    _instace = new NhapHangDAL();
                }
                return _instace;
            }
            private set => _instace = value;
        }
        public async Task<int> CreateNewBillReceipt(CinemaManagementEntities context, HDNhapHangDTO hdNhap)
        {
            int maxBillReceiptId = await context.HDNhapHangs.MaxAsync(HDNhapHang => HDNhapHang.SoHDNhap);
            int newBillReceiptId = maxBillReceiptId + 1;
            HDNhapHang HDNhap = new HDNhapHang
            {
                SoHDNhap = newBillReceiptId,
                NgayNhap = DateTime.Now,
                MaNVNhap = hdNhap.MaNVNhap,
                MaSPNhap = hdNhap.MaSPNhap,
                DonGiaNhap = hdNhap.DonGiaNhap,
                SoLuong = hdNhap.SoLuong,
                ThanhTien = hdNhap.SoLuong * hdNhap.DonGiaNhap
            };
            context.HDNhapHangs.Add(HDNhap);
            return newBillReceiptId;
        }
        public async Task<int> GetAllBillReceipt(CinemaManagementEntities context, HDNhapHangDTO hdNhap)
        {
            int maxBillReceiptId = await context.HDNhapHangs.MaxAsync(HDNhapHang => HDNhapHang.SoHDNhap);
            int newBillReceiptId = maxBillReceiptId + 1;
            HDNhapHang HDNhap = new HDNhapHang
            {
                SoHDNhap = newBillReceiptId,
                NgayNhap = DateTime.Now,
                MaNVNhap = hdNhap.MaNVNhap,
                MaSPNhap = hdNhap.MaSPNhap,
                DonGiaNhap = hdNhap.DonGiaNhap,
                SoLuong = hdNhap.SoLuong,
                ThanhTien = hdNhap.SoLuong * hdNhap.DonGiaNhap
            };
            context.HDNhapHangs.Add(HDNhap);
            return newBillReceiptId;
        }
    }
    
}
