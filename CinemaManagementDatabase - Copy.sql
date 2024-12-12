CREATE DATABASE CinemaManagement
GO
USE CinemaManagement
GO


CREATE TABLE [dbo].[BanVe] (
    [MaSC] int  NOT NULL,
    [MaGhe] int  NOT NULL,
    [DaBan] bit  NOT NULL
);
GO


CREATE TABLE [dbo].[CTHDNhap] (
    [SoHDNhap] int  NOT NULL,
    [MaSPNhap] int  NOT NULL,
    [DonGiaNhap] decimal  NOT NULL,
    [SoLuong] int  NOT NULL,
    [TongTien] decimal  NOT NULL
);
GO


CREATE TABLE [dbo].[CTHDSanPham] (
    [SoHD] int  NOT NULL,
    [MaSP] int  NOT NULL,
    [DonGia] decimal  NOT NULL,
    [SoLuong] int  NOT NULL,
    [ThanhTien] decimal  NOT NULL
);
GO


CREATE TABLE [dbo].[Ghe] (
    [MaGhe] int  NOT NULL,
    [SoGhe] int  NOT NULL,
    [SoPhong] int  NOT NULL
);
GO


CREATE TABLE [dbo].[HDNhapHang] (
    [SoHDNhap] int  NOT NULL,
    [NgayNhap] datetime  NOT NULL,
    [ThanhTien] decimal  NOT NULL,
    [MaNVNhap] int  NOT NULL
);
GO


CREATE TABLE [dbo].[HoaDon] (
    [SoHD] int  NOT NULL,
    [MaKH] int  NOT NULL,
    [MaNV] int  NOT NULL,
    [NgayHD] datetime  NOT NULL,
    [ChietKhau] int  NOT NULL,
    [GiaTriHD] decimal  NOT NULL,
    [ThanhTien] decimal  NOT NULL,
    [GiamGia] decimal  NOT NULL
);
GO


CREATE TABLE [dbo].[KhachHang] (
    [MaKH] int  NOT NULL,
    [TenKH] nvarchar(50)  NOT NULL,
    [GioiTinh] nvarchar(10)  NULL,
    [SDT_KH] char(10)  NOT NULL,
    [email_KH] varchar(50)  NULL,
    [NgaySinh] datetime  NOT NULL,
    [HDTichLuy] decimal  NOT NULL,
    [NgayDK] datetime  NOT NULL
);
GO


CREATE TABLE [dbo].[NhanVien] (
    [MaNV] int  NOT NULL,
    [TenNV] nvarchar(50)  NOT NULL,
    [acc_username] varchar(20)  NOT NULL,
    [acc_password] varchar(20)  NOT NULL,
    [SDT_NV] char(10)  NOT NULL,
    [email_NV] varchar(50)  NULL,
    [NgaySinh] datetime  NOT NULL,
    [GioiTinh] nvarchar(3)  NULL,
    [NgayVaoLam] datetime  NOT NULL,
    [ChucVu] nvarchar(50)  NOT NULL,
    [Staff_Level] tinyint  NOT NULL,
    [IsDeleted] bit  NULL
);
GO


CREATE TABLE [dbo].[Phim] (
    [MaPhim] int  NOT NULL,
    [TheLoai] nvarchar(100)  NOT NULL,
    [ThoiLuong] int  NOT NULL,
    [NuocSX] nvarchar(50)  NULL,
    [NgayPH] datetime  NULL,
    [DaoDien] nvarchar(50)  NULL,
    [NoiDung] nvarchar(max)  NOT NULL,
    [GioiHanTuoi] tinyint  NOT NULL,
    [Poster] varbinary(max)  NOT NULL,
    [TenPhim] nvarchar(100)  NOT NULL,
    [DoanhThu] decimal  NOT NULL
);
GO


CREATE TABLE [dbo].[PhongChieu] (
    [SoPhong] int  NOT NULL,
    [SLGhe] int  NOT NULL
);
GO


CREATE TABLE [dbo].[SanPham] (
    [MaSP] int  NOT NULL,
    [LoaiSP] nvarchar(50)  NOT NULL,
    [TenSP] nvarchar(100)  NOT NULL,
    [SoLuong] int  NOT NULL,
    [GiaSP] decimal  NOT NULL,
    [HinhAnhSP] varbinary(max)  NULL,
    [IsDeleted] bit  NULL
);
GO


CREATE TABLE [dbo].[SuatChieu] (
    [MaSC] int  NOT NULL,
    [MaPhim] int  NOT NULL,
    [SoPhongChieu] int  NOT NULL,
    [BatDau] datetime  NOT NULL,
    [KetThuc] datetime  NOT NULL,
    [GiaVe] decimal  NOT NULL
);
GO


