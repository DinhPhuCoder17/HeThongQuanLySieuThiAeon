--drop database QuanLySieuThiAEON
go
create database QuanLySieuThiAEON
use QuanLySieuThiAEON
go


CREATE TABLE Nhanvien (
    Manhanvien varchar(10) CONSTRAINT PK_Nhanvien PRIMARY KEY,
    Hoten NVARCHAR(100),
    CCCD VARCHAR(20),
    Ngaysinh DATE,
    Gioitinh NVARCHAR(10),
    Diachi NVARCHAR(255),
    Sodienthoai VARCHAR(15)
);

CREATE TABLE Quanly (
    Manhanvien varchar(10) CONSTRAINT PK_Quanly PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(50),
    CONSTRAINT FK_Quanly_Nhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien)
);

CREATE TABLE HD_Nhaphang (
    Sohd varchar(10) CONSTRAINT PK_HD_Nhaphang PRIMARY KEY,
    Ngaydat DATE,
    Trangthai NVARCHAR(50),
    Thanhtien DECIMAL(18, 2),
	Soluong INT
);

CREATE TABLE QuanlyKho (
    Manhanvien varchar(10) CONSTRAINT PK_QuanlyKho PRIMARY KEY,
    CONSTRAINT FK_QuanlyKho_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Quanly(Manhanvien),
);

CREATE TABLE Calam (
    Macalam varchar(10) CONSTRAINT PK_Calam PRIMARY KEY,
    Tencalam NVARCHAR(100),
    ThoigianBD datetime,
    ThoigianKT datetime,
	Soluong int
	CONSTRAINT CK_Thoigian CHECK (ThoigianKT > ThoigianBD)
);

CREATE TABLE QuanlyTCNS (
    Manhanvien varchar(10) CONSTRAINT PK_QuanlyTCNS PRIMARY KEY,
    CONSTRAINT FK_QuanlyTCNS_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Quanly(Manhanvien)
);

CREATE TABLE Chamcong (
    ID varchar(10) CONSTRAINT PK_Chamcong PRIMARY KEY,
    ThoigianCN DATE,
    Checkin TIME,
    Checkout TIME,
	Trangthai nvarchar(100),
    Socong float,
    Macalam varchar(10),
    Manhanvien varchar(10),
    CONSTRAINT FK_Chamcong_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien),
    CONSTRAINT FK_Chamcong_Macalam FOREIGN KEY (Macalam) REFERENCES Calam(Macalam)
);

CREATE TABLE Nhacungcap (
    MaNCC varchar(10) CONSTRAINT PK_Nhacungcap PRIMARY KEY,
    TenNCC NVARCHAR(255),
    Diachi NVARCHAR(255),
    Masothue VARCHAR(20),
    Sodienthoai VARCHAR(15)
);

CREATE TABLE Hanghoa (
    Mahanghoa varchar(10) CONSTRAINT PK_Hanghoa PRIMARY KEY,
    Tenhanghoa NVARCHAR(255),
    Tiennhap DECIMAL(18,2),
    Tendanhmuc NVARCHAR(100),
    Tienban DECIMAL(18,2),
	ImageData varbinary(max),
	Soluong INT,
    Uudai NVARCHAR(255),
    MaNCC varchar(10),
    CONSTRAINT FK_Hanghoa_MaNCC FOREIGN KEY (MaNCC) REFERENCES Nhacungcap(MaNCC),
);

--CREATE TABLE Hansudung (
 --   Malo varchar(10) constraint PK_HSD PRIMARY KEY,
   -- Ngaysanxuat datetime,
   -- Hansudung datetime
--);

--CREATE TABLE HSD_HH (
	--Malo varchar(10) constraint FK_HSDHH_Malo_Hansudung Foreign Key references Hansudung(Malo),
--	Mahanghoa varchar(10) constraint FK_HSDHH_Mahanghoa_HH Foreign Key references Hanghoa(Mahanghoa),
--);

CREATE TABLE HD_HH (
    Mahanghoa varchar(10),
    Sohd varchar(10),
	Ngaynhap date,
	Soluongdat INT,
	Soluongnhan INT,
	Hanthanhtoan DATE,
	Ngaysanxuat DATE,
	Hansudung DATE
    CONSTRAINT PK_HD_HH PRIMARY KEY (Mahanghoa, Sohd),
    CONSTRAINT FK_HD_HH_Mahanghoa FOREIGN KEY (Mahanghoa) REFERENCES Hanghoa(Mahanghoa),
    CONSTRAINT FK_HD_HH_Sohd FOREIGN KEY (Sohd) REFERENCES HD_Nhaphang(Sohd)
);

