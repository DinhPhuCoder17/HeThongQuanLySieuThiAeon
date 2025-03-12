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

CREATE TABLE Khachhang (
    Sodienthoai varchar(15) CONSTRAINT PK_Khachhang PRIMARY KEY,
    Hoten NVARCHAR(100),
    Diachi NVARCHAR(255),
    Diemthuong INT,
    Gioitinh NVARCHAR(10),
	Hang nvarchar(50),
	Xoa int
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