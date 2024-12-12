using CinemaManagement.DTOs;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class PhimDAL
    {
        private static PhimDAL _instance;
        public static PhimDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhimDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        //Này là crud với get
        public async Task<List<PhimDTO>> GetAllMovie()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsphim = (from p in context.Phims
                                  select new PhimDTO
                                  {
                                      MaPhim = p.MaPhim,
                                      TenPhim = p.TenPhim,
                                      TheLoai = p.TheLoai,
                                      ThoiLuong = p.ThoiLuong,
                                      NuocSX = p.NuocSX,
                                      NgayPH = p.NgayPH,
                                      DaoDien = p.DaoDien,
                                      NoiDung = p.NoiDung,
                                      GioiHanTuoi = p.GioiHanTuoi,
                                      Poster = p.Poster,
                                      DoanhThu = p.DoanhThu
                                  }).ToListAsync();
                    return await dsphim;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
        public async Task<(bool, string)> DeleteMovie(int maPhimXoa)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var p = await context.Phims.FindAsync(maPhimXoa);
                    if (p == null)
                    {
                        return (false, "Phim không tồn tại");
                    }
                    context.Phims.Remove(p);
                    await context.SaveChangesAsync();
                    return (true, "Đã xoá phim");
                }
            }
            catch (DbUpdateException ex)
            {
                return (false, "Lỗi CSDL");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, "Lỗi hệ thống");
            }
        }
        public async Task<(bool, string)> UpdateMovie(PhimDTO pcapnhat)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var p = await context.Phims.FindAsync(pcapnhat.MaPhim);
                    if (p == null)
                    {
                        return (false, "Phim không tồn tại");
                    }
                    p.MaPhim = pcapnhat.MaPhim;
                    p.TenPhim = pcapnhat.TenPhim;
                    p.TheLoai = pcapnhat.TheLoai;
                    p.ThoiLuong = pcapnhat.ThoiLuong;
                    p.NuocSX = pcapnhat.NuocSX;
                    p.NgayPH = pcapnhat.NgayPH;
                    p.DaoDien = pcapnhat.DaoDien;
                    p.NoiDung = pcapnhat.NoiDung;
                    p.GioiHanTuoi = pcapnhat.GioiHanTuoi;
                    p.Poster = pcapnhat.Poster;
                    p.DoanhThu = pcapnhat.DoanhThu;

                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL");
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
            return (true, "Đã cập nhật");
        }
        public async Task<(bool, string, int)> AddMovie(PhimDTO phim)
        {
            int newMovieId = -1;
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    int maxMovieId;
                    if (await context.Phims.AnyAsync()) maxMovieId = await context.Phims.MaxAsync(s => s.MaPhim);
                    else maxMovieId = 0;
                    newMovieId = maxMovieId + 1;

                    var p = new Phim()
                    {
                        MaPhim = newMovieId, 
                        TenPhim = phim.TenPhim,
                        TheLoai = phim.TheLoai,
                        ThoiLuong = phim.ThoiLuong,
                        NuocSX = phim.NuocSX,
                        NgayPH = phim.NgayPH,
                        DaoDien = phim.DaoDien,
                        NoiDung = phim.NoiDung,
                        GioiHanTuoi = phim.GioiHanTuoi,
                        Poster = phim.Poster,
                    };

                    context.Phims.Add(p);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL", newMovieId);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString(), newMovieId);
            }
            return (true, "Thêm phim thành công", newMovieId);
        }
        public PhimDTO GetMovieById(int Id)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    PhimDTO phim = (from p in context.Phims
                                where p.MaPhim == Id
                                select new PhimDTO 
                                {
                                    MaPhim = p.MaPhim,
                                    TenPhim = p.TenPhim,
                                    TheLoai = p.TheLoai,
                                    ThoiLuong = p.ThoiLuong,
                                    NuocSX = p.NuocSX,
                                    NgayPH = p.NgayPH,
                                    DaoDien = p.DaoDien,
                                    NoiDung = p.NoiDung,
                                    GioiHanTuoi = p.GioiHanTuoi,
                                    Poster = p.Poster,
                                    DoanhThu = p.DoanhThu
                                }).FirstOrDefault();
                    return phim;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}
