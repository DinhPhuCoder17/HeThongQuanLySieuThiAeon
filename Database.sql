--drop database QuanLySieuThiAEON
go
create database QuanLySieuThiAEON
go
use QuanLySieuThiAEON
go

--select * from Quanly

CREATE TABLE Nhanvien (
    Manhanvien varchar(10) CONSTRAINT PK_Nhanvien PRIMARY KEY,
    Hoten NVARCHAR(100),
    CCCD VARCHAR(20),
    Ngaysinh DATE,
    Gioitinh NVARCHAR(10),
    Diachi NVARCHAR(255),
    Sodienthoai VARCHAR(15),
	Vaitro nvarchar(100),
	Xoa int
);
--Account
CREATE TABLE Quanly (
    Manhanvien varchar(10) CONSTRAINT PK_Quanly PRIMARY KEY,
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255),
	Role VARCHAR(50),
    CONSTRAINT FK_Quanly_Nhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien)
);
--End Account
CREATE TABLE Khachhang (
	Sodienthoai varchar(10) CONSTRAINT PK_KH PRIMARY KEY,
	Hoten nvarchar(255),
	Diachi nvarchar(255),
	Diemthuong int,
	Gioitinh nvarchar(10),
	Hang nvarchar(50),
	Xoa int default 1
)


CREATE TABLE HD_Nhaphang (
    Sohd varchar(10) CONSTRAINT PK_HD_Nhaphang PRIMARY KEY,
    Ngaydat DATETIME,
    Trangthai NVARCHAR(50),
    Tongtien DECIMAL(18, 2),
	Soluong INT,
	Hanthanhtoan date
);

CREATE TABLE QuanlyKho (
    Manhanvien varchar(10) CONSTRAINT PK_QuanlyKho PRIMARY KEY,
    CONSTRAINT FK_QuanlyKho_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Quanly(Manhanvien),
);
--select * from calam
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
--delete from Chamcong

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
	THSD int
    CONSTRAINT FK_Hanghoa_MaNCC FOREIGN KEY (MaNCC) REFERENCES Nhacungcap(MaNCC),
	Xoa int,
	Barcode VARCHAR(20) not null
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
	Ngaysanxuat DATE,
	Hansudung DATE,
	Thanhtien Decimal(18,2),
	Trangthai nvarchar(100),
    CONSTRAINT PK_HD_HH PRIMARY KEY (Mahanghoa, Sohd),
    CONSTRAINT FK_HD_HH_Mahanghoa FOREIGN KEY (Mahanghoa) REFERENCES Hanghoa(Mahanghoa),
    CONSTRAINT FK_HD_HH_Sohd FOREIGN KEY (Sohd) REFERENCES HD_Nhaphang(Sohd)
);

--delete from Hoadonbanhang
--select * from Hoadonbanhang
CREATE TABLE Hoadonbanhang (
    Mahoadon varchar(10) CONSTRAINT PK_Hoadonbanhang PRIMARY KEY,
    Thoigianban DATETIME,
    Manhanvien varchar(10),
    Sodienthoai varchar(15),
    CONSTRAINT FK_Hoadonbanhang_Manhanvien FOREIGN KEY (Manhanvien) REFERENCES Nhanvien(Manhanvien),
	Thanhtien float
);
-- select * from HH_HDBH where Mahoadon='HD0001'
--delete from HH_HDBH
CREATE TABLE HH_HDBH (
    Mahanghoa varchar(10),
    Mahoadon varchar(10),
	 Tenhanghoa NVARCHAR(255),
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

CREATE TABLE Khieunai (
    Mahanghoa VARCHAR(10),
    Sohd VARCHAR(10),
    Loaikhieunai NVARCHAR(100),
    Lydochitiet NVARCHAR(1000),
	Luongchenhlech int,
	Yeucauxuly NVARCHAR(1000),
    CONSTRAINT PK_Khieunai PRIMARY KEY (Mahanghoa, Sohd),
    CONSTRAINT FK_KN_Mahanghoa_Sohd_HDHH FOREIGN KEY (Mahanghoa, Sohd) 
        REFERENCES HD_HH (Mahanghoa, Sohd)
);


--Trigger--
--Trigger đếm giờ chuyển trạng thái xác nhận--
-- End Trigger đếm giờ chuyển trạng thái xác nhận--

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
    @Sodienthoai VARCHAR(15),
	@Vaitro nvarchar(100)
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
	Values (@newMaNhanvien, @Hoten, @CCCD, @Ngaysinh, @Gioitinh, @Diachi, @Sodienthoai, @Vaitro, 1);
	print 'adding successfully: ' + @newMaNhanvien;
	-- Trả về mã nhân viên vừa thêm
	SELECT @newMaNhanvien;
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
	@THSD int
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
	-- Trả về mã NCC vừa thêm
    SELECT @newMaNCC;
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
	IF EXISTS (
        SELECT 1
        FROM Calam
        WHERE NOT (
            @ThoigianBD >= ThoigianKT OR @ThoigianKT <= ThoigianBD
        )
    )
    BEGIN
        RETURN;
    END;
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
CREATE PROCEDURE themMaHDBH
    @Thoigianban DATETIME,
    @Manhanvien VARCHAR(10),
    @Sodienthoai VARCHAR(15)
AS
BEGIN
    DECLARE @newMaHDBH VARCHAR(10);
    DECLARE @maxMaHDBH VARCHAR(10);
    DECLARE @soMoi INT;
    DECLARE @Thanhtien DECIMAL(18,2);

    -- Lấy mã hóa đơn lớn nhất hiện tại
    SELECT @maxMaHDBH = MAX(Mahoadon) FROM Hoadonbanhang;

    -- Nếu chưa có hóa đơn nào, mã đầu tiên là HD0001
    IF @maxMaHDBH IS NULL
        SET @newMaHDBH = 'HD0001';
    ELSE
    BEGIN
        SET @soMoi = CAST(SUBSTRING(@maxMaHDBH, 3, 4) AS INT) + 1;
        SET @newMaHDBH = 'HD' + RIGHT('0000' + CAST(@soMoi AS VARCHAR(4)), 4);
    END

    -- Chèn dữ liệu vào bảng Hoadonbanhang
    INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien)
    VALUES (@newMaHDBH, @Thoigianban, @Manhanvien, @Sodienthoai, 0);  -- Tạm thời để Thanhtien = 0

    -- Tính tổng tiền của tất cả các dòng có cùng Mahoadon trong HH_HDBH
    SELECT @Thanhtien = SUM(Tongtien) 
    FROM HH_HDBH 
    WHERE Mahoadon = @newMaHDBH;

    -- Cập nhật Thanhtien trong Hoadonbanhang
    UPDATE Hoadonbanhang 
    SET Thanhtien = ISNULL(@Thanhtien, 0)
    WHERE Mahoadon = @newMaHDBH;

    PRINT 'Thêm hóa đơn thành công: ' + @newMaHDBH;
