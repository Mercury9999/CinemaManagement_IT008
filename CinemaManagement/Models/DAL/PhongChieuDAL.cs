using CinemaManagement.DTOs;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class PhongChieuDAL
    {
        private static PhongChieuDAL _instance;
        public static PhongChieuDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhongChieuDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task GenerateChair()
        {
            using (var context = new CinemaManagementEntities())
            {
                for (int i = 1; i <= 5; i++)
                {
                    PhongChieu phongChieu = new PhongChieu
                    {
                        SoPhong = i,
                        SLGhe = 110,
                        Ghes = new List<Ghe>()
                    };

                    for (int j = 1; j <= 110; j++)
                    {
                        Ghe ghe = new Ghe
                        {
                            MaGhe = (i-1)*110+j,
                            SoGhe = j,
                            SoPhong = i,
                        };
                        context.Ghes.Add(ghe);
                    }

                    context.PhongChieux.Add(phongChieu);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
