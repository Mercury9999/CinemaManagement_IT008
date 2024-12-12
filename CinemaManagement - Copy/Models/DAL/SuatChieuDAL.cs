using CinemaManagement.DTOs;
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class SuatChieuDAL
    {
        private static SuatChieuDAL _instance;
        public  static SuatChieuDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SuatChieuDAL();
                }
                return _instance;
            }
            private set => _instance = value;   
        }
        public async Task<(bool, string, int)> AddShowTime(SuatChieuDTO suatchieu)
        {
            int newShowtimeId = -1;
            try
            {
                using(var context = new CinemaManagementEntities())
                {
                    var p = suatchieu.Phim;
                    if(p == null)
                    {
                        return (false, " Phim không tồn tại", 0);
                    }
                    var st = suatchieu.BatDau;
                    var ed = suatchieu.KetThuc;
                    TimeSpan thoigian = ed - st;
                    double tg = thoigian.TotalMinutes;
                    if(tg < suatchieu.Phim.ThoiLuong + 20)
                    {
                        return (false, "Thời gian suất chiếu tối thiểu phải dài hơn thời lượng phim 20 phút", newShowtimeId);
                        //Thời gian suất chiếu min = thời gian chiếu phim + 20 phút, lúc in vé chỉ in thời gian bắt đầu và thời lượng phim
                    }
                    var suatChieuList = context.SuatChieux.Where(sc => sc.SoPhongChieu == suatchieu.SoPhongChieu).ToList();

                    var s = suatChieuList.Where(sc => IsTimeBetween(st, sc.BatDau, sc.KetThuc) || IsTimeBetween(ed, sc.BatDau, sc.KetThuc)).FirstOrDefault();

                    if (s != null)
                    {
                        return (false, $"Thời gian {ConvertDateTime.Clock(s.BatDau)} đến {ConvertDateTime.Clock(s.KetThuc)} đã có suất chiếu khác ", newShowtimeId);
                    }
                    int maxShowtimeId = -1;
                    if (await context.SuatChieux.AnyAsync()) maxShowtimeId = await context.SuatChieux.MaxAsync(s => s.MaSC);
                    else maxShowtimeId = 0;
                    newShowtimeId = maxShowtimeId + 1;
                    SuatChieu sc = new SuatChieu()
                    {
                        MaSC = newShowtimeId,
                        MaPhim = suatchieu.MaPhim,
                        BatDau = suatchieu.BatDau,
                        KetThuc = suatchieu.KetThuc,
                        GiaVe = suatchieu.GiaVe,
                        SoPhongChieu = suatchieu.SoPhongChieu
                    };
                    context.SuatChieux.Add(sc);

                    //Tạo ds bán vé
                    var dsghe = await (from ghe in context.Ghes
                                 where ghe.SoPhong == suatchieu.SoPhongChieu
                                 select ghe.MaGhe
                                ).ToListAsync();
                    List<BanVe> banve = new List<BanVe>();
                    foreach( var maghe in dsghe  )
                    {
                        banve.Add(new BanVe
                        {
                            MaSC = newShowtimeId,
                            MaGhe = maghe,
                            DaBan = false
                        });
                    }
                    context.BanVes.AddRange(banve);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống: " + ex.Message, newShowtimeId);
            }
            return (true, "Thêm suất chiếu thành công", newShowtimeId);
        }
        public async Task<List<SuatChieuDTO>> GetShowTimeByRoom(int soPhong, DateTime selectedDate)
        {
            List<SuatChieuDTO> dsSC = new List<SuatChieuDTO>();
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var suatChieuList = await (from sc in context.SuatChieux
                                               join p in context.Phims on sc.MaPhim equals p.MaPhim
                                               where sc.SoPhongChieu == soPhong && DbFunctions.TruncateTime(sc.BatDau) == selectedDate.Date
                                               select new
                                               {
                                                   sc.MaSC,
                                                   sc.BatDau,
                                                   sc.KetThuc,
                                                   sc.GiaVe,
                                                   sc.SoPhongChieu,
                                                   sc.MaPhim
                                               }).ToListAsync();

                    dsSC = suatChieuList.Select(sc => new SuatChieuDTO
                    {
                        MaSC = sc.MaSC,
                        BatDau = sc.BatDau,
                        KetThuc = sc.KetThuc,
                        GiaVe = sc.GiaVe,
                        SoPhongChieu = sc.SoPhongChieu,
                        Phim = PhimDAL.Instance.GetMovieById(sc.MaPhim)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                CustomControls.MyMessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            return dsSC;
        }


        //Này cho tính năng tìm kiếm
        //Nhận 1 phim và ngày, trả về danh sách suất chiếu của phim đó trong ngày đó
        public async Task<List<SuatChieuDTO>> GetShowTimeByMovie(int MaPhim, DateTime dt)
        {
            List<SuatChieuDTO> dsSC = new List<SuatChieuDTO>();
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    dsSC = await (from s in context.SuatChieux
                                    where s.MaPhim == MaPhim && s.BatDau.Date == dt.Date
                                    select new SuatChieuDTO
                                    {
                                        MaSC = s.MaSC,
                                        MaPhim = s.MaPhim,
                                        BatDau = s.BatDau,
                                        KetThuc = s.KetThuc,
                                        GiaVe = s.GiaVe,
                                        SoPhongChieu = s.SoPhongChieu
                                    }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return dsSC;
        }

        static bool IsTimeBetween(DateTime checkTime, DateTime startTime, DateTime endTime)
        {
            return checkTime >= startTime && checkTime <= endTime;
        }
    }

}
