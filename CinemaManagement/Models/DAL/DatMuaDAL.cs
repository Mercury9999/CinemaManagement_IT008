using CinemaManagement.DTOs;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class DatMuaDAL
    {
        private static DatMuaDAL _instance;
        public static DatMuaDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatMuaDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<(bool, string, int)> CreateBooking(HoaDonDTO hoadon, List<VeDTO> dsVe, List<CTHDSanPham> dsSanPham)
        {
            int newBillId = 0;
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    //Tạo hoá đơn mới
                    newBillId = await CreateNewBill(context, hoadon);
                    //Thêm vé nếu có
                    if (dsVe.Any())
                    {
                        string ex = await UpdateTicketSell(context, dsVe);
                        if (ex != null)
                        {
                            return (false, ex, newBillId);
                        }
                        ex = await AddNewTicket(context, dsVe, newBillId);
                        if (ex != null)
                        {
                            return (false, ex, newBillId);
                        }
                    }

                    //Thêm sản phẩm nếu có
                    if (dsSanPham.Any())
                    {
                        string ex = await AddNewProductBillInfo(context, dsSanPham, newBillId);
                        if (ex != null)
                        {
                            return (false, ex, newBillId);
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false, "Lỗi hệ thống", newBillId);
            }
            return (true, "Giao dịch thành công", newBillId);
        }
        public async Task<int> CreateNewBill(CinemaManagementEntities context, HoaDonDTO hoadon)
        {
            int maxBillId = await context.HoaDons.MaxAsync(HoaDon => HoaDon.SoHD);
            int newBillId = maxBillId + 1;
            HoaDon newBill = new HoaDon
            {
                SoHD = hoadon.SoHD,
                MaKH = hoadon.MaKH,
                MaNV = hoadon.MaNV,
                NgayHD = DateTime.Now,
                ChietKhau = hoadon.ChietKhau,
                GiamGia = hoadon.GiamGia,
                GiaTriHD = hoadon.GiaTriHD,
                ThanhTien = hoadon.ThanhTien
            };

            context.HoaDons.Add(newBill);

            return newBillId;
        }
        public async Task<string> AddNewTicket(CinemaManagementEntities context, List<VeDTO> dsVe, int newBillId)
        {
            try
            {
                List<Ve> DsVe = new List<Ve>();
                int TicketId = await context.Ves.MaxAsync(Ve => Ve.MaVe);
                for (int i = 0; i < dsVe.Count; i++)
                {
                    DsVe.Add(
                    new Ve
                    {
                        SoHD = newBillId,
                        MaVe = ++TicketId,
                        MaSC = dsVe[i].MaSC,
                        MaGhe = dsVe[i].MaGhe,
                        SoGhe = dsVe[i].SoGhe,
                        GiaVe = dsVe[i].GiaVe
                    });
                }
                context.Ves.AddRange(DsVe);
                await context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Lỗi hệ thống";
            }
        }

        public async Task<string> AddNewProductBillInfo(CinemaManagementEntities context, List<CTHDSanPham> dsSanPham, int newBillId)
        {
            try
            {
                List<CTHDSanPham> dsCTHDSP = new List<CTHDSanPham>();

                for (int i = 0; i < dsSanPham.Count(); i++)
                {
                    dsCTHDSP.Add(
                     new CTHDSanPham
                     {
                         SoHD = newBillId,
                         MaSP = dsSanPham[i].MaSP,
                         DonGia = dsSanPham[i].DonGia,
                         SoLuong = dsSanPham[i].SoLuong,
                     });
                    var SanPham = await context.SanPhams.FindAsync(dsSanPham[i].MaSP);
                    if(SanPham == null)
                    {
                        return ("Sản phẩm " + dsSanPham[i].MaSP + " không tồn tại, vui lòng thêm sản phẩm");
                    }
                    if (SanPham.SoLuong < dsSanPham[i].SoLuong)
                    {
                        return "Không đủ sản phẩm còn trong kho";
                    }
                    SanPham.SoLuong -= dsSanPham[i].SoLuong;
                }
                context.CTHDSanPhams.AddRange(dsCTHDSP);
                await context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Lỗi hệ thống";
            }
        }
        public async Task<string> UpdateTicketSell(CinemaManagementEntities context, List<VeDTO> dsVe)
        {
            try
            {
                List<KeyValuePair<int, int>> dsGhe = new List<KeyValuePair<int, int>>();
                foreach (VeDTO ve in dsVe)
                {
                    dsGhe.Add(new KeyValuePair<int, int>(ve.MaVe, ve.MaSC));
                }
                var dsDatGhe = await context.BanVes.Where(s => dsGhe.Any(ghe => ghe.Key == s.MaGhe && ghe.Value == s.MaSC)).ToListAsync();
                List<KeyValuePair<int, int>> dsGheDaDat = new List<KeyValuePair<int, int>>();
                foreach (var s in dsDatGhe)
                {
                    if (s.DaBan == false)
                    {
                        s.DaBan = true;
                    }
                    else
                    {
                        var ghe = s.Ghe;
                        var suatchieu = s.MaSC;
                        dsGheDaDat.Add(new KeyValuePair<int, int>(ghe.MaGhe, suatchieu));
                    }
                }
                if (dsGheDaDat.Any())
                {
                    return ("Các ghế sau đã có người đặt : " + String.Join(", ", dsGheDaDat.Select(ghe => "$Ghế {ghe.key} suất chiếu {ghe.value}")));
                }
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Lỗi hệ thống";
            }
        }
    }
}