END;

go
--drop procedure themHH_HDBH
--Thêm Hóa Đơn Chi Tiết Hóa Đơn---
go
CREATE PROCEDURE themHH_HDBH
    @Tenhanghoa NVARCHAR(255),
    @Soluong INT
AS
BEGIN
    DECLARE @Mahoadon VARCHAR(10);
    DECLARE @Mahanghoa VARCHAR(10);
    DECLARE @Tienban DECIMAL(18,2);
    DECLARE @Tongtien DECIMAL(18,2);
    DECLARE @SoluongTonKho INT;

    -- Lấy mã hóa đơn mới nhất từ bảng Hoadonbanhang
    SELECT TOP 1 @Mahoadon = Mahoadon 
    FROM Hoadonbanhang 
    ORDER BY Mahoadon DESC;

    -- Kiểm tra nếu không tìm thấy hóa đơn nào
    IF @Mahoadon IS NULL
    BEGIN
        PRINT ' Error: No existing Mahoadon in Hoadonbanhang!';
        RETURN;
    END

    -- Lấy mã hàng hóa, giá bán và số lượng tồn kho từ bảng Hanghoa
    SELECT @Mahanghoa = Mahanghoa, @Tienban = Tienban, @SoluongTonKho = Soluong
    FROM Hanghoa 
    WHERE Tenhanghoa = @Tenhanghoa;

    -- Kiểm tra nếu không tìm thấy mã hàng hóa
    IF @Mahanghoa IS NULL
    BEGIN
        PRINT ' Error: Tenhanghoa does not exist!';
        RETURN;
    END

    -- Kiểm tra nếu số lượng tồn kho không đủ để bán
    IF @SoluongTonKho < @Soluong
    BEGIN
        PRINT ' Error: Not enough stock for ' + @Tenhanghoa + '. Available: ' + CAST(@SoluongTonKho AS NVARCHAR);
        RETURN;
    END

    -- Tính tổng tiền
    SET @Tongtien = @Soluong * @Tienban;

    -- Thêm dữ liệu vào bảng HH_HDBH
    INSERT INTO HH_HDBH (Mahoadon, Mahanghoa, Tenhanghoa, Soluong, Tongtien)
    VALUES (@Mahoadon, @Mahanghoa, @Tenhanghoa, @Soluong, @Tongtien);

    -- Cập nhật tổng tiền trong Hoadonbanhang
    UPDATE Hoadonbanhang 
    SET Thanhtien = (SELECT SUM(Tongtien) FROM HH_HDBH WHERE Mahoadon = @Mahoadon)
    WHERE Mahoadon = @Mahoadon;

    -- Trừ đi số lượng hàng hóa đã bán từ bảng Hanghoa
    UPDATE Hanghoa 
    SET Soluong = Soluong - @Soluong
    WHERE Mahanghoa = @Mahanghoa;

    PRINT ' Added successfully to HH_HDBH and updated Thanhtien in Hoadonbanhang.';
    PRINT ' Stock updated: ' + @Tenhanghoa + ' - Remaining: ' + CAST(@SoluongTonKho - @Soluong AS NVARCHAR);
END;

go
--xóa Hóa đơn
CREATE PROCEDURE sp_XoaHoaDon
    @MaHoaDon VARCHAR(10)
AS
BEGIN
    -- Xóa chi tiết hóa đơn trước
    DELETE FROM HH_HDBH WHERE Mahoadon = @MaHoaDon;
    
    -- Xóa hóa đơn chính
    DELETE FROM Hoadonbanhang WHERE Mahoadon = @MaHoaDon;
    
    PRINT 'Đã xóa hóa đơn thành công!';
END;
--DROP PROCEDURE sp_XoaHoaDon
--Procedure thêm mã hoá đơn nhập hàng--
go
create proc themMaHDNH
    @Tongtien DECIMAL(18, 2),
	@Soluong INT
