--drop database QuanLySieuThiAEON
go
create database QuanLySieuThiAEON
go
use QuanLySieuThiAEON
go


CREATE TABLE Nhanvien (
    Manhanvien varchar(10) CONSTRAINT PK_Nhanvien PRIMARY KEY,
    Hoten NVARCHAR(100),
    CCCD VARCHAR(20),
    Ngaysinh DATE,
    Gioitinh NVARCHAR(10),
    Diachi NVARCHAR(255),
    Sodienthoai VARCHAR(15),
	Xoa int
);

CREATE TABLE Khachhang (
	Sodienthoai varchar(10) CONSTRAINT PK_KH PRIMARY KEY,
	Hoten nvarchar(255),
	Diachi nvarchar(255),
	Diemthuong int,
	Gioitinh nvarchar(10),
	Hang nvarchar(50),
	Xoa int default 1
)

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
    Tongtien DECIMAL(18, 2),
	Soluong INT,
	Hanthanhtoan date
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
	Soluong int,
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
    Sodienthoai VARCHAR(15),
	Xoa int
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
	THSD varchar(50)
    CONSTRAINT FK_Hanghoa_MaNCC FOREIGN KEY (MaNCC) REFERENCES Nhacungcap(MaNCC),
	Xoa int
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



CREATE TABLE Hoadonbanhang (
    Mahoadon varchar(10) CONSTRAINT PK_Hoadonbanhang PRIMARY KEY,
    Thoigianban DATETIME,
    Manhanvien varchar(10),
    Sodienthoai varchar(15),
    CONSTRAINT FK_Hoadonbanhang_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien),
	Thanhtien float
);


CREATE TABLE HH_HDBH (
    Mahanghoa varchar(10),
    Mahoadon varchar(10),
	Soluong INT,
    CONSTRAINT PK_HH_HDBH PRIMARY KEY (Mahanghoa, Mahoadon),
    CONSTRAINT FK_HH_HDBH_Mahanghoa FOREIGN KEY (Mahanghoa) REFERENCES Hanghoa(Mahanghoa),
    CONSTRAINT FK_HH_HDBH_Mahoadon FOREIGN KEY (Mahoadon) REFERENCES Hoadonbanhang(Mahoadon),
	Tongtien float
);



CREATE TABLE Batbuoc(
	Macalam varchar(10),
	Manhanvien varchar(10),
	CONSTRAINT PK_Batbuoc PRIMARY KEY (Macalam, Manhanvien),
    CONSTRAINT FK_Batbuoc_Macalam FOREIGN KEY (Macalam) REFERENCES Calam(Macalam),
    CONSTRAINT FK_Batbuoc_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien)
);





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
go
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

--Trigger cập nhật hạng khách hàng--
go
Create trigger tg_HDBH
on Hoadonbanhang
for insert
As
Begin
	Declare @TongTienDaMua float, @Sodienthoai varchar(15)
	Set @Sodienthoai = (
		Select Sodienthoai
		From inserted
	)
	Set @TongTienDaMua = (
		Select SUM(Thanhtien)
		From Hoadonbanhang
		Where Sodienthoai = @Sodienthoai
	)

	if (@TongTienDaMua * 1000) < 1000000
	Begin
		Update Khachhang set Hang = N'Thành viên' Where Sodienthoai = @Sodienthoai
	End
	else if	(@TongTienDaMua * 1000) < 3000000
	Begin
		Update Khachhang set Hang = N'Bạc' Where Sodienthoai = @Sodienthoai
	End
	else if	(@TongTienDaMua * 1000) < 7000000
	Begin
		Update Khachhang set Hang = N'Vàng' Where Sodienthoai = @Sodienthoai
	End
	else if	(@TongTienDaMua * 1000) < 10000000
	Begin
		Update Khachhang set Hang = N'Kim Cương' Where Sodienthoai = @Sodienthoai
	End

