using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class NhanVienDAL
    {
        private static NhanVienDAL _instance;
        public static NhanVienDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NhanVienDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        //Dùng cho chức năng thêm xoá sửa
        public async Task<List<NhanVienDTO>> GetAllStaff()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsnhanvien = (from nv in context.NhanViens
                                      select new NhanVienDTO
                                      {
                                          MaNV = nv.MaNV,
                                          TenNV = nv.TenNV,
                                          SDT_NV = nv.SDT_NV,
                                          email_NV = nv.email_NV,
                                          NgaySinh = nv.NgaySinh,
                                          GioiTinh = nv.GioiTinh,
                                          ChucVu = nv.ChucVu,
                                          NgayVaoLam = nv.NgayVaoLam
                                      }).ToListAsync();
                    return await dsnhanvien;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
        public async Task<(bool, string)> DeleteStaff(int maNvXoa)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var nv = await context.NhanViens.FindAsync(maNvXoa);
                    if (nv == null)
                    {
                        return (false, "Nhân viên nvông tồn tại");
                    }
                    context.NhanViens.Remove(nv);
                    await context.SaveChangesAsync();
                    return (true, "Đã xoá nhân viên");
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
        public async Task<(bool, string)> UpdateStaff(NhanVienDTO nvcapnhat)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var nv = await context.NhanViens.FindAsync(nvcapnhat.MaNV);
                    if(nv == null)
                    {
                        return (false, "Mã nhân viên nvông tồn tại");
                    }
                    bool checkUsername = await context.NhanViens.AnyAsync(s => s.acc_username == nvcapnhat.acc_username && s.MaNV != nvcapnhat.MaNV);
                    bool checkSDT = await context.NhanViens.AnyAsync(s => s.SDT_NV == nvcapnhat.SDT_NV && s.MaNV != nvcapnhat.MaNV);
                    bool checkEmail = await context.NhanViens.AnyAsync(s => s.email_NV == nvcapnhat.email_NV && s.MaNV != nvcapnhat.MaNV);
                    if(checkUsername)
                    {
                        return (false, "Tên đăng nhập đã tồn tại");
                    }
                    if (checkSDT && !checkEmail)
                    {
                        return (false, "Số điện thoại đã được đăng ký");
                    }
                    if (checkEmail && !checkSDT)
                    {
                        return (false, "Email đã được đăng ký");
                    }
                    if (checkSDT && checkEmail)
                    {
                        return (false, "Số điện thoại và email đã được đăng ký");
                    }
                    nv.TenNV = nvcapnhat.TenNV;
                    nv.acc_username = nvcapnhat.acc_username;
                    nv.SDT_NV = nvcapnhat.SDT_NV;
                    nv.email_NV = nvcapnhat.email_NV;
                    nv.NgaySinh = nvcapnhat.NgaySinh;
                    nv.GioiTinh = nvcapnhat.GioiTinh;
                    nv.NgayVaoLam = nvcapnhat.NgayVaoLam;
                    nv.ChucVu = nvcapnhat.ChucVu;
                    
                    await context.SaveChangesAsync();

                }
            }
            catch(DbUpdateException e) 
            {
                return (false, "Lỗi CSDL");
            }
            catch(Exception ex)
            {
                return(false, ex.ToString());
            }
            return (true, "Đã cập nhật");
        }
        public async Task<(bool, string, int)> AddStaff(NhanVienDTO nhanvien)
        {
            int newStaffId = -1;
            try
            {
                using(var context = new CinemaManagementEntities())
                {
                    int maxStaffId = await context.NhanViens.MaxAsync(s => s.MaNV);
                    newStaffId = maxStaffId + 1;

                    var nv = new NhanVien();

                    //Kiểm tra SĐT, email của NV mới
                    bool checkSDT = await context.NhanViens.AnyAsync(s => s.SDT_NV == nhanvien.SDT_NV && s.MaNV != nhanvien.MaNV);
                    bool checkEmail = await context.NhanViens.AnyAsync(s => s.email_NV == nhanvien.email_NV && s.MaNV != nhanvien.MaNV);
                    if (checkSDT && !checkEmail)
                    {
                        return (false, "Số điện thoại đã được đăng ký", newStaffId);
                    }
                    if (checkEmail && !checkSDT)
                    {
                        return (false, "Email đã được đăng ký", newStaffId);
                    }
                    if(checkSDT && checkEmail)
                    {
                        return (false, "Số điện thoại và email đã được đăng ký", newStaffId);
                    }
                    nv.MaNV = newStaffId;
                    nv.TenNV = nhanvien.TenNV;
                    nv.acc_username = nhanvien.acc_username;
                    nv.acc_password = nhanvien.acc_password;
                    nv.SDT_NV = nhanvien.SDT_NV;
                    nv.email_NV = nhanvien.email_NV;
                    nv.NgaySinh = nhanvien.NgaySinh;
                    nv.GioiTinh = nhanvien.GioiTinh;
                    nv.NgayVaoLam = DateTime.Now;
                    nv.ChucVu = nhanvien.ChucVu;

                    context.NhanViens.Add(nv);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL", newStaffId);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString(), newStaffId);
            }
            return (true, "Đăng ký nhân viên thành công", newStaffId);
        }
        //Dùng cho chức năng login
        public async Task<(bool, string, NhanVienDTO)> Login(string username, string password)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var staff = await (from nv in context.NhanViens.AsNoTracking()
                                       where nv.acc_username == username && nv.acc_password == password
                                       select new NhanVienDTO
                                       {
                                           MaNV = nv.MaNV,
                                           TenNV = nv.TenNV,
                                           SDT_NV = nv.SDT_NV,
                                           email_NV = nv.email_NV,
                                           NgaySinh = nv.NgaySinh,
                                           GioiTinh = nv.GioiTinh,
                                           ChucVu = nv.ChucVu,
                                           NgayVaoLam = nv.NgayVaoLam
                                       }).FirstOrDefaultAsync();
                    if (staff == null)
                    {
                        return (false, "Sai thông tin đăng nhập", null);
                    }
                    else return (true, "Đăng nhập thành công", staff);
                }
            }
            catch(System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch(Exception ex)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }
        public async Task<NhanVienDTO> GetStaffById(int id)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                     var n =   (from nv in context.NhanViens
                                where nv.MaNV == id
                                select new NhanVienDTO
                                {
                                    MaNV = nv.MaNV,
                                    TenNV = nv.TenNV,
                                    GioiTinh = nv.GioiTinh,
                                    acc_password = nv.acc_password,
                                    acc_username = nv.acc_username,
                                    NgaySinh = nv.NgaySinh,
                                    NgayVaoLam = nv.NgayVaoLam,
                                    ChucVu = nv.ChucVu,
                                    SDT_NV = nv.SDT_NV,
                                    email_NV = nv.email_NV,
                                }).FirstOrDefaultAsync();

                    if (n == null)
                    {
                        MyMessageBox.Show("Sai mã nhân viên");
                        return null;
                    }
                    return await n;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Lỗi dữ liệu: " + ex.ToString());
                throw;
            }
        }
        public async Task<(bool, string)> ChangePassword(string username, string newpassword)
        {
            try
            {
                using(var context = new CinemaManagementEntities())
                {
                    var nv = context.NhanViens.FirstOrDefault(s => s.acc_username == username);
                    if(nv == null)
                    {
                        return (false, "Tài nvoản nvông tồn tại");
                    }
                    nv.acc_password = newpassword;
                    await context.SaveChangesAsync();
                }
            }
            catch(DbUpdateException ex)
            {
                return (false, "Lỗi CSDL");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, "Lỗi hệ thống");
            }
            return (true, "Đổi mật nvẩu thành công");
        }
    }
}