As
Begin 
Declare @newMaHDNH varchar(10);
Declare @maxMaHDNH varchar(10);
Declare @soMoi int;
	--Lấy mã hoá đơn nhập hàng lớn nhất hiện tại
Select @maxMaHDNH = MAX(Sohd) from HD_Nhaphang
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
	Values (@newMaHDNH, getDate(), N'Chờ Xác Nhận', @Tongtien, @Soluong, DATEADD(MONTH, 1, GETDATE()));
	print 'adding successfully: ' + @newMaHDNH;
End;
go
--DROP PROCEDURE usp_GetWeeklyExpense
CREATE PROCEDURE usp_GetWeeklyExpense
    @Year INT,
    @Month INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Xác định ngày đầu và ngày cuối của tháng
    DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1);
    DECLARE @EndDate DATE = EOMONTH(@StartDate);

    WITH WeeklyData AS (
        SELECT 
            CASE 
                WHEN DAY(Ngaynhap) BETWEEN 1 AND 7 THEN 1
                WHEN DAY(Ngaynhap) BETWEEN 8 AND 14 THEN 2
                WHEN DAY(Ngaynhap) BETWEEN 15 AND 21 THEN 3
                ELSE 4
            END AS WeekNumber,
            SUM(Thanhtien) AS TotalExpense
        FROM HD_HH
        WHERE Ngaynhap BETWEEN @StartDate AND @EndDate
            AND Trangthai = N'Đã Nhập Kho'
        GROUP BY 
            CASE 
                WHEN DAY(Ngaynhap) BETWEEN 1 AND 7 THEN 1
                WHEN DAY(Ngaynhap) BETWEEN 8 AND 14 THEN 2
                WHEN DAY(Ngaynhap) BETWEEN 15 AND 21 THEN 3
                ELSE 4
            END
    ),
    Weeks AS (
        SELECT 1 AS WeekNumber UNION ALL
        SELECT 2 UNION ALL
        SELECT 3 UNION ALL
        SELECT 4
    )
    SELECT 
        w.WeekNumber,
        COALESCE(wd.TotalExpense, 0) AS TotalExpense
    FROM Weeks w
    LEFT JOIN WeeklyData wd ON w.WeekNumber = wd.WeekNumber
    ORDER BY w.WeekNumber;
END;





--EXEC usp_GetWeeklyExpense @Year = 2025, @Month = 4;



go
CREATE PROCEDURE usp_GetWeeklyRevenue
    @Year INT,
    @Month INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH WeeklyData AS (
        SELECT 
            -- Tính số tuần trong tháng
            ((DAY(Thoigianban) - 1) / 7) + 1 AS WeekNumber,
            SUM(Thanhtien) AS TotalRevenue
        FROM Hoadonbanhang
        WHERE YEAR(Thoigianban) = @Year 
              AND MONTH(Thoigianban) = @Month
             
        GROUP BY ((DAY(Thoigianban) - 1) / 7) + 1
    )
    SELECT 
        WeekNumber, 
        COALESCE(TotalRevenue, 0) AS TotalRevenue
    FROM WeeklyData
    ORDER BY WeekNumber;
END;
GO
CREATE PROCEDURE usp_GetMonthlyBestSeller
    @Year INT,
    @Month INT
AS
BEGIN
    SELECT TOP 5 
        hh.Tenhanghoa, 
        SUM(hh_hdbh.Soluong) AS TongSoluong, 
        SUM(hh_hdbh.Tongtien) AS TongTien
    FROM HH_HDBH hh_hdbh
    JOIN Hanghoa hh ON hh_hdbh.Mahanghoa = hh.Mahanghoa
    JOIN Hoadonbanhang hdb ON hh_hdbh.Mahoadon = hdb.Mahoadon
    WHERE YEAR(hdb.Thoigianban) = @Year 
    AND MONTH(hdb.Thoigianban) = @Month
    GROUP BY hh.Tenhanghoa
    ORDER BY TongSoluong DESC;
END;

go

go
CREATE PROCEDURE usp_GetMonthlyWorstSeller
    @Year INT,
    @Month INT
AS
BEGIN
    SELECT TOP 3 
        hh.Tenhanghoa, 
        SUM(hh_hdbh.Soluong) AS TongSoluong, 
        SUM(hh_hdbh.Tongtien) AS TongTien
    FROM HH_HDBH hh_hdbh
    JOIN Hanghoa hh ON hh_hdbh.Mahanghoa = hh.Mahanghoa
    JOIN Hoadonbanhang hdb ON hh_hdbh.Mahoadon = hdb.Mahoadon
    WHERE YEAR(hdb.Thoigianban) = @Year 
    AND MONTH(hdb.Thoigianban) = @Month
    GROUP BY hh.Tenhanghoa
    ORDER BY TongSoluong ASC;
END;
go 
-- Procedure thêm dữ liệu bảng chấm công
	-- Tự động thêm khoá chính
	-- Tự động thêm Trạng Thái
		-- T – DG: Vô làm trễ, về trễ hoặc đúng giờ 
		-- DG: Vô đúng giờ hoặc sớm, về trễ hoặc đúng giờ
		-- T – S: Đi trễ về sớm
go
-- DROP PROCEDURE themChamCong;
CREATE PROCEDURE themChamCong 
    @ThoigianCN DATE, 
    @Checkin TIME, 
    @Checkout TIME,
    @Macalam VARCHAR(10), 
    @Manhanvien VARCHAR(10)