CREATE TABLE Khachhang (
    Sodienthoai varchar(15) CONSTRAINT PK_Khachhang PRIMARY KEY,
    Hoten NVARCHAR(100),
    Diachi NVARCHAR(255),
    Diemthuong INT,
    Gioitinh NVARCHAR(10),
	Hang nvarchar(50)
);

CREATE TABLE Hoadonbanhang (
    Mahoadon varchar(10) CONSTRAINT PK_Hoadonbanhang PRIMARY KEY,
    Thoigianban DATETIME,
    Manhanvien varchar(10),
    Sodienthoai varchar(15),
    CONSTRAINT FK_Hoadonbanhang_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien),
    CONSTRAINT FK_Hoadonbanhang_Makhachhang FOREIGN KEY (Sodienthoai) REFERENCES Khachhang(Sodienthoai)
);

CREATE TABLE HH_HDBH (
    Mahanghoa varchar(10),
    Mahoadon varchar(10),
	Soluong INT,
    CONSTRAINT PK_HH_HDBH PRIMARY KEY (Mahanghoa, Mahoadon),
    CONSTRAINT FK_HH_HDBH_Mahanghoa FOREIGN KEY (Mahanghoa) REFERENCES Hanghoa(Mahanghoa),
    CONSTRAINT FK_HH_HDBH_Mahoadon FOREIGN KEY (Mahoadon) REFERENCES Hoadonbanhang(Mahoadon)
);

CREATE TABLE Batbuoc(
	Macalam varchar(10),
	Manhanvien varchar(10),
	CONSTRAINT PK_Batbuoc PRIMARY KEY (Macalam, Manhanvien),
    CONSTRAINT FK_Batbuoc_Macalam FOREIGN KEY (Macalam) REFERENCES Calam(Macalam),
    CONSTRAINT FK_Batbuoc_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien)
);




--Nha Cung Cap--
INSERT INTO Nhacungcap (MaNCC, TenNCC, Diachi, Masothue, Sodienthoai)
VALUES ('NCC001', N'Vinamilk', N'10 Tân Trào, Quận 7, TP.HCM', '0300588569', '02854155555');
--End Nha Cung Cap--

--Hang Hoa--
INSERT INTO Hanghoa (Mahanghoa, Tenhanghoa, Tiennhap, Tendanhmuc, Tienban, ImageData, Soluong, Uudai, MaNCC)  
VALUES 
('HH0001', N'Sữa tươi Vinamilk 1L', 25000, N'Sữa tươi', 30000,  (SELECT * FROM OPENROWSET(BULK 'C:\Users\ADMIN\Desktop\Game\Công Nghệ Phần Mềm\Siêu Thị Aeon\Cơ sở dữ liệu\image\Sua1lVinamilk.jpg', SINGLE_BLOB) AS ImageFile), 0, N'Giảm 5% khi mua 2 hộp', 'NCC001'),
('HH0002', N'Sữa đặc Ông Thọ 380g', 20000, N'Sữa đặc', 25000, (SELECT * FROM OPENROWSET(BULK 'C:\Users\ADMIN\Desktop\Game\Công Nghệ Phần Mềm\Siêu Thị Aeon\Cơ sở dữ liệu\image\Sua1lVinamilk.jpg', SINGLE_BLOB) AS ImageFile), 0, N'Mua 3 tặng 1', 'NCC001'),
('HH0003', N'Yoghurt Vinamilk Có Đường 100g', 5000, N'Sữa chua', 7000, (SELECT * FROM OPENROWSET(BULK 'C:\Users\ADMIN\Desktop\Game\Công Nghệ Phần Mềm\Siêu Thị Aeon\Cơ sở dữ liệu\image\Sua1lVinamilk.jpg', SINGLE_BLOB) AS ImageFile), 0, N'Giảm 10% khi mua 10 hộp', 'NCC001'),
('HH0004', N'Sữa bột Vinamilk Dielac Alpha 900g', 180000, N'Sữa bột', 210000, (SELECT * FROM OPENROWSET(BULK 'C:\Users\ADMIN\Desktop\Game\Công Nghệ Phần Mềm\Siêu Thị Aeon\Cơ sở dữ liệu\image\Sua1lVinamilk.jpg', SINGLE_BLOB) AS ImageFile), 0, N'Giảm 20.000đ khi mua trên 2 hộp', 'NCC001'),
('HH0005', N'Sữa hạt Vinamilk Óc Chó 180ml', 12000, N'Sữa hạt', 15000, (SELECT * FROM OPENROWSET(BULK 'C:\Users\ADMIN\Desktop\Game\Công Nghệ Phần Mềm\Siêu Thị Aeon\Cơ sở dữ liệu\image\Sua1lVinamilk.jpg', SINGLE_BLOB) AS ImageFile), 0, N'Mua 5 tặng 1', 'NCC001');
--Hang Hoa--

