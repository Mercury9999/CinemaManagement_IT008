using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class SuCoDAL
    {
        private static SuCoDAL _instance;
        public static SuCoDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SuCoDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<SuCoDTO>> GetAllSuCo()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    List<SuCoDTO> dsSC = await (from sc in context.SuCoes
                                                select new SuCoDTO
                                                {
                                                    MaSuCo = sc.MaSuCo,
                                                    MaNVBao = sc.MaNVBao,
                                                    DiaDiem = sc.DiaDiem,
                                                    CTSuCo = sc.CTSuCo,
                                                    TinhTrang = sc.TinhTrang,
                                                    NgayBaoSC = sc.NgayBaoSC,
                                                    PhiSuaChua = sc.PhiSuaChua
                                                }).ToListAsync();

                    return dsSC;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<(string, SuCo)> AddSuCo(SuCoDTO newSuCo)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var maxId = await context.SuCoes.MaxAsync(s => s.MaSuCo);
                    SuCo SC = new SuCo()
                    {
                        MaSuCo = maxId + 1,
                        MaNVBao = newSuCo.MaNVBao,
                        DiaDiem = newSuCo.DiaDiem,
                        CTSuCo = newSuCo.CTSuCo,
                        TinhTrang = newSuCo.TinhTrang,
                        NgayBaoSC = DateTime.Now,
                        PhiSuaChua = 0
                    };
                    context.SuCoes.Add(SC);

                    await context.SaveChangesAsync();

                    return ("Thêm sự cố thành công", SC);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ("Lỗi hệ thống", null);
            }
        }


        public async Task<(bool, string)> UpdateSuCo(SuCoDTO UpdatedSC)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {

                    var SC = await context.SuCoes.FindAsync(UpdatedSC.MaSuCo);
                    if (SC == null)
                    {
                        return (false, "Không tồn tại sự cố");
                    }
                    SC.MaSuCo = UpdatedSC.MaSuCo;
                    SC.MaNVBao = UpdatedSC.MaNVBao;
                    SC.DiaDiem = UpdatedSC.DiaDiem;
                    SC.CTSuCo = UpdatedSC.CTSuCo;
                    SC.TinhTrang = UpdatedSC.TinhTrang;
                    SC.PhiSuaChua = UpdatedSC.PhiSuaChua;
                    SC.NgayBaoSC = UpdatedSC.NgayBaoSC;

                    await context.SaveChangesAsync();

                    return (true, "Đã cập nhật sự cố");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, "Lỗi hệ thống");
            }
        }
    }
}