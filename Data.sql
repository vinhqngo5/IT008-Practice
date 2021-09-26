-- CREATE DATABASE Temp
-- USE Temp
-- DROP DATABASE QUANLYQUANCAFE
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
	Id INT IDENTITY(1, 1) PRIMARY KEY,
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
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	IdCategory INT NOT NULL,
	Price FLOAT NOT NULL DEFAULT 0

		FOREIGN KEY (IdCategory) REFERENCES dbo.FoodCategory(Id)
)
GO

CREATE TABLE Bill
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	IdTable INT NOT NULL,
	Status BIT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán

		FOREIGN KEY (IdTable) REFERENCES dbo.TableFood(Id)
)
GO

CREATE TABLE BillInfo
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
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

-- Create procedure for login

CREATE PROC USP_Login
	@UserName nvarchar(100),
	@passWord nvarchar(100)
AS
BEGIN
	SELECT *
	FROM Account
	WHERE UserName = @userName AND PassWord =@passWord
END
GO

-- Create value for TableFood
DELETE FROM dbo.TableFood
WHERE 1 = 1
GO
DBCC CHECKIDENT ('TableFood', RESEED, 0)
DECLARE @i INT = 1
WHILE @i <= 20
BEGIN
	INSERT dbo.TableFood
		(Name)
	VALUES
		(N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO

UPDATE  dbo.TableFood
SET Status = 1 WHERE Id = 9 OR Id = 15
GO

-- Show value in TableFood
SELECT *
FROM dbo.TableFood
GO

CREATE PROC USP_GetTableList
AS
SELECT *
FROM TableFood
GO

EXEC USP_GetTableList
GO

-- Add data for FoodCategories, food, Bill, BillInfo
-- Delete old data in table and reset id
DELETE FROM dbo.BillInfo
WHERE 1 = 1
DBCC CHECKIDENT ('BillInfo', RESEED, 0)
GO
DELETE FROM dbo.Bill
WHERE 1 = 1
DBCC CHECKIDENT ('Bill', RESEED, 0)
GO
DELETE FROM dbo.Food
WHERE 1 = 1
DBCC CHECKIDENT ('Food', RESEED, 0)
GO
DELETE FROM dbo.FoodCategory
WHERE 1 = 1
DBCC CHECKIDENT ('FoodCategory', RESEED, 0)
GO

-- Add categories
INSERT dbo.FoodCategory
	( Name )
VALUES
	( N'Cà phê' ),
	( N'Trà sữa' ),
	( N'Sinh tố' ),
	( N'Nước ép' )

-- Add food
INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 ),
	( N'Cà phê sữa', 1, 15000 ),
	( N'Trà sữa trân châu', 2, 20000 ),
	( N'Sinh tố bơ', 3, 15000 ),
	( N'Nước ép cam', 4, 10000 )

-- Add bill
INSERT	dbo.Bill
	( DateCheckIn , DateCheckOut , IdTable , Status )
VALUES
	( GETDATE() , NULL , 1 , 0 ),
	( GETDATE() , NULL , 2 , 0 ),
	( GETDATE() , GETDATE() , 3 , 1 )

-- Add bill info
INSERT	dbo.BillInfo
	( idBill, idFood, count )
VALUES
	( 1, 1, 2 ),
	( 1, 2, 1 ),
	( 2, 3, 3 ),
	( 3, 4, 5 )
GO

SELECT *
FROM TableFood
SELECT *
FROM FoodCategory
SELECT *
FROM Food
SELECT *
FROM Bill
SELECT *
FROM BillInfo

UPDATE TableFood
SET Status = 1
WHERE Id = 6 OR Id = 9

SELECT *
FROM Bill
WHERE IdTable = 53 AND STATUS = 0
GO

CREATE PROC USP_GetBill
	@idTable INT
AS
BEGIN
	SELECT *
	FROM Bill
	WHERE IdTable = @idTable
END
GO

EXEC USP_GetBill @idTable = 55
GO

CREATE PROC USP_GetBillInfo
	@idBill INT
AS
BEGIN
	SELECT *
	FROM BillInfo
	WHERE IdBill = @idBill
END
GO

EXEC USP_GetBillInfo @idBill = -1
GO

CREATE PROC USP_GetMenu
	@idTable INT
AS
BEGIN
	SELECT Name, Price, Count, Price * Count as TotalPrice
	FROM Food, Bill, BillInfo
	WHERE Bill.IdTable = @idTable AND Bill.Id = BillInfo.IdBill AND BillInfo.IdFood = Food.Id AND Bill.Status = 0
END
GO

EXEC USP_GetMenu @idTable = 55