End

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
		Set @newMaNhanvien = 'NV0001';
	--Tiến hành tạo mã mới
	Else
	Begin
	Set @soMoi = cast(substring(@MaxMaNhanvien, 3, 4) AS INT) + 1;
	Set @newMaNhanvien = 'NV' + right('0000' + cast(@soMoi as varchar(4)), 4)
	End
	--Insert
	Insert into Nhanvien(Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Xoa)
	Values (@newMaNhanvien, @Hoten, @CCCD, @Ngaysinh, @Gioitinh, @Diachi, @Sodienthoai, 1);
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
    @MaNCC varchar(10),
	@THSD varchar(50)
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
INSERT INTO Hanghoa (Mahanghoa, Tenhanghoa, Tiennhap, Tendanhmuc, Tienban, ImageData, Soluong, Uudai, MaNCC, THSD, Xoa)  
Values (@newMaHanghoa, @Tenhanghoa, @Tiennhap, @Tendanhmuc, @Tienban, @ImageData, @Soluong, @Uudai, @MaNCC, @THSD, 1);
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
	Insert into Nhacungcap(MaNCC, TenNCC, Diachi, Masothue, Sodienthoai, Xoa)
	Values (@newMaNCC, @TenNCC, @Diachi, @Masothue, @Sodienthoai, 1);
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
    @Tongtien DECIMAL(18, 2),
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
	Insert into HD_Nhaphang(Sohd, Ngaydat, Trangthai, Tongtien, Soluong, Hanthanhtoan)
	Values (@newMaHDNH, @Ngaydat, @Trangthai, @Tongtien, @Soluong, DATEADD(MONTH, 1, @Ngaydat));
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
	Select @ThoigianBD = CONVERT(TIME, ThoigianBD), @ThoigianKT = CONVERT(TIME, ThoigianKT)
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
	Set @Socong = @SoPhut / 60.0 / 8.0

	-- Thêm dữ liệu vào bảng Chamcong
    INSERT INTO Chamcong (ID, ThoigianCN, Checkin, Checkout, Socong, Trangthai, Macalam, Manhanvien)
    VALUES (@ID, @ThoigianCN, @Checkin, @Checkout, @Socong, @Trangthai, @Macalam, @Manhanvien);
	PRINT 'Đã thêm chấm công với ID: ' + @ID + ' và trạng thái: ' + @Trangthai;
End;


--Trigger Them Khach hang--
go
CREATE PROC ThemKH 
		@Sodienthoai nvarchar(12),
		@Hoten nvarchar(100),
		@Diachi nvarchar(255),
		@Gioitinh nvarchar(100)
As
Begin
	if exists (
		Select *
		From Khachhang
		Where Sodienthoai = @Sodienthoai And Xoa = 0
	)
	Begin
		Update Khachhang set Xoa = 1, Hoten = @Hoten, Diachi = @Diachi, Gioitinh = @Gioitinh, Diemthuong = 0, Hang = 'Thành viên' Where Sodienthoai = @Sodienthoai
	End
	Else
	Begin
		Declare @Diemthuong int, @Hang nvarchar(50)
	Set @Diemthuong = 0
	Set @Diemthuong = (
		Select sum(Thanhtien)
		From Hoadonbanhang
		Where Sodienthoai = @Sodienthoai
	)/1000

	if(@Diemthuong is null)
	Begin
			Set @Diemthuong = 0
	End

	if (@Diemthuong * 1000) < 1000000
	Begin
		Set @Hang = N'Thành viên'
	End
	else if	(@Diemthuong * 1000) < 3000000
	Begin
		Set @Hang = N'Bạc'
	End
	else if	(@Diemthuong * 1000) < 7000000
	Begin
		Set @Hang = N'Vàng'
	End
	else
	Begin
		Set @Hang = N'Kim Cương'
	End

	Insert into Khachhang values(@Sodienthoai, @Hoten, @Diachi, @Diemthuong, @Gioitinh, @Hang, 1)
	End
End

