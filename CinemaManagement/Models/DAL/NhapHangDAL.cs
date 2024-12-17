using CinemaManagement.CustomControls;
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
        public async Task<int> CreateNewBillReceipt(HDNhapHangDTO hdNhap)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {

                    int maxBillReceiptId = await context.HDNhapHangs.MaxAsync(s => s.SoHDNhap);
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
                    var spnhap = await context.HDNhapHangs.FindAsync(hdNhap.MaSPNhap);
                    if(spnhap == null)
                    {
                        MyMessageBox.Show("Sản phẩm được nhập không tồn tại");
                        return -1;
                    }
                    spnhap.SoLuong += hdNhap.SoLuong;
                    context.HDNhapHangs.Add(HDNhap);
                    context.SaveChangesAsync();
                    return newBillReceiptId;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.ToString());
                return -1;
            }
        }
    }
    
}