AS
BEGIN
    DECLARE @ID VARCHAR(10);
    DECLARE @ThoigianBD TIME;
    DECLARE @ThoigianKT TIME;
    DECLARE @Trangthai NVARCHAR(100);

    -- Lấy giờ bắt đầu và kết thúc từ bảng Ca làm
    SELECT @ThoigianBD = CONVERT(TIME, ThoigianBD), @ThoigianKT = CONVERT(TIME, ThoigianKT)
    FROM Calam
    WHERE Macalam = @Macalam;

    -- Kiểm tra nếu không tìm thấy ca làm
    IF @ThoigianBD IS NULL OR @ThoigianKT IS NULL
    BEGIN
        PRINT 'Không tìm thấy ca làm';
        RETURN;
    END;

    -- Xác định trạng thái dựa trên Checkin và Checkout
    IF @Checkin > @ThoigianBD AND @Checkout < @ThoigianKT
        SET @Trangthai = N'T - S'; -- Đi trễ, về sớm
    ELSE IF @Checkin > @ThoigianBD AND @Checkout >= @ThoigianKT
        SET @Trangthai = N'T - DG'; -- Đi trễ, về đúng giờ hoặc về trễ
    ELSE IF @Checkin <= @ThoigianBD AND @Checkout >= @ThoigianKT
        SET @Trangthai = N'DG'; -- Đi đúng giờ hoặc sớm, về đúng giờ hoặc về trễ
	 ELSE IF @Checkin <= @ThoigianBD AND @Checkout < @ThoigianKT
        SET @Trangthai = N'DG - S'; -- Đi đúng giờ hoặc sớm, về đúng giờ hoặc về trễ
    ELSE
        SET @Trangthai = N'DG'; -- Mặc định nếu không rơi vào các trường hợp trên

    -- Tạo ID tự động (CC0001, CC0002,...)
    DECLARE @MaxID INT;
    SELECT @MaxID = MAX(CAST(SUBSTRING(ID, 3, 4) AS INT)) FROM Chamcong;
    
    -- Nếu bảng rỗng, bắt đầu từ 1
    IF @MaxID IS NULL
        SET @MaxID = 1;
    ELSE
        SET @MaxID = @MaxID + 1;

    -- Set ID
    SET @ID = 'CC' + RIGHT('0000' + CAST(@MaxID AS VARCHAR(4)), 4);

    DECLARE @Socong FLOAT;
    DECLARE @SoGiay INT;

    -- Kiểm tra thời gian về sớm hoặc đúng giờ
    IF @Checkout <= @ThoigianKT -- Về sớm
       
        SET @SoGiay = DATEDIFF(SECOND, @Checkin, @ThoigianKT);

    -- Chuyển giây thành số công (giờ), với giả sử một ngày làm việc là 8 giờ (28800 giây)
    SET @Socong = @SoGiay / 28800.0; -- 28800 giây = 8 giờ

    -- Kiểm tra nếu Checkin muộn hơn một giây so với ThoigianBD (Tính trễ)
    IF @Checkin > @ThoigianBD
    BEGIN
        -- Tính số công theo giây, nếu đi trễ thì giảm công
        SET @Socong = @Socong - ((DATEDIFF(SECOND, @ThoigianBD, @Checkin)) / 28800.0);
    END;

    -- Thêm dữ liệu vào bảng Chamcong
    INSERT INTO Chamcong (ID, ThoigianCN, Checkin, Checkout, Socong, Trangthai, Macalam, Manhanvien)
    VALUES (@ID, @ThoigianCN, @Checkin, @Checkout, @Socong, @Trangthai, @Macalam, @Manhanvien);

    PRINT 'Đã thêm chấm công với ID: ' + @ID + ' và trạng thái: ' + @Trangthai;
END;



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
--Thêm vào chi tiết HDNH--
Create proc themHD_HH
	@Mahanghoa varchar(10),
	@Sohd varchar(10),
	@Soluongdat int
As
Begin
	Declare @Thanhtien Decimal(18,2)
	Select @Thanhtien = @Soluongdat * Tiennhap
	From Hanghoa

	Insert into HD_HH values (@Mahanghoa, @Sohd, null, @Soluongdat, 0, null, null, @Thanhtien, N'Chưa Nhập Kho')
End
--Thêm vào chi tiết HDNH--

go
--Procedure thêm Khiếu Nại--
Create proc themKhieuNai
	@Mahanghoa varchar(10),
	@Sohd varchar(10),
	@Loaikhieunai nvarchar(100),
	@Lydochitiet nvarchar(1000),
	@Luongchenhlech int,
	@Yeucauxuly nvarchar(1000)
As
Begin
	if exists(
		Select 1
		From Khieunai
		Where Mahanghoa = @Mahanghoa and @Sohd = Sohd
	)
	Begin
		Update Khieunai set Loaikhieunai = @Loaikhieunai, Lydochitiet = @Lydochitiet, Luongchenhlech = @Luongchenhlech , Yeucauxuly = @Yeucauxuly
		Where Mahanghoa = @Mahanghoa and Sohd = @Sohd
	End
	Else
	Begin
		Insert into Khieunai values(@Mahanghoa, @Sohd, @Loaikhieunai, @Lydochitiet, @Luongchenhlech, @Yeucauxuly)
	End
End

