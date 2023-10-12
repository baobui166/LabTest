USE [master]
GO
CREATE DATABASE [QLSanPham]
GO
USE [QLSanPham]
GO


CREATE TABLE [dbo].[LoaiSP]
(
    MaLoai CHAR(2) NOT NULL PRIMARY KEY,
    TenLoai NVARCHAR(30) NOT NULL
)

CREATE TABLE [dbo].[SanPham]
(
    MaSP CHAR(6) NOT NULL PRIMARY KEY,
    TenSp NVARCHAR(30),
    NgayNhap DATETIME,
    MaLoai CHAR(2) REFERENCES [dbo].[LoaiSP](MaLoai)
)

INSERT INTO [dbo].[LoaiSP] (MaLoai, TenLoai)
VALUES ('01', N'Bàn'), ('02', N'Ghế'), ('03', N'Ly');

INSERT INTO [dbo].[SanPham] (MaSP, TenSP, NgayNhap, MaLoai)
VALUES 
('SP0001', N'Bàn tròn', '2023-08-15', '01'),
('SP0002', N'Bàn vuông', '2023-05-25', '01'),
('SP0003', N'Ghế sắt', '2023-05-26', '02'),
('SP0004', N'Ly thủy tinh', '2023-05-27', '03'),
('SP0005', N'Ghế nhựa', '2023-08-10', '02');
