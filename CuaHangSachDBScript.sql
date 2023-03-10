/****** Object:  Database [CuaHangSach]    Script Date: 06/02/2023 7:12:38 PM ******/
CREATE DATABASE [CuaHangSach]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [CuaHangSach] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [CuaHangSach] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CuaHangSach] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CuaHangSach] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CuaHangSach] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CuaHangSach] SET ARITHABORT OFF 
GO
ALTER DATABASE [CuaHangSach] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CuaHangSach] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CuaHangSach] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CuaHangSach] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CuaHangSach] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CuaHangSach] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CuaHangSach] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CuaHangSach] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CuaHangSach] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CuaHangSach] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CuaHangSach] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CuaHangSach] SET  MULTI_USER 
GO
ALTER DATABASE [CuaHangSach] SET ENCRYPTION ON
GO
ALTER DATABASE [CuaHangSach] SET QUERY_STORE = ON
GO
ALTER DATABASE [CuaHangSach] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/****** Object:  User [userCHSach1]    Script Date: 06/02/2023 7:12:38 PM ******/
CREATE USER [userCHSach1] FOR LOGIN [userCHSach1] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'userCHSach1'
GO
/****** Object:  UserDefinedTableType [dbo].[KQMaSach]    Script Date: 06/02/2023 7:12:38 PM ******/
CREATE TYPE [dbo].[KQMaSach] AS TABLE(
	[MaSach] [char](6) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[KQSach]    Script Date: 06/02/2023 7:12:38 PM ******/
CREATE TYPE [dbo].[KQSach] AS TABLE(
	[MaSach] [char](6) NULL,
	[ISBN] [varchar](250) NULL,
	[TenSach] [varchar](250) NULL,
	[TacGia] [varchar](250) NULL,
	[DonGia] [int] NULL,
	[NamXuatBan] [int] NULL,
	[NhaXuatBan] [nvarchar](250) NULL,
	[MaLoai] [int] NULL,
	[Hinh] [nvarchar](250) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_split_string_to_column]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_split_string_to_column] (
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
    )