--HD Nhap Hang---
Insert into HD_Nhaphang values('01', '01/02/2005', N'Chưa xử lý', 100000, 10000)
Insert into HD_Nhaphang values('02', '01/02/2005', N'Chưa xử lý', 100000, 10000)
--HD Nhap Hang---

--HH-HD--
Insert into HD_HH values
('HH002', '01', GETDATE(), 1, 0),
('HH001', '01', GETDATE(), 10, 0),
('HH005', '01', GETDATE(), 5, 0)

Insert into HD_HH values
('HH002', '02', GETDATE(), 300, 0),
('HH001', '02', GETDATE(), 10, 0),
('HH005', '02', GETDATE(), 100, 0)
--End HH-HD--

--Nhanvien--
INSERT INTO Nhanvien (Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai)
VALUES 
('NV001', N'Nguyễn Văn An', '123456789012', '1990-05-15', N'Nam', N'123 Đường Láng, Hà Nội', '0987654321'),
('NV002', N'Trần Thị Bình', '987654321098', '1995-08-20', N'Nữ', N'45 Lê Lợi, TP.HCM', '0912345678'),
('NV003', N'Lê Văn Cường', '456789123456', '1988-12-01', N'Nam', N'78 Hùng Vương, Đà Nẵng', '0935123456'),
('NV004', N'Phạm Thị Dung', '321654987123', '1993-03-10', N'Nữ', N'56 Trần Phú, Nha Trang', '0908765432'),
('NV005', N'Hoàng Văn Em', '654321789456', '1997-07-25', N'Nam', N'89 Nguyễn Huệ, Huế', '0978123456');
--End Nhanvien--

--Calam--
INSERT INTO Calam (Macalam, Tencalam, ThoigianBD, ThoigianKT)  
VALUES  
    ('CL001', N'Ca sáng', '2025-03-06 06:00:00', '2025-03-06 14:00:00', 2),  
    ('CL002', N'Ca chiều', '2025-03-06 14:00:00', '2025-03-06 22:00:00', 2),  
    ('CL003', N'Ca đêm', '2025-03-06 22:00:00', '2025-03-07 06:00:00', 2);  
--End Calam-


--Bat buoc--
Insert into Batbuoc values 
('CL001', 'NV001')
Insert into Batbuoc values ('CL001', 'NV002')
Insert into Batbuoc values  ('CL001', 'NV003')
Insert into Batbuoc values  ('CL001', 'NV005')
--End Bat buoc--

--Chamcong--
INSERT INTO Chamcong (ID, ThoigianCN, Checkin, Checkout, Trangthai, Socong, Macalam, Manhanvien)  
VALUES  
('CC002', '2025-03-06', '08:00:00', '17:00:00', N'Đã chấm công', 1.0, 'CL001', 'NV005')
--End Chamcong--

--Hoadonbanhang--
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai)
VALUES 
('HD001', '2025-03-06 10:30:00', 'NV001', null),
('HD002', '2025-03-06 11:00:00', 'NV002', null),
('HD003', '2025-03-06 14:15:00', 'NV003', null);
--End Hoadonbanhang--


--HH_HDBH--
Insert into HH_HDBH values ('HH001', 'HD001', 5)
Insert into HH_HDBH values ('HH002', 'HD001', 200)
--End HH_HDBH--





--Trigger--
--Trigger doi trang thai nhap hang, cap nhat so luong ton kho--
go
Create trigger tg_TTNhapHang
on HD_HH
For Update
As
Begin
	--Doi trang thai--
	if exists(
		Select *
		From HD_HH
		Where Soluongnhan != Soluongdat
	)
	Begin
		Update HD_Nhaphang set Trangthai = N'Chưa xử lý'
	End
	Else
	Begin
		Update HD_Nhaphang set Trangthai = N'Đã xử lý'
	End

	--Them so luong trong kho--
	Declare @soluongbd int, @soluongmoi int
	Select @soluongbd = Soluongnhan From deleted
	Select @soluongmoi = Soluongnhan From inserted

	UPDATE Hanghoa
    SET Soluong = Soluong + (@soluongmoi - @soluongbd)
    FROM Hanghoa hh
    JOIN deleted d ON hh.Mahanghoa = d.Mahanghoa