go
INSERT INTO Nhanvien (Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Vaitro, Xoa) 
VALUES 
('NV0001', N'Nguyễn Văn A', '123456789012', '1990-01-01', N'Nam', N'Hà Nội', '0987654321', N'Lao công', 1),
('NV0002', N'Trần Thị B', '123456789013', '1992-02-02', N'Nữ', N'Hồ Chí Minh', '0912345678', N'Bảo vệ', 1),
('NV0003', N'Lê Văn C', '123456789014', '1995-03-03', N'Nam', N'Đà Nẵng', '0901234567', N'Bảo vệ',  1),
('NV0004', N'Phạm Thị D', '123456789015', '1998-04-04', N'Nữ', N'Hải Phòng', '0988123456', N'Nhân viên tài chính', 1),
('NV0005', N'Hồ Văn E', '123456789016', '1991-05-05', N'Nam', N'Cần Thơ', '0971234567', N'Nhân viên kho', 1),
('NV0006', N'Đinh Thị F', '123456789017', '1994-06-06', N'Nữ', N'Bình Dương', '0961234567', N'Thu ngân', 1),
('NV0007', N'Bùi Văn G', '123456789018', '1993-07-07', N'Nam', N'Quảng Ninh', '0951234567', N'PG', 1),
('NV0008', N'Ngô Thị H', '123456789019', '1996-08-08', N'Nữ', N'Vũng Tàu', '0941234567', N'Kiểm soát chất lượng', 1),
('NV0009', N'Doãn Văn I', '123456789020', '1997-09-09', N'Nam', N'Thái Bình', '0931234567', N'Kế toán', 1),
('NV0010', N'Vũ Thị K', '123456789021', '1990-10-10', N'Nữ', N'An Giang', '0921234567', N'Nhân viên bán hàng', 1);

Insert into Quanly values
('NV0001', '1', '$2a$11$z9zAD5bZeEbk81MyEfUwQuRITnMDNuctPaACjDsbdWqf/rRzIZ1fy', 'Admin'),
('NV0002', '2', '$2a$11$z9zAD5bZeEbk81MyEfUwQuRITnMDNuctPaACjDsbdWqf/rRzIZ1fy', 'Kho'),
('NV0003', '3', '$2a$11$z9zAD5bZeEbk81MyEfUwQuRITnMDNuctPaACjDsbdWqf/rRzIZ1fy', 'TCNS')
select * from Quanly
--UPDATE Quanly SET Password = '$2a$11$z9zAD5bZeEbk81MyEfUwQuRITnMDNuctPaACjDsbdWqf/rRzIZ1fy' WHERE Username = '1';

INSERT INTO Khachhang (Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang, Xoa) VALUES
('0987654321', N'Nguyễn Văn An', N'Hà Nội', 100, N'Nam', N'Thành viên', 1),
('0971122334', N'Trần Thị Bình', N'Hồ Chí Minh', 200, N'Nữ', N'Bạc', 1),
('0962233445', N'Lê Minh Quang', N'Đà Nẵng', 150, N'Nam', N'Vàng', 1),
('0953344556', N'Phạm Hoài Nam', N'Cần Thơ', 120, N'Nam', N'Thành viên', 1),
('0944455667', N'Hoàng Thanh Tâm', N'Hải Phòng', 300, N'Nữ', N'Bạch Kim', 1),
('0935566778', N'Vũ Đức Toàn', N'Bình Dương', 250, N'Nam', N'Vàng', 1),
('0926677889', N'Đặng Mỹ Linh', N'Quảng Ninh', 180, N'Nữ', N'Bạc', 1),
('0917788990', N'Ngô Anh Tú', N'Nha Trang', 130, N'Nam', N'Thành viên', 1),
('0899900112', N'Lý Quang Minh', N'Tây Ninh', 90, N'Nam', N'Thành viên', 1);

INSERT INTO Nhacungcap (MaNCC, TenNCC, Diachi, Masothue, Sodienthoai, Xoa)
VALUES
    ('NC0001', N'Công ty TNHH Thực Phẩm An Phát', N'123 Lê Lợi, Hà Nội', '0101234567', '0912345678', 1),
    ('NC0002', N'Công ty CP Hóa Mỹ Phẩm Việt', N'456 Trần Hưng Đạo, TP.HCM', '0202345678', '0923456789', 1),
    ('NC0003', N'Công ty TNHH Dịch Vụ Thương Mại Minh Khang', N'789 Lý Thường Kiệt, Đà Nẵng', '0303456789', '0934567890', 1),
    ('NC0004', N'Công ty CP Sản Xuất Hòa Bình', N'12 Nguyễn Huệ, Huế', '0404567890', '0945678901', 1),
    ('NC0005', N'Công ty TNHH Nông Sản Việt', N'34 Quang Trung, Hải Phòng', '0505678901', '0956789012', 1),
    ('NC0006', N'Công ty CP Công Nghệ Thịnh Vượng', N'56 Lê Lai, Cần Thơ', '0606789012', '0967890123', 1),
    ('NC0007', N'Công ty TNHH Xuất Nhập Khẩu Thành Công', N'78 Hai Bà Trưng, Nha Trang', '0707890123', '0978901234', 1),
    ('NC0008', N'Công ty CP Thương Mại Đại Phát', N'90 Phạm Ngũ Lão, Quy Nhơn', '0808901234', '0989012345', 1),
    ('NC0009', N'Công ty TNHH Dược Phẩm An Bình', N'102 Nguyễn Trãi, Vũng Tàu', '0909012345', '0990123456', 1),
    ('NC0010', N'Công ty CP Phân Phối Nam Việt', N'114 Đống Đa, Bình Định', '1001234567', '0901234567', 1);