CREATE TABLE [dbo].[SuCo] (
    [MaSuCo] int  NOT NULL,
    [MaNVBao] int  NOT NULL,
    [DiaDiem] nvarchar(100)  NOT NULL,
    [CTSuCo] nvarchar(max)  NOT NULL,
    [TinhTrang] bit  NOT NULL,
    [PhiSuaChua] decimal  NOT NULL,
    [NgayBaoSC] datetime  NOT NULL
);
GO


CREATE TABLE [dbo].[Ve] (
    [SoHD] int  NOT NULL,
    [MaVe] int  NOT NULL,
    [MaSC] int  NOT NULL,
    [MaGhe] int  NOT NULL,
    [SoGhe] int  NOT NULL,
    [GiaVe] decimal  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MaSC], [MaGhe] in table 'BanVe'
ALTER TABLE [dbo].[BanVe]
ADD CONSTRAINT [PK_BanVe]
    PRIMARY KEY CLUSTERED ([MaSC], [MaGhe] ASC);
GO

-- Creating primary key on [SoHDNhap], [MaSPNhap] in table 'CTHDNhap'
ALTER TABLE [dbo].[CTHDNhap]
ADD CONSTRAINT [PK_CTHDNhap]
    PRIMARY KEY CLUSTERED ([SoHDNhap], [MaSPNhap] ASC);
GO

-- Creating primary key on [SoHD], [MaSP] in table 'CTHDSanPham'
ALTER TABLE [dbo].[CTHDSanPham]
ADD CONSTRAINT [PK_CTHDSanPham]
    PRIMARY KEY CLUSTERED ([SoHD], [MaSP] ASC);
GO

-- Creating primary key on [MaGhe] in table 'Ghe'
ALTER TABLE [dbo].[Ghe]
ADD CONSTRAINT [PK_Ghe]
    PRIMARY KEY CLUSTERED ([MaGhe] ASC);
GO

-- Creating primary key on [SoHDNhap] in table 'HDNhapHang'
ALTER TABLE [dbo].[HDNhapHang]
ADD CONSTRAINT [PK_HDNhapHang]
    PRIMARY KEY CLUSTERED ([SoHDNhap] ASC);
GO

-- Creating primary key on [SoHD] in table 'HoaDon'
ALTER TABLE [dbo].[HoaDon]
ADD CONSTRAINT [PK_HoaDon]
    PRIMARY KEY CLUSTERED ([SoHD] ASC);
GO

-- Creating primary key on [MaKH] in table 'KhachHang'
ALTER TABLE [dbo].[KhachHang]
ADD CONSTRAINT [PK_KhachHang]
    PRIMARY KEY CLUSTERED ([MaKH] ASC);
GO

-- Creating primary key on [MaNV] in table 'NhanVien'
ALTER TABLE [dbo].[NhanVien]
ADD CONSTRAINT [PK_NhanVien]
    PRIMARY KEY CLUSTERED ([MaNV] ASC);
GO

-- Creating primary key on [MaPhim] in table 'Phim'
ALTER TABLE [dbo].[Phim]
ADD CONSTRAINT [PK_Phim]
    PRIMARY KEY CLUSTERED ([MaPhim] ASC);
GO

-- Creating primary key on [SoPhong] in table 'PhongChieu'
ALTER TABLE [dbo].[PhongChieu]
ADD CONSTRAINT [PK_PhongChieu]
    PRIMARY KEY CLUSTERED ([SoPhong] ASC);
GO

-- Creating primary key on [MaSP] in table 'SanPham'
ALTER TABLE [dbo].[SanPham]
ADD CONSTRAINT [PK_SanPham]
    PRIMARY KEY CLUSTERED ([MaSP] ASC);
GO

-- Creating primary key on [MaSC] in table 'SuatChieu'
ALTER TABLE [dbo].[SuatChieu]
ADD CONSTRAINT [PK_SuatChieu]
    PRIMARY KEY CLUSTERED ([MaSC] ASC);
GO

-- Creating primary key on [MaSuCo] in table 'SuCo'
ALTER TABLE [dbo].[SuCo]
ADD CONSTRAINT [PK_SuCo]
    PRIMARY KEY CLUSTERED ([MaSuCo] ASC);
GO

-- Creating primary key on [SoHD], [MaVe] in table 'Ve'
ALTER TABLE [dbo].[Ve]
ADD CONSTRAINT [PK_Ve]
    PRIMARY KEY CLUSTERED ([SoHD], [MaVe] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MaGhe] in table 'BanVe'