go
INSERT INTO Nhanvien (Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Xoa) 
VALUES 
('NV0001', N'Nguyễn Văn A', '123456789012', '1990-01-01', N'Nam', N'Hà Nội', '0987654321', 1),
('NV0002', N'Trần Thị B', '123456789013', '1992-02-02', N'Nữ', N'Hồ Chí Minh', '0912345678', 1),
('NV0003', N'Lê Văn C', '123456789014', '1995-03-03', N'Nam', N'Đà Nẵng', '0901234567', 1),
('NV0004', N'Phạm Thị D', '123456789015', '1998-04-04', N'Nữ', N'Hải Phòng', '0988123456', 1),
('NV0005', N'Hồ Văn E', '123456789016', '1991-05-05', N'Nam', N'Cần Thơ', '0971234567', 1),
('NV0006', N'Đinh Thị F', '123456789017', '1994-06-06', N'Nữ', N'Bình Dương', '0961234567', 1),
('NV0007', N'Bùi Văn G', '123456789018', '1993-07-07', N'Nam', N'Quảng Ninh', '0951234567', 1),
('NV0008', N'Ngô Thị H', '123456789019', '1996-08-08', N'Nữ', N'Vũng Tàu', '0941234567', 1),
('NV0009', N'Doãn Văn I', '123456789020', '1997-09-09', N'Nam', N'Thái Bình', '0931234567', 1),
('NV0010', N'Vũ Thị K', '123456789021', '1990-10-10', N'Nữ', N'An Giang', '0921234567', 1);

INSERT INTO Khachhang (Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang, Xoa) VALUES
('0987654321', N'Nguyễn Văn An', N'Hà Nội', 100, N'Nam', N'Thành viên', 1),
('0971122334', N'Trần Thị Bình', N'Hồ Chí Minh', 200, N'Nữ', N'Bạc', 1),
('0962233445', N'Lê Minh Quang', N'Đà Nẵng', 150, N'Nam', N'Vàng', 1),
('0953344556', N'Phạm Hoài Nam', N'Cần Thơ', 120, N'Nam', N'Thành viên', 1),
('0944455667', N'Hoàng Thanh Tâm', N'Hải Phòng', 300, N'Nữ', N'Bạch Kim', 1),
('0935566778', N'Vũ Đức Toàn', N'Bình Dương', 250, N'Nam', N'Vàng', 1),
('0926677889', N'Đặng Mỹ Linh', N'Quảng Ninh', 180, N'Nữ', N'Bạc', 1),
('0917788990', N'Ngô Anh Tú', N'Nha Trang', 130, N'Nam', N'Thành viên', 1),
('0908899001', N'Bùi Thị Hoa', N'Huế', 210, N'Nữ', N'Bạch Kim', 1),
('0899900112', N'Lý Quang Minh', N'Tây Ninh', 90, N'Nam', N'Thành viên', 1);

INSERT INTO Nhacungcap (MaNCC, TenNCC, Diachi, Masothue, Sodienthoai, Xoa)
VALUES
    ('NCC0001', N'Công ty TNHH Thực Phẩm An Phát', N'123 Lê Lợi, Hà Nội', '0101234567', '0912345678', 1),
    ('NCC0002', N'Công ty CP Hóa Mỹ Phẩm Việt', N'456 Trần Hưng Đạo, TP.HCM', '0202345678', '0923456789', 1),
    ('NCC0003', N'Công ty TNHH Dịch Vụ Thương Mại Minh Khang', N'789 Lý Thường Kiệt, Đà Nẵng', '0303456789', '0934567890', 1),
    ('NCC0004', N'Công ty CP Sản Xuất Hòa Bình', N'12 Nguyễn Huệ, Huế', '0404567890', '0945678901', 1),
    ('NCC0005', N'Công ty TNHH Nông Sản Việt', N'34 Quang Trung, Hải Phòng', '0505678901', '0956789012', 1),
    ('NCC0006', N'Công ty CP Công Nghệ Thịnh Vượng', N'56 Lê Lai, Cần Thơ', '0606789012', '0967890123', 1),
    ('NCC0007', N'Công ty TNHH Xuất Nhập Khẩu Thành Công', N'78 Hai Bà Trưng, Nha Trang', '0707890123', '0978901234', 1),
    ('NCC0008', N'Công ty CP Thương Mại Đại Phát', N'90 Phạm Ngũ Lão, Quy Nhơn', '0808901234', '0989012345', 1),
    ('NCC0009', N'Công ty TNHH Dược Phẩm An Bình', N'102 Nguyễn Trãi, Vũng Tàu', '0909012345', '0990123456', 1),
    ('NCC0010', N'Công ty CP Phân Phối Nam Việt', N'114 Đống Đa, Bình Định', '1001234567', '0901234567', 1);