INSERT INTO Hanghoa (Mahanghoa, Tenhanghoa, Tiennhap, Tendanhmuc, Tienban, ImageData, Soluong, Uudai, MaNCC, THSD, Xoa, Barcode) 
VALUES
    ('HH0001', N'Gạo ST25', 15000, N'Thực phẩm', 20000, NULL, 500, '5%', 'NC0001', 30, 1, '1000000000001'),
    ('HH0002', N'Dầu ăn Simply 1L', 45000, N'Thực phẩm', 55000, NULL, 500, '10%', 'NC0002', 120, 1, '1000000000002'),
    ('HH0003', N'Sữa Vinamilk 180ml', 6500, N'Đồ uống', 9000, NULL, 500, '7%', 'NC0003', 90, 1, '1000000000003'),
    ('HH0004', N'Mì Hảo Hảo', 3500, N'Thực phẩm', 5000, NULL, 500, '3%', 'NC0004', 180, 1, '1000000000004'),
    ('HH0005', N'Nước suối La Vie 500ml', 4000, N'Đồ uống', 6000, NULL, 500, '8%', 'NC0005', 90, 1, '1000000000005'),
    ('HH0006', N'Bánh Chocopie', 75000, N'Bánh kẹo', 95000, NULL, 500, '12%', 'NC0006', 30, 1, '1000000000006'),
    ('HH0007', N'Bột giặt Omo 4.5kg', 120000, N'Hóa phẩm', 145000, NULL, 500, '15%', 'NC0007', 120, 1, '1000000000007'),
    ('HH0008', N'Kem đánh răng P/S', 25000, N'Hóa phẩm', 35000, NULL, 500, '10%', 'NC0008', 480, 1, '1000000000008'),
    ('HH0009', N'Nước mắm Nam Ngư 500ml', 32000, N'Thực phẩm', 45000, NULL, 500, '6%', 'NC0009', 90, 1, '1000000000009'),
    ('HH0010', N'Khẩu trang y tế 50 cái', 45000, N'Chăm sóc sức khỏe', 60000, NULL, 500, '20%', 'NC0010', 270, 1, '1000000000010');
	

	
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0001', '2024-03-01 08:30:00', 'NV0001', '0971122334', 800000)
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0002', '2024-03-02 10:45:00', 'NV0002', '0962233445', 2300000)
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0003', '2024-03-03 14:20:00', 'NV0003', '0962233445', 1700000)
INSERT INTO Hoadonbanhang (Mahoadon, Thoigianban, Manhanvien, Sodienthoai, Thanhtien) VALUES
('HD0004', '2024-03-04 14:20:00', 'NV0003', '0962233445', 100000)

Insert into HH_HDBH values
('HH0001', 'HD0001',N'Gạo ST25',10,200000)

exec themMacalam N'Ca thường', '2024-03-15 08:30:00', '2024-03-15 15:30:00', 3
exec themMacalam N'Ca thường', '2024-03-15 16:30:00', '2024-03-15 21:30:00', 3
exec themMacalam N'Ca thường', '2025-03-14 16:30:00', '2024-03-15 21:30:00', 3
exec themMacalam N'Ca thường', '2025-03-13 08:30:00', '2025-03-13 15:30:00', 3


Insert into Batbuoc values('CL0001', 'NV0001')
Insert into Batbuoc values('CL0001', 'NV0002')
Insert into Batbuoc values('CL0001', 'NV0003')
Insert into Batbuoc values('CL0002', 'NV0004')
Insert into Batbuoc values('CL0002', 'NV0005')
Insert into Batbuoc values('CL0004', 'NV0004')



EXEC themChamCong '2025-03-15', '13:45:00', '15:30:00', 'CL0001', 'NV0003';
EXEC themChamCong '2025-03-15', '10:30:00', '17:30:00', 'CL0001', 'NV0002';
EXEC themChamCong '2025-03-15', '09:30:00', '15:30:00', 'CL0001', 'NV0003';