ALTER TABLE [dbo].[BanVe]
ADD CONSTRAINT [FK__BanVe__MaGhe__17036CC0]
    FOREIGN KEY ([MaGhe])
    REFERENCES [dbo].[Ghe]
        ([MaGhe])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BanVe__MaGhe__17036CC0'
CREATE INDEX [IX_FK__BanVe__MaGhe__17036CC0]
ON [dbo].[BanVe]
    ([MaGhe]);
GO

-- Creating foreign key on [MaSC] in table 'BanVe'
ALTER TABLE [dbo].[BanVe]
ADD CONSTRAINT [FK__BanVe__MaSC__160F4887]
    FOREIGN KEY ([MaSC])
    REFERENCES [dbo].[SuatChieu]
        ([MaSC])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MaSPNhap] in table 'CTHDNhap'
ALTER TABLE [dbo].[CTHDNhap]
ADD CONSTRAINT [FK__CT_HDNHAP__MaSPN__59FA5E80]
    FOREIGN KEY ([MaSPNhap])
    REFERENCES [dbo].[SanPham]
        ([MaSP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CT_HDNHAP__MaSPN__59FA5E80'
CREATE INDEX [IX_FK__CT_HDNHAP__MaSPN__59FA5E80]
ON [dbo].[CTHDNhap]
    ([MaSPNhap]);
GO

-- Creating foreign key on [SoHDNhap] in table 'CTHDNhap'
ALTER TABLE [dbo].[CTHDNhap]
ADD CONSTRAINT [FK_CTHDNhap_CTHD]
    FOREIGN KEY ([SoHDNhap])
    REFERENCES [dbo].[HDNhapHang]
        ([SoHDNhap])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MaSP] in table 'CTHDSanPham'
ALTER TABLE [dbo].[CTHDSanPham]
ADD CONSTRAINT [FK__CTHD_SP__MaSP__5535A963]
    FOREIGN KEY ([MaSP])
    REFERENCES [dbo].[SanPham]
        ([MaSP])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CTHD_SP__MaSP__5535A963'
CREATE INDEX [IX_FK__CTHD_SP__MaSP__5535A963]
ON [dbo].[CTHDSanPham]
    ([MaSP]);
GO

-- Creating foreign key on [SoHD] in table 'CTHDSanPham'
ALTER TABLE [dbo].[CTHDSanPham]
ADD CONSTRAINT [FK__CTHDSanPha__SoHD__06CD04F7]
    FOREIGN KEY ([SoHD])
    REFERENCES [dbo].[HoaDon]
        ([SoHD])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SoPhong] in table 'Ghe'
ALTER TABLE [dbo].[Ghe]
ADD CONSTRAINT [FK__Ghe__SoPhong__5DCAEF64]
    FOREIGN KEY ([SoPhong])
    REFERENCES [dbo].[PhongChieu]
        ([SoPhong])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Ghe__SoPhong__5DCAEF64'
CREATE INDEX [IX_FK__Ghe__SoPhong__5DCAEF64]
ON [dbo].[Ghe]
    ([SoPhong]);
GO

-- Creating foreign key on [MaGhe] in table 'Ve'
ALTER TABLE [dbo].[Ve]
ADD CONSTRAINT [FK__VE__MaGhe__5812160E]
    FOREIGN KEY ([MaGhe])
    REFERENCES [dbo].[Ghe]
        ([MaGhe])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__VE__MaGhe__5812160E'
CREATE INDEX [IX_FK__VE__MaGhe__5812160E]
ON [dbo].[Ve]
    ([MaGhe]);
GO

 
-- Creating non-clustered index for FOREIGN KEY 'FK__HDNHAPHAN__MaNVN__59063A47'
CREATE INDEX [IX_FK__HDNHAPHAN__MaNVN__59063A47]
ON [dbo].[HDNhapHang]
    ([MaNVNhap]);
GO

-- Creating foreign key on [MaKH] in table 'HoaDon'
ALTER TABLE [dbo].[HoaDon]
ADD CONSTRAINT [FK__HoaDon__MaKH__03F0984C]
    FOREIGN KEY ([MaKH])
    REFERENCES [dbo].[KhachHang]
        ([MaKH])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__HoaDon__MaKH__03F0984C'
CREATE INDEX [IX_FK__HoaDon__MaKH__03F0984C]
ON [dbo].[HoaDon]
    ([MaKH]);
GO

-- Creating foreign key on [MaNV] in table 'HoaDon'
ALTER TABLE [dbo].[HoaDon]
ADD CONSTRAINT [FK__HoaDon__MaNV__04E4BC85]
    FOREIGN KEY ([MaNV])
    REFERENCES [dbo].[NhanVien]
        ([MaNV])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__HoaDon__MaNV__04E4BC85'