End
-- End Trigger doi trang thai nhap hang, cap nhat so luong ton kho--

--Trigger tranh nhan vien cham cong 2 lan--
go
Create trigger tg_ChamCong
on Chamcong
For Insert
As
Begin
	--Kiem tra nhan vien da cham cong chua--
	if (
		Select count(*)
		From Chamcong join Inserted i on Chamcong.Macalam = i.Macalam
		Where Chamcong.Manhanvien = i.Manhanvien
		Group By Chamcong.Macalam, Chamcong.Manhanvien
	) >= 2
	Begin
		print(N'Nhân viên đã chấm công ca này')
		rollback tran
		return
	End

	--Kiem tra nhan vien co duoc xep ca do khong--
	if not exists (
		Select 1
		From inserted i join Batbuoc bb
		on i.Macalam = bb.Macalam and i.Manhanvien = bb.Manhanvien
	)
	Begin
		print(N'Nhân viên không được phân công ca này')
		rollback tran
	End	
End
--Trigger Cham cong--

--Trigger bang Batbuoc--
go
Create trigger tg_BB
on Batbuoc
for insert
As
Begin
	--Khong duoc them khi vuot qua so luong ca--
	Declare @macalam varchar(10), @soluong int
	Select @macalam = (select Macalam from inserted)
	if (
		Select count(*)
		From inserted i join Batbuoc bb
		on i.Macalam = bb.Macalam
	) > (
		Select Soluong
		From Calam join inserted i
		on i.Macalam = Calam.Macalam
	)
	Begin
		print(N'Đã vượt quá số lượng người trong ca')
		rollback tran
	End
End
--End Trigger bang Batbuoc--

--Trigger bang HH_HDBH--
Create trigger tg_HH_HDBH
on HH_HDBH
for Insert
As
Begin
	--Kiem tra so luong hang hoa khong lon hon ton kho --
	if exists (
		Select 1
		From inserted i join Hanghoa hh 
		on i.Mahanghoa = hh.Mahanghoa
		Where i.Soluong > hh.Soluong
	)
	Begin
		print(N'Số lượng hàng hóa không hợp lệ - Nhiều hơn số lượng trong kho')
		rollback tran
		return
	End

	--Kiem tra so luong hang hoa khong am--
	if exists (
		Select 1
		From inserted i
		Where i.Soluong < 0
	)
	Begin
		print(N'Số lượng hàng hóa không hợp lệ - Giá trị âm')
		rollback tran
		return
	End

	Declare @soluongban int
	Select @soluongban = soluong From inserted
	Update Hanghoa set Soluong = Soluong - @soluongban
	Where Mahanghoa = (Select Mahanghoa from inserted)
End
--End Trigger bang HH_HDBH--

----------------------------Procedure-----------------------------
--Procedure thêm mã cho nhân viên mới--
go
create proc themMaNhanvien 
	@Hoten NVARCHAR(100),
    @CCCD VARCHAR(20),
    @Ngaysinh DATE,
    @Gioitinh NVARCHAR(10),
    @Diachi NVARCHAR(255),
    @Sodienthoai VARCHAR(15)
As
Begin 
Declare @newMaNhanvien varchar(10);
Declare @maxMaNhanvien varchar(10);
Declare @soMoi int;
	--Lấy mã nhân viên lớn nhất hiện tại
Select @maxMaNhanvien = MAX(Manhanvien) from Nhanvien;
	--Nếu chưa có ai, mã đầu tiên là NV0001
	If @maxMaNhanvien is null
		Set @newMaNhanvien = 'SV0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@MaxMaNhanvien, 3, 4) AS INT) + 1;
	Set @newMaNhanvien = 'NV' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into Nhanvien(Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai)
	Values (@newMaNhanvien, @Hoten, @CCCD, @Ngaysinh, @Gioitinh, @Diachi, @Sodienthoai);
	print 'adding successfully: ' + @newMaNhanvien;
End;