go
--Select * From HD_Nhaphang
--Select * From HD_HH
--Delete from HD_Nhaphang
--delete from HD_HH
exec themMaHDNH 10000, 10
exec themHD_HH 'HH0002', 'NH0001', 100
go 
INSERT INTO HD_HH (Mahanghoa, Sohd, Ngaynhap, Soluongdat, Soluongnhan, Ngaysanxuat, Hansudung, Thanhtien, Trangthai)
VALUES
('HH0001', 'HD0001', '2025-01-05', 10, 10, '2024-01-05', '2027-01-05', 500000, N'Đã Nhập Kho'),
('HH0002', 'HD0002', '2025-01-12', 8, 8, '2024-01-12', '2027-01-12', 450000, N'Đã Nhập Kho'),
('HH0003', 'HD0003', '2025-01-18', 15, 15, '2024-01-18', '2027-01-18', 700000, N'Đã Nhập Kho'),
('HH0004', 'HD0004', '2025-01-25', 5, 5, '2024-01-25', '2027-01-25', 300000, N'Đã Nhập Kho'),
('HH0005', 'HD0005', '2025-02-03', 20, 20, '2024-02-03', '2027-02-03', 800000, N'Đã Nhập Kho'),
('HH0006', 'HD0006', '2025-02-10', 12, 12, '2024-02-10', '2027-02-10', 600000, N'Đã Nhập Kho'),
('HH0007', 'HD0007', '2025-02-15', 7, 7, '2024-02-15', '2027-02-15', 350000, N'Đã Nhập Kho'),
('HH0008', 'HD0008', '2025-02-22', 18, 18, '2024-02-22', '2027-02-22', 900000, N'Đã Nhập Kho'),
('HH0009', 'HD0009', '2025-02-28', 13, 13, '2024-02-28', '2027-02-28', 650000, N'Đã Nhập Kho'),
('HH0010', 'HD0010', '2025-03-02', 14, 14, '2024-03-02', '2027-03-02', 720000, N'Đã Nhập Kho'),
('HH0010', 'HD0011', '2025-03-09', 11, 11, '2024-03-09', '2027-03-09', 530000, N'Đã Nhập Kho'),
('HH0010', 'HD0012', '2025-03-16', 9, 9, '2024-03-16', '2027-03-16', 460000, N'Đã Nhập Kho'),
('HH0010', 'HD0013', '2025-03-21', 22, 22, '2024-03-21', '2027-03-21', 880000, N'Đã Nhập Kho'),
('HH0010', 'HD0014', '2025-03-25', 10, 10, '2024-03-25', '2027-03-25', 590000, N'Đã Nhập Kho'),
('HH0010', 'HD0015', '2025-01-08', 6, 6, '2024-01-08', '2027-01-08', 400000, N'Đã Nhập Kho'),
('HH0010', 'HD0016', '2025-01-14', 16, 16, '2024-01-14', '2027-01-14', 670000, N'Đã Nhập Kho'),
('HH0010', 'HD0017', '2025-01-20', 18, 18, '2024-01-20', '2027-01-20', 780000, N'Đã Nhập Kho'),
('HH0010', 'HD0018', '2025-02-05', 25, 25, '2024-02-05', '2027-02-05', 910000, N'Đã Nhập Kho'),
('HH0010', 'HD0019', '2025-02-18', 13, 13, '2024-02-18', '2027-02-18', 620000, N'Đã Nhập Kho'),
('HH0010', 'HD0020', '2025-03-28', 15, 15, '2024-03-28', '2027-03-28', 740000, N'Đã Nhập Kho');

INSERT INTO HD_HH (Mahanghoa, Sohd, Ngaynhap, Soluongdat, Soluongnhan, Ngaysanxuat, Hansudung, Thanhtien, Trangthai)
VALUES
('HH0010', 'HD0021', '2025-01-05', 9, 9, '2024-01-05', '2027-01-05', 480000, N'Đã Nhập Kho'),
('HH0009', 'HD0022', '2025-01-12', 7, 7, '2024-01-12', '2027-01-12', 420000, N'Đã Nhập Kho'),
('HH0008', 'HD0023', '2025-01-18', 14, 14, '2024-01-18', '2027-01-18', 680000, N'Đã Nhập Kho'),
('HH0007', 'HD0024', '2025-01-25', 6, 6, '2024-01-25', '2027-01-25', 320000, N'Đã Nhập Kho'),
('HH0006', 'HD0025', '2025-02-03', 19, 19, '2024-02-03', '2027-02-03', 790000, N'Đã Nhập Kho'),
('HH0005', 'HD0026', '2025-02-10', 11, 11, '2024-02-10', '2027-02-10', 580000, N'Đã Nhập Kho'),
('HH0004', 'HD0027', '2025-02-15', 8, 8, '2024-02-15', '2027-02-15', 370000, N'Đã Nhập Kho'),
('HH0003', 'HD0028', '2025-02-22', 17, 17, '2024-02-22', '2027-02-22', 890000, N'Đã Nhập Kho'),
('HH0004', 'HD0029', '2025-02-28', 12, 12, '2024-02-28', '2027-02-28', 640000, N'Đã Nhập Kho'),
('HH0006', 'HD0030', '2025-03-02', 13, 13, '2024-03-02', '2027-03-02', 710000, N'Đã Nhập Kho'),
('HH0010', 'HD0031', '2025-03-09', 10, 10, '2024-03-09', '2027-03-09', 520000, N'Đã Nhập Kho'),
('HH0003', 'HD0032', '2025-03-16', 8, 8, '2024-03-16', '2027-03-16', 450000, N'Đã Nhập Kho'),
('HH0003', 'HD0033', '2025-03-21', 21, 21, '2024-03-21', '2027-03-21', 860000, N'Đã Nhập Kho'),
('HH0004', 'HD0034', '2025-03-25', 9, 9, '2024-03-25', '2027-03-25', 570000, N'Đã Nhập Kho'),
('HH0005', 'HD0035', '2025-01-08', 5, 5, '2024-01-08', '2027-01-08', 390000, N'Đã Nhập Kho'),
('HH0006', 'HD0036', '2025-01-14', 15, 15, '2024-01-14', '2027-01-14', 660000, N'Đã Nhập Kho'),
('HH0007', 'HD0037', '2025-01-20', 17, 17, '2024-01-20', '2027-01-20', 770000, N'Đã Nhập Kho'),
('HH0008', 'HD0038', '2025-02-05', 24, 24, '2024-02-05', '2027-02-05', 900000, N'Đã Nhập Kho'),
('HH0009', 'HD0039', '2025-02-18', 12, 12, '2024-02-18', '2027-02-18', 610000, N'Đã Nhập Kho'),
('HH0010', 'HD0040', '2025-03-28', 14, 14, '2024-03-28', '2027-03-28', 730000, N'Đã Nhập Kho');




-- Tiếp tục với 90 dòng còn lại...



