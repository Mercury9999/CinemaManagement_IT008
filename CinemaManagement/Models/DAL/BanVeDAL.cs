using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.View;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class BanVeDAL
    {
        private static BanVeDAL _instance;
        public static BanVeDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BanVeDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<BanVeDTO>> GetTicketSell(int MaSC)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsBanVe = await (from s in context.BanVes
                                         join g in context.Ghes on s.MaGhe equals g.MaGhe
                                         join sc in context.SuatChieux on s.MaSC equals sc.MaSC
                                         where s.MaSC == MaSC
                                         select new BanVeDTO
                                         {
                                             MaSC = s.MaSC,
                                             MaGhe = s.MaGhe,
                                             Ghe = new GheDTO
                                             {
                                                 MaGhe = g.MaGhe,
                                                 SoGhe = g.SoGhe,
                                             },
                                             SuatChieu = new SuatChieuDTO
                                             {
                                                 BatDau = sc.BatDau,
                                                 KetThuc = sc.KetThuc,
                                                 GiaVe = sc.GiaVe,
                                                 SoPhongChieu = sc.SoPhongChieu,
                                                 MaPhim = sc.MaPhim
                                             },
                                         }).ToListAsync();
                    return dsBanVe;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString());
                return null;
            }
        }
        public async Task<bool> OrderTicket(List<BanVeDTO> dsVeMua)
        {
            using (var context = new CinemaManagementEntities())
            {
                for (int i = 0; i < dsVeMua.Count; i++)
                {
                    var ve = await context.BanVes.FindAsync(dsVeMua[i].MaSC, dsVeMua[i].MaGhe);
                    if(ve != null) ve.DaBan = true;
                }
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