--Procedure thêm mã cho hàng hoá mới--
go
create proc themMaHanghoa 
	@Tenhanghoa NVARCHAR(255),
    @Tiennhap DECIMAL(18,2),
    @Tendanhmuc NVARCHAR(100),
    @Tienban DECIMAL(18,2),
	@ImageData varbinary(max),
	@Soluong INT,
    @Uudai NVARCHAR(255),
    @MaNCC varchar(10)
As
Begin 
Declare @newMaHanghoa varchar(10);
Declare @maxMaHanghoa varchar(10);
Declare @soMoi int;
	--Lấy mã hàng hoá nhất hiện tại
Select @maxMaHanghoa = MAX(Mahanghoa) from Hanghoa;
	--Nếu chưa có hàng hoá, mã đầu tiên là HH0001
If @maxMaHanghoa is null
	Set @newMaHanghoa = 'HH0001';
	--Tiến hành tạo mã mới
Else
Begin
	Set @soMoi = cast(substring(@maxMaHanghoa, 3, 4) AS INT) + 1;
	Set @newMaHanghoa = 'HH' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
INSERT INTO Hanghoa (Mahanghoa, Tenhanghoa, Tiennhap, Tendanhmuc, Tienban, ImageData, Soluong, Uudai, MaNCC)  
Values (@newMaHanghoa, @Tenhanghoa, @Tiennhap, @Tendanhmuc, @Tienban, @ImageData, @Soluong, @Uudai, @MaNCC);
print 'adding successfully: ' + @newMaHanghoa;
End;

--Procedure thêm mã nhà cung cấp mới--
go
create proc themMaNhacungcap
	@TenNCC NVARCHAR(255),
    @Diachi NVARCHAR(255),
    @Masothue VARCHAR(20),
    @Sodienthoai VARCHAR(15)
As
Begin 
Declare @newMaNCC varchar(10);
Declare @maxMaNCC varchar(10);
Declare @soMoi int;
	--Lấy mã nhà cung cấp lớn nhất hiện tại
Select @maxMaNCC = MAX(MaNCC) from Nhacungcap;
	--Nếu chưa có nhà cung cấp, mã đầu tiên là NC0001
	If @maxMaNCC is null
		Set @newMaNCC = 'NC0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@maxMaNCC, 3, 4) AS INT) + 1;
	Set @newMaNCC = 'NC' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into Nhacungcap(MaNCC, TenNCC, Diachi, Masothue, Sodienthoai)
	Values (@newMaNCC, @TenNCC, @Diachi, @Masothue, @Sodienthoai);
	print 'adding successfully: ' + @newMaNCC;
End;

--Procedure thêm mã ca làm mới--
go
create proc themMacalam
	@Tencalam NVARCHAR(100),
    @ThoigianBD datetime,
    @ThoigianKT datetime,
	@Soluong INT
As
Begin 
Declare @newMacalam varchar(10);
Declare @maxMacalam varchar(10);
Declare @soMoi int;
	--Lấy mã ca làm lớn nhất hiện tại
Select @maxMacalam = MAX(Macalam) from Calam;
	--Nếu chưa có ca làm, mã đầu tiên là CL0001
	If @maxMacalam is null
		Set @newMacalam = 'CL0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@maxMacalam, 3, 4) AS INT) + 1;
	Set @newMacalam = 'CL' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into Calam(Macalam, Tencalam, ThoigianBD, ThoigianKT, Soluong)
	Values (@newMacalam, @Tencalam, @ThoigianBD, @ThoigianKT, @Soluong);
	print 'adding successfully: ' + @newMacalam;
End;

--Procedure thêm mã hoá đơn bán hàng--
go
create proc themMaHDBH
	@Thoigianban DATETIME,
    @Manhanvien varchar(10),
    @Sodienthoai varchar(15)
As
Begin 
Declare @newMaHDBH varchar(10);
Declare @maxMaHDBH varchar(10);
Declare @soMoi int;
	--Lấy mã hoá đơn bán hàng lớn nhất hiện tại
Select @maxMaHDBH = MAX(Macalam) from Calam;
	--Nếu chưa có hoá đơn bán hàng, mã đầu tiên là BH0001
	If @maxMaHDBH is null
		Set @newMaHDBH = 'BH0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@maxMaHDBH, 3, 4) AS INT) + 1;
	Set @newMaHDBH = 'BH' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into Hoadonbanhang(Mahoadon, Thoigianban, Manhanvien, Sodienthoai)
	Values (@newMaHDBH, @Thoigianban, @Manhanvien, @Sodienthoai);
	print 'adding successfully: ' + @newMaHDBH;
End;

