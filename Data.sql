CREATE DATABASE QUANLYQUANCAFE
GO

USE QUANLYQUANCAFE
GO

-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo

CREATE TABLE TableFood
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	Status BIT NOT NULL DEFAULT 0
	-- Trống: 0 || Có người: 1
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Staff',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type BIT NOT NULL DEFAULT 0
	-- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	IdCategory INT NOT NULL,
	Price FLOAT NOT NULL DEFAULT 0

		FOREIGN KEY (IdCategory) REFERENCES dbo.FoodCategory(Id)
)
GO

CREATE TABLE Bill
(
	Id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	IdTable INT NOT NULL,
	Status BIT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán

		FOREIGN KEY (IdTable) REFERENCES dbo.TableFood(Id)
)
GO

CREATE TABLE BillInfo
(
	Id INT IDENTITY PRIMARY KEY,
	IdBill INT NOT NULL,
	IdFood INT NOT NULL,
	Count INT NOT NULL DEFAULT 0

		FOREIGN KEY (IdBill) REFERENCES dbo.Bill(Id),
	FOREIGN KEY (IdFood) REFERENCES dbo.Food(Id)
)
GO

-- INSERT VALUES

INSERT INTO dbo.Account
	(
	UserName,
	DisplayName,
	PassWord,
	Type
	)
VALUES
	(
		N'K9', -- UserName - nvarchar(100)
		N'RongK9', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		1  -- Type - bit
	)
INSERT INTO dbo.Account
	(
	UserName,
	DisplayName,
	PassWord,
	Type
	)
VALUES
	(
		N'staff', -- UserName - nvarchar(100)
		N'staff', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		0  -- Type - bit
	)
GO

SELECT *
FROM dbo.Account 
GO

CREATE PROC USP_GetAccountByUserName
	@userName nvarchar(100)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE UserName = @userName;
END
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'K9'
--nvarchar(100)
GO

SELECT *
FROM dbo.Account
WHERE UserName = N'K9' AND PassWord = N'1'
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE @userName=UserName and @passWord=PassWord
END
GO

USP_Login @userName='staff', @passWord='1'
GO

DECLARE @i INT = 11
WHILE @i <= 20
BEGIN
	INSERT dbo.TableFood (Name) VALUES (N'Bàn ' + CAST (@i AS nvarchar(100)))
	SET @i = @i+1
END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO

UPDATE dbo.TableFood SET Status = 1 WHERE Id=9
GO

--Thêm category
INSERT	dbo.FoodCategory(Name)
VALUES (N'Hải sản') -- Name nvarchar(100)
INSERT  dbo.FoodCategory(Name)
VALUES (N'Nông sản') -- Name nvarchar(100))
INSERT dbo.FoodCategory(Name)
VALUES (N'Sản sản') -- Name nvarchar(100))
INSERT dbo.FoodCategory(Name)
VALUES (N'Lâm sản') -- Name nvarchar(100))
INSERT dbo.FoodCategory(Name)
VALUES (N'Nước') -- Name nvarchar(100))
GO
-- Thêm Food
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Mực một nắng nước sa tế', -- name - nvarchar(100)
          1, -- idCategory - int
          120000)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Nghêu hấp xả', 1, 50000)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Dú dê nướng sữa', 2, 60000)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Heo rừng nướng muối ớt', 3, 75000)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Cơm chiên mushi', 4, 999999)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'7Up', 5, 15000)
INSERT dbo.Food
        ( Name, IdCategory, Price )
VALUES  ( N'Cafe', 5, 12000)
GO
--Thêm Bill
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          IdTable ,
          Status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          4, -- idTable - int
          0  -- status - bit
        )
INSERT	dbo.Bill
        ( DateCheckIn ,
          DateCheckOut ,
          IdTable ,
          Status
        )
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          5 , -- idTable - int
          1  -- status - bit
        )
GO
--Thêm BillInfo
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 1, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 1, -- idBill - int
          3, -- idFood - int
          4  -- count - int
          )
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 1, -- idBill - int
          5, -- idFood - int
          1  -- count - int
          )
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 2, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 2, -- idBill - int
          6, -- idFood - int
          2  -- count - int
          )
INSERT	dbo.BillInfo
        ( IdBill, IdFood, Count )
VALUES  ( 2, -- idBill - int
          5, -- idFood - int
          2  -- count - int
          )       
GO

select * from dbo.TableFood
select * from dbo.Bill
select * from dbo.Food
select * from dbo.BillInfo
select * from dbo.FoodCategory
GO

CREATE PROC USP_GetUncheckBillByTableId
@idTable int, @status bit
AS 
BEGIN
	SELECT * FROM dbo.Bill WHERE @idtable = IdTable and @status = Status
END
GO
CREATE PROC USP_GetBillInfoByIdBill
@idBill int
AS 
BEGIN
	SELECT * FROM dbo.BillInfo WHERE @idBill =IdBill
END
GO

USP_GetUncheckBillByTableId @idTable = 4, @status = 0
USP_GetBillInfoByIdBill @idBill = 1

select f.Name, bi.count, f.Price, bi.Count*f.Price as TotalPrice
from Bill b, BillInfo bi, Food f
where b.Id = bi.IdBill and bi.IdFood=f.Id and b.Status=0 and b.IdTable = 4