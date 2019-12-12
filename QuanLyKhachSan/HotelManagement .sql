create database HotelManagement
drop database HotelManagement
use HotelManagement
drop database HotelManagement

CREATE TABLE NhanVien
(
MaNV INT IDENTITY(1,1) PRIMARY KEY,
HoNV nvarchar(50)NOT NULL,
TenNV nvarchar(50) NOT NULL,
NgaySinh DATEtime  NOT NULL,
GioiTinh nvarchar(5) NOT NULL,
DiaChi nvarchar (100) NOT NULL,
Luong FLOAT NOT NULL,
ChucVu nvarchar(50) NOT NULL,
)
CREATE TABLE KhachHang
(
MaKH INT IDENTITY(1,1) primary key,
HoKH nvarchar(50) NOT NULL,
TenKH nvarchar(50) not null,
GioiTinh nvarchar(5) NOT NULL,
NgaySinh DATEtime  NOT NULL,
QuocTich nvarchar(50) not null,
SDT nvarchar(15) not null,
MaPhong int not null,
)
CREATE TABLE Phong
(
MaPhong INT IDENTITY(1,1) PRIMARY KEY,
TenPhong nvarchar(100)NOT NULL,
LoaiPhong nvarchar(50) NOT NULL,
GiaPhong float NOT NULL,
TinhTrang nvarchar(50) not null,
MaNV int not null,
MaDV int not null,
)
CREATE TABLE DichVu
(
	MaDV INT IDENTITY(1,1) primary key,
	TenDV nvarchar(50) not null,
	GiaDV float not null,
)
CREATE TABLE HoaDon
(
	MaHD INT IDENTITY(1,1) primary key,
	MaNV int not null,
	SoPhong nvarchar(100) not null,
	NgayDat DATEtime not null,
	NgayTra DATEtime not null,
	GiaHD float not null,
)

CREATE TABLE TaiKhoan
(
	MaTaiKhoan INT IDENTITY(1,1) primary key,
	MaNV int not null,
	TenTaiKhoan nvarchar(50) not null,
	MatKhau nvarchar(50) not null,
	Quyen nvarchar(50) not null,
	TinhTrang nvarchar(50) not null,
)

create table ChiTietHoaDon(
	MaCTHD INT IDENTITY(1,1) PRIMARY KEY,
	MaKH INT not null,
	MaHD INT not null,
	DanhSachDichVu nvarchar(100) not null
)
alter table Phong add constraint Phong_MaNV_FK foreign key(MaNV) references NhanVien(MaNV)
alter table Phong add constraint Phong_MaDV_FK foreign key(MaDV) references DichVu(MaDV)
alter table HoaDon add constraint HoaDon_MaNV_FK foreign key(MaNV) references NhanVien(MaNV)
alter table KhachHang add constraint KhachHang_MaPhong_FK foreign key(MaPhong) references Phong(MaPhong)
alter table TaiKhoan add constraint TaiKhoan_MaNV_FK foreign key(MaNV) references NhanVien(MaNV)
alter table ChiTietHoaDon add constraint ChiTietHoaDon_MaKH_FK foreign key(MaKH) references KhachHang(MaKH)
alter table ChiTietHoaDon add constraint ChiTietHoaDon_MaHD_FK foreign key(MaHD) references HoaDon(MaHD)


----------------------------------------------------------------------------------------------
/*NHANVIEN*/ 
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Không',N'Không','2000/01/01',N'Không',N'Không',0,N'không')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Vuong Ngoc',N'Quyen','1957/10/22',N'Nu',N'450 Trung Vuong,Ha Noi',3000000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Nguyen Thanh',N'Tung','1955/01/09',N'Nam',N'731 Tran Hung Dao,Q1,TPHCM',2500000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Le Thi',N'Nhan','1996/01/18',N'Nu',N'291 Ho Van Hue,QPN,TPHCM',2500000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Dinh Ba',N'Tien','1968/01/09',N'Nam',N'638 Nguyen Van Cu ,Q5,TPHCM',2200000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Bui Thuy',N'Vu','1972/07/19',N'Nam,',N'332 Nguyen Thai Hoc ,Q1,TPHCM',2200000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Nguyen Manh',N'Hung','1973/09/05',N'Nam',N'978 Ba Ria Vung Tau',2000000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Tran Thanh',N'Tam','1975/07/31',N'Nu',N'543 Mai Thi Luu ,Q1,TPHCM',2200000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Tran Hong',N'Van','1976/07/04',N'Nu',N'980 Le Hong Phong ,Q10,TPHCM',1800000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'khong',N'Van','1976/07/04',N'Nu',N'980 Le Hong Phong ,Q10,TPHCM',1800000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Tran d',N'Van','1976/07/04',N'Nu',N'980 Le Hong Phong ,Q10,TPHCM',1800000,N'Nhân Viên')
INSERT INTO NHANVIEN(HONV,TENNV,NgaySinh,GioiTinh,DiaChi,Luong,ChucVu) VALUES (N'Tran w',N'Van','1976/07/04',N'Nu',N'980 Le Hong Phong ,Q10,TPHCM',1800000,N'Nhân Viên')
----------------------------------------------------------------------------------------------
/*KhachHang*/
insert into KhachHang(HoKH,TenKH,GioiTinh, NgaySinh, QuocTich,SDT,MaPhong) values (N'Nguyễn Hùng',N'Vương',N'Nam','1999/05/17',N'Việt Nam','0785715880',4)
insert into KhachHang(HoKH,TenKH,GioiTinh, NgaySinh, QuocTich,SDT,MaPhong) values (N'Huỳnh Nhật',N'Trí',N'Nam','1999/02/03',N'Việt Nam','0123456789',2)
insert into KhachHang(HoKH,TenKH,GioiTinh, NgaySinh, QuocTich,SDT,MaPhong) values (N'Trần Quốc',N'Trung',N'Nam','1999/02/03',N'Việt Nam','0123456789',3)