RETURNS @out_put TABLE (
    [column_id] INT IDENTITY(1, 1) NOT NULL,
    [value] NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @value NVARCHAR(MAX),
        @pos INT = 0,
        @len INT = 0

    SET @string = CASE 
            WHEN RIGHT(@string, 1) != @delimiter
                THEN @string + @delimiter
            ELSE @string
            END

    WHILE CHARINDEX(@delimiter, @string, @pos + 1) > 0
    BEGIN
        SET @len = CHARINDEX(@delimiter, @string, @pos + 1) - @pos
        SET @value = SUBSTRING(@string, @pos, @len)

        INSERT INTO @out_put ([value])
        SELECT LTRIM(RTRIM(@value)) AS [column]

        SET @pos = CHARINDEX(@delimiter, @string, @pos + @len) + 1
    END

    RETURN
END
GO
/****** Object:  Table [dbo].[CTDONHANG]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDONHANG](
	[MaDonHang] [int] NOT NULL,
	[MaSach] [char](6) NOT NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_CTDonHang] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC,
	[MaSach] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONHANG]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONHANG](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[MaNguoiDung] [int] NULL,
	[Tinh] [nvarchar](250) NULL,
	[TPQuan] [nvarchar](250) NULL,
	[DiaChiNha] [nvarchar](250) NULL,
	[SoDienThoai] [varchar](20) NULL,
	[ThanhTien] [bigint] NULL,
	[PTThanhToan] [nvarchar](50) NULL,
	[DaThanhToan] [tinyint] NULL,
	[NgayDat] [datetime] NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIOHANG]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIOHANG](
	[MaNguoiDung] [int] NOT NULL,
	[MaSach] [char](6) NOT NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_GioHang] PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC,
	[MaSach] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISACH]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISACH](
	[MaLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NGUOIDUNG]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NGUOIDUNG](
	[MaNguoiDung] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](500) NULL,
	[Email] [nvarchar](100) NULL,
	[TenDangNhap] [nvarchar](100) NULL,
	[MatKhau] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SACH]    Script Date: 06/02/2023 7:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SACH](
	[MaSach] [char](6) NOT NULL,
	[ISBN] [varchar](250) NULL,
	[TenSach] [varchar](250) NULL,
	[TacGia] [varchar](250) NULL,
	[DonGia] [int] NULL,
	[NamXuatBan] [int] NULL,
	[NhaXuatBan] [nvarchar](250) NULL,
	[MaLoai] [int] NULL,
	[Hinh] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (36, N'STK001', 3)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (36, N'STK002', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (37, N'STK003', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (38, N'STK003', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (38, N'STK005', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (39, N'STK005', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (39, N'STK011', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (40, N'STK001', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (40, N'STK003', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (41, N'STK004', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (41, N'STK006', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (42, N'STK003', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (42, N'STK013', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (43, N'STK001', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (43, N'STK012', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (44, N'STK001', 3)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (44, N'STK003', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (45, N'STK004', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (45, N'STK005', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (46, N'STK001', 3)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (46, N'STK003', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (47, N'STK003', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (47, N'STK012', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (48, N'STK002', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (48, N'STK005', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (49, N'STK002', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (49, N'STK005', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (50, N'STK003', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (50, N'STK004', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (50, N'STK007', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (51, N'STK018', 2)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (52, N'STK004', 1)
INSERT [dbo].[CTDONHANG] ([MaDonHang], [MaSach], [SoLuong]) VALUES (52, N'STK006', 2)
GO
SET IDENTITY_INSERT [dbo].[DONHANG] ON 

INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (36, 1, N'tphcm', N'quận 3', N'11 Lê Văn Sỹ', N'0987654321', 2173500, N'TheNganHang', 1, CAST(N'2022-11-04T02:35:53.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (37, 1, N'TPHCM', N'Quận 6', N'123 Nguyễn Trãi', N'0123456789', 1220472, N'TheNganHang', 0, CAST(N'2022-11-04T03:16:28.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (38, 1, N'TPHCM', N'Quận Thủ Đức', N'1 Võ Văn Ngân', N'0123456789', 1490400, N'TheNganHang', 1, CAST(N'2022-11-04T03:23:18.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (39, 1, N'TPHCM', N'Quận 3', N'21 Lê Văn Sỹ', N'0123456789', 1593072, N'TheNganHang', 1, CAST(N'2022-11-04T03:26:04.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (40, 1, N'TPHCM', N'Quận 9', N'5 Lê Văn Việt', N'0987653124', 1573200, N'TheNganHang', 1, CAST(N'2022-11-04T03:40:11.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (41, 1, N'TPHCM', N'Quận 1', N'2 Lê Lợi', N'0987675432', 2484000, N'TheNganHang', 1, CAST(N'2022-11-04T03:54:36.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (42, 1, N'TPHCM', N'Quận 5', N'2 Nguyễn Văn Bá', N'0123456789', 1552500, N'TheNganHang', 1, CAST(N'2022-11-10T20:29:11.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (43, 1, N'TPHCM', N'Q 5', N'123 Nguyen hue', N'0123456789', 1407600, N'TheNganHang', 1, CAST(N'2022-11-10T20:51:19.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (44, 1, N'TPHCM', N'Quận 1', N'1 Nguyen Hue', N'0123456789', 1490400, N'TheNganHang', 1, CAST(N'2022-11-12T19:06:29.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (45, 4, N'TPHCM', N'Quận 3', N'1 Lê Văn Sỹ', N'0123456789', 2484000, N'TheNganHang', 1, CAST(N'2022-11-12T19:46:17.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (46, 1, N'TPHCM', N'Quận 1', N'1 Nguyễn Huệ', N'0123456789', 1925100, N'TheNganHang', 1, CAST(N'2022-11-12T20:00:31.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (47, 4, N'TPHCM', N'Quận 4', N'1 Nguyễn Du', N'0123456789', 1573200, N'TheNganHang', 1, CAST(N'2022-11-13T19:12:41.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (48, 5, N'TP HCM', N'Thủ Đức', N'123 Võ Văn Ngân', N'0123456789', 2359800, N'TheNganHang', 1, CAST(N'2023-02-03T18:00:44.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (49, 5, N'TP HCM', N'Thủ Đức', N'123 Võ Văn Ngân', N'0123456789', 2359800, N'TheNganHang', 1, CAST(N'2023-02-03T18:01:02.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (50, 5, N'Bình Dương', N'Tân An', N'2 Nguyễn Du', N'0123456789', 2277000, N'TheNganHang', 1, CAST(N'2023-02-03T18:26:14.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (51, 1, N'TP HCM', N'Quận 7', N'123 Nguyễn Du', N'0123456789', 1200600, N'TheNganHang', 1, CAST(N'2023-02-04T19:18:28.000' AS DateTime))
INSERT [dbo].[DONHANG] ([MaDonHang], [MaNguoiDung], [Tinh], [TPQuan], [DiaChiNha], [SoDienThoai], [ThanhTien], [PTThanhToan], [DaThanhToan], [NgayDat]) VALUES (52, 1, N'TPHCM', N'Thủ Đức', N'123 Võ Văn Ngân', N'0123456789', 1863000, N'TheNganHang', 1, CAST(N'2023-02-06T17:40:03.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[DONHANG] OFF
GO
INSERT [dbo].[GIOHANG] ([MaNguoiDung], [MaSach], [SoLuong]) VALUES (1, N'STK002', 1)
INSERT [dbo].[GIOHANG] ([MaNguoiDung], [MaSach], [SoLuong]) VALUES (4, N'STK002', 1)
INSERT [dbo].[GIOHANG] ([MaNguoiDung], [MaSach], [SoLuong]) VALUES (5, N'STK001', 1)
INSERT [dbo].[GIOHANG] ([MaNguoiDung], [MaSach], [SoLuong]) VALUES (5, N'STK006', 1)
INSERT [dbo].[GIOHANG] ([MaNguoiDung], [MaSach], [SoLuong]) VALUES (5, N'STK009', 2)
GO
SET IDENTITY_INSERT [dbo].[LOAISACH] ON 

INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (1, N'Tiểu thuyết hư cấu')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (2, N'Phiêu lưu / Kịch tính')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (3, N'Khoa học viễn tưởng')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (4, N'Lãng mạn')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (5, N'Kinh dị')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (6, N'Sách trẻ em')
INSERT [dbo].[LOAISACH] ([MaLoai], [TenLoai]) VALUES (7, N'Tự truyện / Hồi ký')
SET IDENTITY_INSERT [dbo].[LOAISACH] OFF
GO
SET IDENTITY_INSERT [dbo].[NGUOIDUNG] ON 

INSERT [dbo].[NGUOIDUNG] ([MaNguoiDung], [HoTen], [Email], [TenDangNhap], [MatKhau]) VALUES (1, N'Vũ Nguyễn', N'vunguyen@gmail.com', N'vunguyen', N'$2a$11$YeG.5.yS0BAltbexomLlo.KArtlCE6ulcKzC2bSe3Kr9mwq0x.Rae')
INSERT [dbo].[NGUOIDUNG] ([MaNguoiDung], [HoTen], [Email], [TenDangNhap], [MatKhau]) VALUES (4, N'Hoàng Huynh', N'hoanghuynh@gmail.com', N'hoanghuynh', N'$2a$11$owAAQG.krw.wZzvHYKVGT.QsgHRuCXp6dtpPSMzUGZZOv3.wZtEd2')
INSERT [dbo].[NGUOIDUNG] ([MaNguoiDung], [HoTen], [Email], [TenDangNhap], [MatKhau]) VALUES (5, N'Tuấn Trần', N'tuantran@gmail.com', N'tuantran', N'$2a$11$vIlJ2hZNregL2Dlk/spZjePIaa0RXhDmnCX.1tOco.h2exBV/3TI.')
SET IDENTITY_INSERT [dbo].[NGUOIDUNG] OFF
GO
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK001', N'0-332-3233-4', N'The Hobbit', N'J.R.R Tolken', 351900, 1937, N'Allen & Unwin', 1, N'img-01.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK002', N'0-3113-443-4', N'The Lord of the Rings : Fellowship of the ring', N'J.R.R Tolken', 558900, 2000, N'Allen & Unwin', 1, N'img-02.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK003', N'0-113-473-8', N'The Lord of the Rings : The two towers', N'J.R.R Tolken', 434700, 2006, N'Allen & Unwin', 1, N'img-03.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK004', N'0-993-433-3', N'The Lord of the Rings : Return of the King', N'J.R.R Tolken', 621000, 2014, N'Allen & Unwin', 1, N'img-04.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK005', N'0-293-333-6', N'The Castle of Dark', N'Tanith Lee', 621000, 1987, N'Macmilla', 1, N'img-05.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK006', N'0-023-179-4', N'The Winter Players', N'Tanith Lee', 621000, 1977, N'Macmilla', 1, N'img-06.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK007', N'0-023-179-3', N'Tarzan and the forbidden city', N'Edgar Rice Burroughs', 600300, 1914, N'New English Library', 2, N'img-07.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK008', N'0-113-139-6', N'Tarzan of the Apes', N'Edgar Rice Burroughs', 414000, 1999, N'New English Library', 2, N'img-08.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK009', N'0-444-139-6', N'The Gemini Contenders', N'Robert Ludlum', 455400, 2009, N'Dial Press', 2, N'img-09.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK010', N'0-433-439-6', N'Chancellor Manuscript', N'Robert Ludlum', 621000, 1999, N'Dial Press', 2, N'img-10.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK011', N'0-430-131-6', N'Dragon Flight', N'James Clavel', 351072, 2007, N'Atheneum', 3, N'img-11.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK012', N'0-410-121-1', N'Summer Return', N'Venessa Walters', 351900, 2016, N'Penguin', 4, N'img-12.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK013', N'0-230-166-1', N'Tale of the Thief', N'Anne Rice', 558900, 2016, N'Penguin', 5, N'img-03.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK014', N'0-090-881-1', N'Prince and the Pauper', N'Mark Stevenson', 476100, 2011, N'American Pushlishing Co', 6, N'img-04.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK015', N'0-22-121-1', N'Alien', N'Ribbly Scott', 434700, 1996, N'Morpheus', 3, N'img-05.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK016', N'0-413-331-1', N'Gone Girl', N'James Clavell', 621000, 2015, N'Paramount', 2, N'img-06.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK017', N'9-330-121-1', N'All the Missing Girls', N'Megan Miranda', 621000, 2016, N'H & R', 2, N'img-07.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK018', N'0-410-9921-1', N'Empire of Storms', N'Sarah Mass', 600300, 2006, N'Pearson Plc', 1, N'img-08.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK019', N'0-413-121-1', N'Hillbilly Elegy', N'J.D Vance', 414000, 2012, N'Wiley publisher', 7, N'img-09.jpg')
INSERT [dbo].[SACH] ([MaSach], [ISBN], [TenSach], [TacGia], [DonGia], [NamXuatBan], [NhaXuatBan], [MaLoai], [Hinh]) VALUES (N'STK020', N'0-310-331-1', N'The Fire Man', N'Joe Hill', 455400, 2016, N'Scholastic', 1, N'img-10.jpg')
GO
ALTER TABLE [dbo].[CTDONHANG]  WITH CHECK ADD FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DONHANG] ([MaDonHang])
GO
ALTER TABLE [dbo].[CTDONHANG]  WITH CHECK ADD FOREIGN KEY([MaSach])
REFERENCES [dbo].[SACH] ([MaSach])
GO
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD FOREIGN KEY([MaNguoiDung])
REFERENCES [dbo].[NGUOIDUNG] ([MaNguoiDung])
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD FOREIGN KEY([MaNguoiDung])
REFERENCES [dbo].[NGUOIDUNG] ([MaNguoiDung])
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD FOREIGN KEY([MaSach])
REFERENCES [dbo].[SACH] ([MaSach])
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LOAISACH] ([MaLoai])
GO
/****** Object:  StoredProcedure [dbo].[CapNhatDaThanhToanDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CapNhatDaThanhToanDonHang](@MaDonHang int, @DaThanhToan bit, @CurrentID int output)
as
begin try
	if (exists(select * from DONHANG where MaDonHang=@MaDonHang))
	begin
		update DONHANG set DaThanhToan = @DaThanhToan where MaDonHang=@MaDonHang
		set @CurrentID=1
	end
	else
		set @CurrentID=0
end try
begin catch
	set @CurrentID=0
end catch
GO
/****** Object:  StoredProcedure [dbo].[CapNhatMatKhauNguoiDung]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CapNhatMatKhauNguoiDung](@MaNguoiDung int, 
@MatKhauMoi nvarchar(100), @CurrentID int output)
as
begin try
	if (exists(select * from NGUOIDUNG where MaNguoiDung=@MaNguoiDung))
	begin
		update NGUOIDUNG set MatKhau = @MatKhauMoi where MaNguoiDung=@MaNguoiDung
		set @CurrentID=1
	end
	else
		set @CurrentID=0
end try
begin catch
	set @CurrentID=0
end catch
GO
/****** Object:  StoredProcedure [dbo].[CapNhatThongTinNguoiDung]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CapNhatThongTinNguoiDung](@MaNguoiDung int, @HoTen nvarchar(500), @Email nvarchar(100), @CurrentID int output)
as
begin try
	if (exists(select * from [dbo].[NGUOIDUNG] where MaNguoiDung=@MaNguoiDung))
	begin
		update [dbo].[NGUOIDUNG] set HoTen=@HoTen, Email=@Email where MaNguoiDung=@MaNguoiDung;
		set @CurrentID=1;
	end
	else
		set @CurrentID=0
end try
begin catch
	set @CurrentID=0
end catch;
GO
/****** Object:  StoredProcedure [dbo].[LayChiTietDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LayChiTietDonHang](@MaDonHang int)
as
select * from CTDONHANG ctdh, SACH s where MaDonHang = @MaDonHang and ctdh.MaSach = s.MaSach
GO
/****** Object:  StoredProcedure [dbo].[LayDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LayDonHang] (@MaDonHang int)
as
select * from DONHANG where MaDonHang = @MaDonHang
GO
/****** Object:  StoredProcedure [dbo].[LayDSLoaiSach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayDSLoaiSach]
as
select * from LOAISACH
GO
/****** Object:  StoredProcedure [dbo].[LayDSSach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayDSSach]
as
select * from SACH
GO
/****** Object:  StoredProcedure [dbo].[LayDSSachTheoLoai]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayDSSachTheoLoai] @MaLoai int
as
Select * from SACH where MaLoai = @MaLoai
GO
/****** Object:  StoredProcedure [dbo].[LayGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayGioHang](@MaNguoiDung int)
as
select * from GIOHANG where MaNguoiDung = @MaNguoiDung
GO
/****** Object:  StoredProcedure [dbo].[LayGioHangChiTiet]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayGioHangChiTiet](@MaNguoiDung int)
as
select * from GIOHANG gh, SACH s where MaNguoiDung = @MaNguoiDung and gh.MaSach = s.MaSach
GO
/****** Object:  StoredProcedure [dbo].[LayLichSuDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LayLichSuDonHang](@MaNguoiDung int)
as
select * from DONHANG where MaNguoiDung = @MaNguoiDung order by NgayDat DESC, MaDonHang DESC
GO
/****** Object:  StoredProcedure [dbo].[LayLoaiSach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayLoaiSach] @MaLoai int
as
select * from LOAISACH where MaLoai = @MaLoai
GO
/****** Object:  StoredProcedure [dbo].[LayNguoiDung]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayNguoiDung] @TenDangNhap nvarchar(100)
as
begin
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'select * from NGUOIDUNG where TenDangNhap = @TenDangNhap';
	SET @params = N'@TenDangNhap nvarchar(100)';
    EXECUTE sp_executesql @sqlcmd, @params, @TenDangNhap;
end
GO
/****** Object:  StoredProcedure [dbo].[LayNhieuSach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LayNhieuSach] @DSMaSach nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'select * from SACH where MaSach in (select value from fn_split_string_to_column(@DSMaSach,'','') )';
	SET @params = N'@DSMaSach nvarchar(max)';
    EXECUTE sp_executesql @sqlcmd, @params, @DSMaSach;
END
GO
/****** Object:  StoredProcedure [dbo].[LaySach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LaySach] @MaSach varchar(6)
as
begin
DECLARE @sqlcmd NVARCHAR(MAX);
DECLARE @params NVARCHAR(MAX);
SET @sqlcmd = N'select * from SACH where MaSach = @MaSach';
SET @params = N'@MaSach varchar(6)';
EXECUTE sp_executesql @sqlcmd, @params, @MaSach;
end
GO
/****** Object:  StoredProcedure [dbo].[LaySachDeXuat]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LaySachDeXuat] @MaNguoiDung int
as
begin

-- Sach thuoc the loai mua nhieu nhat chua dat hang
Declare @MaLoaiNhieuNhat int
Declare @tableSachMaLoaiNhieuNhat as KQMaSach

set @MaLoaiNhieuNhat = (select TOP 1 MaLoai
from CTDONHANG ctdh, SACH s
where MaDonHang in (select MaDonHang from DONHANG where MaNguoiDung=@MaNguoiDung) and ctdh.MaSach = s.MaSach
group by MaLoai
order by Count(MaLoai) DESC) 

INSERT INTO @tableSachMaLoaiNhieuNhat
select TOP 2 MaSach
from SACH
where MaLoai = @MaLoaiNhieuNhat and MaSach not in (
select distinct MaSach
from CTDONHANG
where MaDonHang in (select MaDonHang from DONHANG where MaNguoiDung=@MaNguoiDung))

-- The loai chua mua
Declare @tableSachLoaiChuaMua as KQMaSach
INSERT INTO @tableSachLoaiChuaMua
select TOP 2  MaSach from SACH where MaLoai not in (
select MaLoai
from CTDONHANG ctdh, SACH s
where MaDonHang in (select MaDonHang from DONHANG where MaNguoiDung=@MaNguoiDung) and ctdh.MaSach = s.MaSach)

-- Sach con lai
Declare @tableSachConLai as KQMaSach
INSERT INTO @tableSachConLai
select MaSach from SACH where MaSach not in (
select distinct MaSach
from CTDONHANG
where MaDonHang in (select MaDonHang from DONHANG where MaNguoiDung=@MaNguoiDung) 
union
select MaSach
from (select * from @tableSachMaLoaiNhieuNhat union select * from @tableSachLoaiChuaMua) as table_temp)

-- Ket qua
Declare @tableResult as KQMaSach
INSERT INTO @tableResult
select * from @tableSachMaLoaiNhieuNhat
union all
select * from @tableSachLoaiChuaMua
union all
select * from @tableSachConLai

select TOP 6 rs.MaSach,[ISBN],[TenSach],[TacGia],[DonGia],[NamXuatBan],[NhaXuatBan],[MaLoai],[Hinh]
from @tableResult rs, SACH s where rs.MaSach = s.MaSach
end
GO
/****** Object:  StoredProcedure [dbo].[LaySachGiaUuDai]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LaySachGiaUuDai]
as
select top 10 * from SACH order by DONGIA
GO
/****** Object:  StoredProcedure [dbo].[LaySachMoi]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LaySachMoi]
as
select top 10 * from SACH order by NamXuatBan DESC
GO
/****** Object:  StoredProcedure [dbo].[LayThongTinNhieuSachGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[LayThongTinNhieuSachGioHang] (@MaNguoiDung int, @MaSach nvarchar(max))
as
begin
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'select *
	from GIOHANG gh, SACH s
	where gh.MaSach = s.MaSach
	and MaNguoiDung = @MaNguoiDung
	and gh.MaSach in (select value from fn_split_string_to_column(@MaSach,'','') )'
	SET @params = N'@MaNguoiDung int, @MaSach nvarchar(max)';
    EXECUTE sp_executesql @sqlcmd, @params, @MaNguoiDung, @MaSach;
end
GO
/****** Object:  StoredProcedure [dbo].[LayThongTinSachGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LayThongTinSachGioHang] (@MaNguoiDung int, @MaSach char(6))
as
begin
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'select * from GIOHANG gh, SACH s where gh.MaSach = @MaSach and gh.MaSach = s.MaSach and MaNguoiDung = @MaNguoiDung'
	SET @params = N'@MaNguoiDung int, @MaSach char(6)';
    EXECUTE sp_executesql @sqlcmd, @params, @MaNguoiDung, @MaSach;
end
GO
/****** Object:  StoredProcedure [dbo].[ThemCTDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ThemCTDonHang](@MaDonHang int, @MaSach char(6), @SoLuong int, @CurrentID int output)
as
begin try
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'insert into CTDONHANG values (@MaDonHang, @MaSach, @SoLuong)'
	SET @params = N'@MaDonHang int, @MaSach char(6), @SoLuong int';
    EXECUTE sp_executesql @sqlcmd, @params, @MaDonHang, @MaSach, @SoLuong;
	set @CurrentID=1
end try
begin catch
	set @CurrentID=0
end catch
GO
/****** Object:  StoredProcedure [dbo].[ThemDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ThemDonHang](@MaNguoiDung int, @Tinh nvarchar(250), @TPQuan nvarchar(250), @DiaChiNha nvarchar(250), @SoDienThoai varchar(20),  @ThanhTien bigint, @PTThanhToan nvarchar(50), @DaThanhToan tinyint, @NgayDat datetime, @CurrentID int output)
as
begin try
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'insert into DONHANG(MaNguoiDung,Tinh,TPQuan,DiaChiNha,SoDienThoai, ThanhTien, PTThanhToan, DaThanhToan, NgayDat) values (@MaNguoiDung, @Tinh, @TPQuan, @DiaChiNha, @SoDienThoai, @ThanhTien, @PTThanhToan, @DaThanhToan, @NgayDat)'
	SET @params = N'@MaNguoiDung int, @Tinh nvarchar(250), @TPQuan nvarchar(250), @DiaChiNha nvarchar(250), @SoDienThoai varchar(20),  @ThanhTien bigint, @PTThanhToan nvarchar(50), @DaThanhToan tinyint, @NgayDat datetime';
    EXECUTE sp_executesql @sqlcmd, @params, @MaNguoiDung, @Tinh, @TPQuan, @DiaChiNha, @SoDienThoai, @ThanhTien, @PTThanhToan, @DaThanhToan, @NgayDat;	
	set @CurrentID=@@IDENTITY;
end try
begin catch
	set @CurrentID=0;
end catch
GO
/****** Object:  StoredProcedure [dbo].[ThemNguoiDung]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ThemNguoiDung] @HoTen nvarchar(500), 
@Email nvarchar(100), @TenDangNhap nvarchar(100), @MatKhau nvarchar(100),
@CurrentID int output
as
begin try
	DECLARE @CheckQuery  NVARCHAR(1000) = N'select * from NGUOIDUNG where TenDangNhap=@TenDangNhap',
        @rowcnt INT
	DECLARE @CheckParams NVARCHAR(MAX) = N'@TenDangNhap nvarchar(100)';
	EXEC Sp_executesql @CheckQuery, @CheckParams, @TenDangNhap;

	SELECT @rowcnt = @@ROWCOUNT

	if(@rowcnt > 0)
		begin
			set @CurrentID=0
			return
		end

	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd = N'insert into NGUOIDUNG(HoTen, Email, TenDangNhap, MatKhau) values (@HoTen, @Email, @TenDangNhap, @MatKhau)'
	SET @params = N'@HoTen nvarchar(500), @Email nvarchar(100), @TenDangNhap nvarchar(100), @MatKhau nvarchar(100)';
    EXECUTE sp_executesql @sqlcmd, @params, @HoTen, @Email, @TenDangNhap, @MatKhau;	
	set @CurrentID=@@IDENTITY
end try
begin catch
		set @CurrentID=0
end catch
GO
/****** Object:  StoredProcedure [dbo].[ThemSachVaoGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ThemSachVaoGioHang](@MaNguoiDung int, @MaSach char(6), @SoLuong int, @CurrentID int output)
as
begin try
DECLARE @CheckQuery  NVARCHAR(1000) = N'select * from GIOHANG where MaNguoiDung=@MaNguoiDung and MaSach = @MaSach',
        @rowcnt INT
	DECLARE @CheckParams NVARCHAR(MAX) = N'@MaNguoiDung int, @MaSach char(6)';
	EXEC Sp_executesql @CheckQuery, @CheckParams, @MaNguoiDung, @MaSach;

	SELECT @rowcnt = @@ROWCOUNT

	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX) = N'@MaNguoiDung int, @MaSach char(6), @SoLuong int';
	
	if(@rowcnt > 0)
		SET @sqlcmd = N'update GIOHANG set SoLuong = @SoLuong where MaNguoiDung=@MaNguoiDung and MaSach = @MaSach'
	else
		SET @sqlcmd = N'insert into GIOHANG(MaNguoiDung,MaSach,SoLuong) values (@MaNguoiDung,@MaSach,@SoLuong)'
	EXECUTE sp_executesql @sqlcmd, @params, @MaNguoiDung, @MaSach, @SoLuong;	
	set @CurrentID=1
end try
begin catch
	set @CurrentID=0
end catch
GO
/****** Object:  StoredProcedure [dbo].[TimKiemSach]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[TimKiemSach] @TuTimKiem nvarchar(500)
as
begin
DECLARE @sqlcmd NVARCHAR(MAX) = N'select * from SACH where TenSach like ''%'' + @TuTimKiem + ''%''' ;
DECLARE @params NVARCHAR(MAX) = N'@TuTimKiem nvarchar(500)';
EXECUTE sp_executesql @sqlcmd, @params, @TuTimKiem;
end
GO
/****** Object:  StoredProcedure [dbo].[XoaCTDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[XoaCTDonHang](@MaDonHang int, @MaSach char(6))
as
delete from CTDONHANG where MaDonHang=@MaDonHang and MaSach = @MaSach
GO
/****** Object:  StoredProcedure [dbo].[XoaDonHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[XoaDonHang](@MaDonHang int)
as
delete from DONHANG where MaDonHang=@MaDonHang
GO
/****** Object:  StoredProcedure [dbo].[XoaNguoiDung]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[XoaNguoiDung] @TenDangNhap nvarchar(100)
as
begin
DECLARE @sqlcmd NVARCHAR(MAX) = N'Delete from NGUOIDUNG where TenDangNhap = @TenDangNhap'
DECLARE @params NVARCHAR(MAX) = N'@TenDangNhap nvarchar(100)';
EXECUTE sp_executesql @sqlcmd, @params, @TenDangNhap;
end
GO
/****** Object:  StoredProcedure [dbo].[XoaNhieuSachGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[XoaNhieuSachGioHang] (@MaNguoiDung int, @MaSach nvarchar(max), @CurrentID int output)
as
begin try
	DECLARE @sqlcmd NVARCHAR(MAX);
    DECLARE @params NVARCHAR(MAX);
	SET @sqlcmd =N'delete from GIOHANG where MaNguoiDung = @MaNguoiDung and MaSach in (select value from fn_split_string_to_column(@MaSach,'','') )'
	SET @params = N'@MaNguoiDung int, @MaSach nvarchar(max)';
    EXECUTE sp_executesql @sqlcmd, @params, @MaNguoiDung, @MaSach;
	set @CurrentID = 1
end try
begin catch
	set @CurrentID = 0
end catch
GO
/****** Object:  StoredProcedure [dbo].[XoaSachGioHang]    Script Date: 06/02/2023 7:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[XoaSachGioHang](@MaNguoiDung int, @MaSach char(6), @CurrentID int output)
as
begin try
	delete from GIOHANG where MaNguoiDung=@MaNguoiDung and MaSach = @MaSach
	set @CurrentID=1
end try
begin catch
	set @CurrentID=0
end catch
GO
ALTER DATABASE [CuaHangSach] SET  READ_WRITE 
GO