--Procedure thêm mã hoá đơn nhập hàng--
go
create proc themMaHDNH
	@Ngaydat DATE,
    @Trangthai NVARCHAR(50),
    @Thanhtien DECIMAL(18, 2),
	@Soluong INT
As
Begin 
Declare @newMaHDNH varchar(10);
Declare @maxMaHDNH varchar(10);
Declare @soMoi int;
	--Lấy mã hoá đơn nhập hàng lớn nhất hiện tại
Select @maxMaHDNH = MAX(Macalam) from Calam;
	--Nếu chưa có hoá đơn nhập hàng, mã đầu tiên là NH0001
	If @maxMaHDNH is null
		Set @newMaHDNH = 'NH0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@maxMaHDNH, 3, 4) AS INT) + 1;
	Set @newMaHDNH = 'NH' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into HD_Nhaphang(Sohd, Ngaydat, Trangthai, Thanhtien, Soluong)
	Values (@newMaHDNH, @Ngaydat, @Trangthai, @Thanhtien, @Soluong);
	print 'adding successfully: ' + @newMaHDNH;
End;

-- Procedure thêm dữ liệu bảng chấm công
	-- Tự động thêm khoá chính
	-- Tự động thêm Trạng Thái
		-- T – DG: Vô làm trễ, về trễ hoặc đúng giờ 
		-- DG: Vô đúng giờ hoặc sớm, về trễ hoặc đúng giờ
		-- T – S: Đi trễ về sớm
go
create proc themChamCong 
	@ThoigianCN date, 
	@Checkin time, 
	@Checkout time,
	@Macalam varchar(10), 
	@Manhanvien varchar(10)
As
Begin
	Declare @ID varchar(10);
	Declare @ThoigianBD time;
	Declare @ThoigianKT time;
	Declare @Trangthai nvarchar(100);

	--Lấy giờ bắt đầu và kết thúc từ bảng Ca làm
	Select @ThoigianBD = ThoigianBD, @ThoigianKT = ThoigianKT
	From Calam
	Where Macalam = @Macalam;
	-- Kiểm tra nếu không tìm thấy ca làm
	IF @ThoigianBD is null or @ThoigianKT is null
	Begin
		Print 'Không tìm thấy ca làm';
		Return;
	End
	-- Xác định trạng thái dựa trên Checkin và Checkout
	If @Checkin > @ThoigianBD and @Checkout < @ThoigianKT
		Set @Trangthai = N'T - S'; -- Đi trễ, về sớm
	Else if @Checkin > @ThoigianBD and @Checkout >= @ThoigianKT
		Set @Trangthai = N'T - DG' -- Đi trễ, về đúng giờ hoặc về trễ
	Else if @Checkin <= @ThoigianBD and @Checkout >= @ThoigianKT
		Set @Trangthai = N'DG' -- Đi đúng giờ hoặc sớm, về đúng giờ hoặc về trễ
	ELSE
        Set @Trangthai = N'T – DG'; -- Mặc định nếu không rơi vào các trường hợp trên

	-- Tạo ID tự động (CC0001, CC0002,...)
    DECLARE @MaxID INT;
    SELECT @MaxID = MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)) FROM Chamcong;
	-- Nếu bảng rỗng, bắt đầu từ 1
    IF @MaxID IS NULL
        Set @MaxID = 1;
    Else
        Set @MaxID = @MaxID + 1;
	-- Set ID
	 SET @ID = 'CC' + RIGHT('0000' + CAST(@MaxID AS VARCHAR(4)), 4);

	 Declare @Socong float;
	 -- Tính số phút làm việc
	 DECLARE @SoPhut INT;

	 If @Checkout < @ThoigianKT -- Về sớm
		Set @SoPhut = DATEDIFF(Minute, @Checkin, @Checkout);
	Else 
		Set @SoPhut = DATEDIFF(Minute, @Checkin, @ThoigianKT);

	-- Chuyển phút thành số công (giờ)
	Set @Socong = @SoPhut / 60.0;

	-- Thêm dữ liệu vào bảng Chamcong
    INSERT INTO Chamcong (ID, ThoigianCN, Checkin, Checkout, Socong, Trangthai, Macalam, Manhanvien)
    VALUES (@ID, @ThoigianCN, @Checkin, @Checkout, @Socong, @Trangthai, @Macalam, @Manhanvien);
	PRINT 'Đã thêm chấm công với ID: ' + @ID + ' và trạng thái: ' + @Trangthai;
End;