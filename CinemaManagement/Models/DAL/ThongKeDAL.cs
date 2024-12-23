using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using System.Data.Entity;

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
        public async Task<List<SanPhamDTO>> GetTop5ProductBySales()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsPhim = await (from sp in context.SanPhams
                                        join cthd in context.CTHDSanPhams on sp.MaSP equals cthd.MaSP
                                        group new { sp, cthd } by new { sp.MaSP, sp.TenSP } into s
                                        orderby s.Sum(s => s.cthd.SoLuong) descending
                                        select new SanPhamDTO
                                        {
                                            MaSP = s.Key.MaSP,
                                            TenSP = s.Key.TenSP,
                                            SoLuong = s.Sum(s => s.cthd.SoLuong)
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
        public async Task<decimal> GetRevenue()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var doanhthu = await context.HoaDons.SumAsync(s => s.ThanhTien);
                    return doanhthu;
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