CREATE INDEX [IX_FK__HoaDon__MaNV__04E4BC85]
ON [dbo].[HoaDon]
    ([MaNV]);
GO

-- Creating foreign key on [SoHD] in table 'Ve'
ALTER TABLE [dbo].[Ve]
ADD CONSTRAINT [FK_Ve_HoaDon]
    FOREIGN KEY ([SoHD])
    REFERENCES [dbo].[HoaDon]
        ([SoHD])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MaNVBao] in table 'SuCo'
ALTER TABLE [dbo].[SuCo]
ADD CONSTRAINT [FK__SUCO__MaNVBao__60A75C0F]
    FOREIGN KEY ([MaNVBao])
    REFERENCES [dbo].[NhanVien]
        ([MaNV])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SUCO__MaNVBao__60A75C0F'
CREATE INDEX [IX_FK__SUCO__MaNVBao__60A75C0F]
ON [dbo].[SuCo]
    ([MaNVBao]);
GO

-- Creating foreign key on [MaPhim] in table 'SuatChieu'
ALTER TABLE [dbo].[SuatChieu]
ADD CONSTRAINT [FK__SUATCHIEU__MaPhi__5BE2A6F2]
    FOREIGN KEY ([MaPhim])
    REFERENCES [dbo].[Phim]
        ([MaPhim])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SUATCHIEU__MaPhi__5BE2A6F2'
CREATE INDEX [IX_FK__SUATCHIEU__MaPhi__5BE2A6F2]
ON [dbo].[SuatChieu]
    ([MaPhim]);
GO

-- Creating foreign key on [SoPhongChieu] in table 'SuatChieu'
ALTER TABLE [dbo].[SuatChieu]
ADD CONSTRAINT [FK__SUATCHIEU__SoPhong__5CD6CB2B]
    FOREIGN KEY ([SoPhongChieu])
    REFERENCES [dbo].[PhongChieu]
        ([SoPhong])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SUATCHIEU__SoPhong__5CD6CB2B'
CREATE INDEX [IX_FK__SUATCHIEU__SoPhong__5CD6CB2B]
ON [dbo].[SuatChieu]
    ([SoPhongChieu]);
GO

-- Creating foreign key on [MaSC] in table 'Ve'
ALTER TABLE [dbo].[Ve]
ADD CONSTRAINT [FK__VE__MaSC__571DF1D5]
    FOREIGN KEY ([MaSC])
    REFERENCES [dbo].[SuatChieu]
        ([MaSC])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__VE__MaSC__571DF1D5'
CREATE INDEX [IX_FK__VE__MaSC__571DF1D5]
ON [dbo].[Ve]
    ([MaSC]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
INSERT INTO NhanVien VALUES ('1', 'Phạm Hùng', 'phamhung', '12345678', '0123456789', 'mercury@gmail.com', '9/15/2000', 'Nam', '11/18/2024', 'Quản lý', 1, 0)
INSERT INTO NhanVien VALUES ('2', 'Hoàng', 'vanhoang', '12345678', '0123456788', 'hoang@uit.edu.vn', '9/15/2000', 'Nam', '11/18/2024', 'Thu ngân', 0, 0)

INSERT INTO KhachHang VALUES ('0', 'Khách vãng lai', '' , '', '', '1/1/2000', 0, '1/1/2000')
INSERT INTO KhachHang VALUES ('1', 'Khách vô danh', 'Nam' , '0987654321', 'khacvodanh@gmail.com', '1/8/2001', 0, '5/1/2015')

INSERT INTO PHIM VALUES ('1', 'Hành động', '120', 'Việt Nam', '11/18/2024', 'Đây là tên đạo diễn', 'Đây là nội dung phim', '18', (CONVERT(VARBINARY(MAX), 'HelloWorld')), 'Đây là tên phim', 0)

INSERT INTO SanPham VALUES (1,'Nước ngọt', 'Sting dâu', 100, '20000', (CONVERT(VARBINARY(MAX), 'HelloWorld')), 0)
INSERT INTO SanPham VALUES (2,'Đồ ăn', 'Bắp rang', 100, '40000', (CONVERT(VARBINARY(MAX), 'HelloWorld')), 0)

-------------
Sửa database sản phẩm nhập
drop table CTHDNhap

alter table HDNhapHang add MaSPNhap int 
alter table HDNhapHang add DonGiaNhap money
alter table HDNhapHang add SoLuong int

alter table HDNhapHang add constraint fk_MASPNhap_MaSP FOREIGN KEY (MaSPNhap) REFERENCES SanPham(MaSP)
