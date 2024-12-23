using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class GheDAL
    {
        private static GheDAL _instance;
        public static GheDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GheDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<GheDTO>> GetSeat(int SoPhong, int MaSC)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsGhe = (from ghe in context.Ghes
                                 where ghe.SoPhong == SoPhong
                                 select new GheDTO
                                 {
                                     SoPhong = ghe.SoPhong,
                                     SoGhe = ghe.SoGhe,
                                     MaGhe = ghe.MaGhe,
                                 }).ToListAsync();
                    return await dsGhe;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Lỗi: " + ex.ToString());
                return null;
            }
        }
    }
}