INSERT INTO Hanghoa (Mahanghoa, Tenhanghoa, Tiennhap, Tendanhmuc, Tienban, ImageData, Soluong, Uudai, MaNCC, THSD, Xoa)
VALUES
    ('HH0001', N'Gạo ST25', 15000, N'Thực phẩm', 20000, NULL, 500, '5%', 'NCC0001', '2025-12-31', 1),
    ('HH0002', N'Dầu ăn Simply 1L', 45000, N'Thực phẩm', 55000, NULL, 500, '10%', 'NCC0002', '2025-11-30', 1),
    ('HH0003', N'Sữa Vinamilk 180ml', 6500, N'Đồ uống', 9000, NULL, 500, '7%', 'NCC0003', '2025-10-15', 1),
    ('HH0004', N'Mì Hảo Hảo', 3500, N'Thực phẩm', 5000, NULL, 500, '3%', 'NCC0004', '2025-09-25', 1),
    ('HH0005', N'Nước suối La Vie 500ml', 4000, N'Đồ uống', 6000, NULL, 500, '8%', 'NCC0005', '2025-08-20', 1),
    ('HH0006', N'Bánh Chocopie', 75000, N'Bánh kẹo', 95000, NULL, 500, '12%', 'NCC0006', '2025-07-12', 1),
    ('HH0007', N'Bột giặt Omo 4.5kg', 120000, N'Hóa phẩm', 145000, NULL, 500, '15%', 'NCC0007', '2025-06-30', 1),
    ('HH0008', N'Kem đánh răng P/S', 25000, N'Hóa phẩm', 35000, NULL, 500, '10%', 'NCC0008', '2025-05-18', 1),
    ('HH0009', N'Nước mắm Nam Ngư 500ml', 32000, N'Thực phẩm', 45000, NULL, 500, '6%', 'NCC0009', '2025-04-10', 1),
    ('HH0010', N'Khẩu trang y tế 50 cái', 45000, N'Chăm sóc sức khỏe', 60000, NULL, 500, '20%', 'NCC0010', '2025-03-01', 1);


INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0001', '2024-03-01 08:30:00', 'NV0001', '0971122334', 800000)
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0002', '2024-03-02 10:45:00', 'NV0002', '0962233445', 2300000)
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0003', '2024-03-03 14:20:00', 'NV0003', '0962233445', 1700000)


Insert into HH_HDBH values
('HH0001', 'HD0001', 10, 200000)
Insert into HH_HDBH values
('HH0010', 'HD0001', 10, 600000)
Insert into HH_HDBH values
('HH0002', 'HD0002', 20, 1100000)
Insert into HH_HDBH values
('HH0005', 'HD0002', 200,1200000)
Insert into HH_HDBH values
('HH0002', 'HD0003', 20, 1100000)
Insert into HH_HDBH values
('HH0010', 'HD0003', 10,600000)

exec themMacalam N'Ca thường', '2024-03-15 08:30:00', '2024-03-15 15:30:00', 3
exec themMacalam N'Ca thường', '2024-03-15 16:30:00', '2024-03-15 21:30:00', 3
exec themMacalam N'Ca thường', '2025-03-14 16:30:00', '2024-03-15 21:30:00', 3
exec themMacalam N'Ca thường', '2025-03-13 08:30:00', '2025-03-13 15:30:00', 3

Select * From Calam
Insert into Batbuoc values('CL0001', 'NV0001')
Insert into Batbuoc values('CL0001', 'NV0002')
Insert into Batbuoc values('CL0001', 'NV0003')
Insert into Batbuoc values('CL0002', 'NV0004')
Insert into Batbuoc values('CL0002', 'NV0005')
Insert into Batbuoc values('CL0004', 'NV0004')

Insert into Batbuoc values('CL0002', 'NV0006')

EXEC themChamCong '2025-03-15', '08:30:00', '15:30:00', 'CL0001', 'NV0001';
EXEC themChamCong '2025-03-15', '10:30:00', '17:30:00', 'CL0001', 'NV0002';
EXEC themChamCong '2025-03-15', '09:30:00', '15:30:00', 'CL0001', 'NV0003';

Select * From Chamcong