----------------------------------------------------------------------------------------------
/*Phong*/
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'A1',N'Phòng Đơn',700000,N'Đã Đặt',2,2)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'A2',N'Phòng Đơn',700000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'A3',N'Phòng Đơn',700000,N'Đã đặt',2,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'A4',N'Phòng Đơn',700000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'B1',N'Phòng Đơn Vip',850000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'B2',N'Phòng Đơn Vip',850000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'B3',N'Phòng Đơn Vip',850000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'B4',N'Phòng Đơn Vip',850000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'C1',N'Phòng Đôi',1000000,N'Đã Đặt',3,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'C2',N'Phòng Đôi',1000000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'C3',N'Phòng Đôi',1000000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'C4',N'Phòng Đôi',1000000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'D1',N'Phòng Đôi Vip',1200000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'D2',N'Phòng Đôi Vip',1200000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'D3',N'Phòng Đôi Vip',1200000,N'Trống',1,1)
insert into Phong(TenPhong, LoaiPhong, GiaPhong,TinhTrang, MaNV, MaDV) values (N'D4',N'Phòng Đôi Vip',1200000,N'Trống',1,1)
----------------------------------------------------------------------------------------------
/*DichVu*/
insert into DichVu(TenDV,GiaDV) values (N'Không có dịch vụ',0)
insert into DichVu(TenDV,GiaDV) values (N'Massage',30000)
insert into DichVu(TenDV,GiaDV) values (N'Xông hơi',30000)
insert into DichVu(TenDV,GiaDV) values (N'Giặt ủi',20000)
insert into DichVu(TenDV,GiaDV) values (N'Fitness',100000)

----------------------------------------------------------------------------------------------
/*HoaDon*/
insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) values (3,'3','2019/12/01','2019/12/13',1130000)
insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) values (2,'2','2019/12/02','2019/12/15',1030000)
insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) values (2,'3','2019/12/03','2020/01/01',1150000)
insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) values (3,'2','2019/12/04','2019/12/23',1180000)
insert into HoaDon(MaNV,SoPhong,NgayDat,NgayTra, GiaHD) values (3,'3@5','2019/12/04','2019/12/20',1080000)
----------------------------------------------------------------------------------------------
/*TaiKhoan*/
insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) values (1,N'Admin',N'Admin',N'Quản Trị',N'Hoạt động')
insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) values (2,N'nv1',N'nv1',N'Nhân Viên',N'Hoạt động')
insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) values (10,N'Admin1',N'Admin1',N'Quản Trị',N'Hoạt động')
insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) values (11,N'nv2',N'nv2',N'Nhân Viên',N'Hoạt động')
insert into TaiKhoan(MaNV,TenTaiKhoan,MatKhau,Quyen,TinhTrang) values (12,N'nv3',N'nv3',N'Nhân Viên',N'Hoạt động')


select max(MaHD)
from hoadon

select *
from khachhang


select *
from chitiethoadon

select *
from hoadon
select *
from taikhoan
where TenTaiKhoan = N'Admin' or  Quyen != N'Quản Trị'

select *
from nhanvien

select* from KhachHang where SDT = N'a'

delete HoaDon
drop table HoaDon
delete khachhang where makh > 3
insert into 
KhachHang(HoKH,TenKH,GioiTinh, NgaySinh, QuocTich,SDT,MaPhong) 
values (N'Trần Quốc',N'Trung',N'Nam','1999/02/03',N'Việt Nam',N'0123456789',3)

update Phong set TinhTrang = N'Trống' where Quyen = N'Trống'