INSERT INTO HD_Nhaphang (Sohd, Ngaydat, Trangthai, Tongtien, Soluong, Hanthanhtoan )
VALUES
    ('HD0001', '2025-01-05', N'Đã Xử Lý', 500000, 10, '2025-02-05'),
    ('HD0002', '2025-01-12', N'Đã Xử Lý', 450000, 8, '2025-02-12'),
    ('HD0003', '2025-01-18', N'Đã Xử Lý', 700000, 15, '2025-02-18'),
    ('HD0004', '2025-01-25', N'Đã Xử Lý', 300000, 5, '2025-02-25'),
    ('HD0005', '2025-02-03', N'Đã Xử Lý', 800000, 20, '2025-03-03'),
    ('HD0006', '2025-02-10', N'Đã Xử Lý', 600000, 12, '2025-03-10'),
    ('HD0007', '2025-02-15', N'Đã Xử Lý', 350000, 7, '2025-03-15'),
    ('HD0008', '2025-02-22', N'Đã Xử Lý', 900000, 18, '2025-03-22'),
    ('HD0009', '2025-02-28', N'Đã Xử Lý', 650000, 13, '2025-03-28'),
    ('HD0010', '2025-03-02', N'Đã Xử Lý', 720000, 14, '2025-04-02'),
    ('HD0011', '2025-03-09', N'Đã Xử Lý', 530000, 11, '2025-04-09'),
    ('HD0012', '2025-03-16', N'Đã Xử Lý', 460000, 9, '2025-04-16'),
    ('HD0013', '2025-03-21', N'Đã Xử Lý', 880000, 22, '2025-04-21'),
    ('HD0014', '2025-03-25', N'Đã Xử Lý', 590000, 10, '2025-04-25'),
    ('HD0015', '2025-01-08', N'Đã Xử Lý', 400000, 6, '2025-02-08'),
    ('HD0016', '2025-01-14', N'Đã Xử Lý', 670000, 16, '2025-02-14'),
    ('HD0017', '2025-01-20', N'Đã Xử Lý', 780000, 18, '2025-02-20'),
    ('HD0018', '2025-02-05', N'Đã Xử Lý', 910000, 25, '2025-03-05'),
    ('HD0019', '2025-02-18', N'Đã Xử Lý', 620000, 13, '2025-03-18'),
    ('HD0020', '2025-03-28', N'Đã Xử Lý', 740000, 15, '2025-04-28');
--delete from HD_Nhaphang
--delete from HD_HH
	INSERT INTO HD_Nhaphang (Sohd, Ngaydat, Trangthai, Tongtien, Soluong, Hanthanhtoan )
VALUES
    ('HD0021', '2025-01-05', N'Đã Xử Lý', 510000, 9, '2025-02-05'),
    ('HD0022', '2025-01-12', N'Đã Xử Lý', 460000, 7, '2025-02-12'),
    ('HD0023', '2025-01-18', N'Đã Xử Lý', 710000, 14, '2025-02-18'),
    ('HD0024', '2025-01-25', N'Đã Xử Lý', 310000, 6, '2025-02-25'),
    ('HD0025', '2025-02-03', N'Đã Xử Lý', 810000, 19, '2025-03-03'),
    ('HD0026', '2025-02-10', N'Đã Xử Lý', 610000, 11, '2025-03-10'),
    ('HD0027', '2025-02-15', N'Đã Xử Lý', 360000, 8, '2025-03-15'),
    ('HD0028', '2025-02-22', N'Đã Xử Lý', 910000, 17, '2025-03-22'),
    ('HD0029', '2025-02-28', N'Đã Xử Lý', 660000, 12, '2025-03-28'),
    ('HD0030', '2025-03-02', N'Đã Xử Lý', 730000, 13, '2025-04-02'),
    ('HD0031', '2025-03-09', N'Đã Xử Lý', 540000, 10, '2025-04-09'),
    ('HD0032', '2025-03-16', N'Đã Xử Lý', 470000, 8, '2025-04-16'),
    ('HD0033', '2025-03-21', N'Đã Xử Lý', 890000, 21, '2025-04-21'),
    ('HD0034', '2025-03-25', N'Đã Xử Lý', 600000, 9, '2025-04-25'),
    ('HD0035', '2025-01-08', N'Đã Xử Lý', 410000, 5, '2025-02-08'),
    ('HD0036', '2025-01-14', N'Đã Xử Lý', 680000, 15, '2025-02-14'),
    ('HD0037', '2025-01-20', N'Đã Xử Lý', 790000, 17, '2025-02-20'),
    ('HD0038', '2025-02-05', N'Đã Xử Lý', 920000, 24, '2025-03-05'),
    ('HD0039', '2025-02-18', N'Đã Xử Lý', 630000, 12, '2025-03-18'),
    ('HD0040', '2025-03-28', N'Đã Xử Lý', 750000, 14, '2025-04-28');

--comment--
Select * From hanghoa

Select bb.Manhanvien, Hoten, Vaitro, ThoigianBD, ThoigianKT  
From Calam cl left join Batbuoc bb 
	on cl.Macalam = bb.Macalam 
	inner join Nhanvien nv on nv.Manhanvien = bb.Manhanvien 
Where ThoigianBD > '2025/3/30' and ThoigianKT < '2025/4/7'
Group by bb.Manhanvien, Hoten, Vaitro, ThoigianBD, ThoigianKT  

exec themMaHDNH 10000000, 10