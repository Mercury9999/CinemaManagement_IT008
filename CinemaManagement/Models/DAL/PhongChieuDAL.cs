using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<List<PhongChieuDTO>> GetAllRoom()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsPhongChieu = (from pc in context.PhongChieux
                                         select new PhongChieuDTO
                                         {
                                             SoPhong = pc.SoPhong,
                                             SLGhe = pc.SLGhe,
                                         }).ToListAsync();
                return await dsPhongChieu;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Lỗi: " + ex.ToString());
                return null;
            }
        }
        public async Task<(bool, string, PhongChieuDTO)> AddNewRoom()
        {
            PhongChieuDTO pc = new PhongChieuDTO()
            {
                SoPhong = -1,
                SLGhe = 0,
            };
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    int maxRoom;
                    if (await context.PhongChieux.AnyAsync()) maxRoom = await context.PhongChieux.MaxAsync(s => s.SoPhong);
                    else maxRoom = 0;
                    pc = new PhongChieuDTO()
                    {
                        SoPhong = maxRoom + 1,
                        SLGhe = 110,
                        Ghe = new List<GheDTO>()
                    };
                    PhongChieu phongChieu = new PhongChieu
                    {
                        SoPhong = pc.SoPhong,
                        SLGhe = pc.SLGhe,
                    };
                    context.PhongChieux.Add(phongChieu);
                    await context.SaveChangesAsync();
                    for (int j = 1; j <= pc.SLGhe; j++)
                    {
                        int newSeatId;
                        if (await context.Ghes.AnyAsync()) newSeatId = await context.Ghes.MaxAsync(s => s.MaGhe) + 1;
                        else newSeatId = 1;
                        Ghe ghe = new Ghe
                        {
                            MaGhe = newSeatId,
                            SoGhe = j,
                            SoPhong = phongChieu.SoPhong
                        };
                        context.Ghes.Add(ghe);
                        GheDTO gheDTO = new GheDTO
                        {
                            MaGhe = ghe.MaGhe, 
                            SoGhe = ghe.SoGhe, 
                            SoPhong = ghe.SoPhong
                        };
                        pc.Ghe.Add(gheDTO);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                return (false, "Lỗi CSDL: " + ex.ToString(), pc);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString(), pc);
            }
            return (true, "Đã thêm phòng chiếu", pc);
        }
    }
}
