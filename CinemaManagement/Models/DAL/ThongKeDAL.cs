using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.Models.DAL
{
    public class ThongKeDAL
    {
        private static ThongKeDAL _instance;
        public static ThongKeDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThongKeDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<KhachHangDTO>> GetTop5CustomerByBenefit()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                       var dsKH =   (from kh in context.KhachHangs
                                    orderby kh.HDTichLuy descending
                                    select new KhachHangDTO
                                    {
                                        MaKH = kh.MaKH,
                                        TenKH = kh.TenKH,
                                        SDT_KH = kh.SDT_KH,
                                        email_KH = kh.email_KH,
                                        NgaySinh = kh.NgaySinh,
                                        GioiTinh = kh.GioiTinh,
                                        NgayDK = kh.NgayDK,
                                        HDTichLuy = kh.HDTichLuy,
                                       }).Take(5).ToListAsync();
                    return await dsKH;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Lỗi dữ liệu: " + ex.ToString());
                throw ex;
            }
        }
        public async Task<List<PhimDTO>> GetTop5FilmByBenefit()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsPhim = await (from p in context.Phims
                                  join sc in context.SuatChieux on p.MaPhim equals sc.MaPhim
                                  join bv in context.BanVes on sc.MaSC equals bv.MaSC
                                  where bv.DaBan == true
                                  group new { p, sc } by new { p.MaPhim, p.TenPhim } into s
                                  orderby s.Sum(s => s.sc.GiaVe) descending
                                  select new PhimDTO
                                  {
                                      MaPhim = s.Key.MaPhim,
                                      TenPhim = s.Key.TenPhim,
                                      DoanhThu = s.Sum(s => s.sc.GiaVe)
                                  }).Take(5).ToListAsync();
                    return dsPhim;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Lỗi dữ liệu: " + ex.ToString());
                throw ex;
            }
        }
    }
}
