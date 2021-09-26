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
FROM dbo.TableFood
GO

EXEC dbo.USP_GetTableList
GO

-- Init data

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

-- Create value for Category

INSERT dbo.FoodCategory
	(Name)
VALUES
	(N'Hải sản')
INSERT dbo.FoodCategory
	(Name)
VALUES
	(N'Nông sản')
INSERT dbo.FoodCategory
	(Name)
VALUES
	(N'Lâm sản')
INSERT dbo.FoodCategory
	(Name)
VALUES
	(N'Nước')

-- Create value for Food

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'Mực một nắng nướng sa tế', -- Name -- nvarchar(100)
		1, -- IdCategory - int
		120000   -- Price - float
	)

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'Nghêu hấp xả', -- Name -- nvarchar(100)
		1, -- IdCategory - int
		50000   -- Price - float
	)

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'Dú dê nướng sữa', -- Name -- nvarchar(100)
		2, -- IdCategory - int
		60000   -- Price - float
	)

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'Heo rừng nướng muối ớt', -- Name -- nvarchar(100)
		3, -- IdCategory - int
		75000   -- Price - float
	)

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'7Up', -- Name -- nvarchar(100)
		4, -- IdCategory - int
		15000   -- Price - float
	)

INSERT dbo.Food
	(Name, IdCategory, Price)
VALUES(
		N'Cafe', -- Name -- nvarchar(100)
		4, -- IdCategory - int
		12000   -- Price - float
	)

-- Create value for Bill

INSERT dbo.Bill
	(DateCheckIn, DateCheckOut, IdTable, Status)
VALUES(
		GETDATE(), -- DateCheckIn -- date
		NULL, -- DateCheckOut -- date
		1, -- IdTable - int
		0   -- Status - bit
	)

INSERT dbo.Bill
	(DateCheckIn, DateCheckOut, IdTable, Status)
VALUES(
		GETDATE(), -- DateCheckIn -- date
		NULL, -- DateCheckOut -- date
		2, -- IdTable - int
		0   -- Status - bit
	)

INSERT dbo.Bill
	(DateCheckIn, DateCheckOut, IdTable, Status)
VALUES(
		GETDATE(), -- DateCheckIn -- date
		GETDATE(), -- DateCheckOut -- date
		2, -- IdTable - int
		1   -- Status - bit
	)

-- Create value for BillInFo
-- For bill 1
INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		1, -- IdBill -- int
		1, -- IdFood -- int
		2 -- Count - int
	)

INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		1, -- IdBill -- int
		3, -- IdFood -- int
		4 -- Count - int
	)
INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		1, -- IdBill -- int
		5, -- IdFood -- int
		1 -- Count - int
	)

-- For bill 2
INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		2, -- IdBill -- int
		1, -- IdFood -- int
		2 -- Count - int
	)

INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		2, -- IdBill -- int
		6, -- IdFood -- int
		2 -- Count - int
	)

-- For bill 3
INSERT dbo.BillInfo
	(IdBill, IdFood, Count)
VALUES(
		3, -- IdBill -- int
		5, -- IdFood -- int
		2 -- Count - int
	)
GO


SELECT *
FROM dbo.Bill
SELECT *
FROM dbo.BillInfo
SELECT *
FROM dbo.Food
SELECT *
FROM dbo.FoodCategory

SELECT *
FROM dbo.Bill
WHERE IdTable = 1 AND Status = 0

SELECT f.Name, bi.Count, f.Price, f.Price * bi.[Count] AS TotalPrice
FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f
WHERE b.IdTable = 2 AND bi.IdBill = b.Id AND bi.IdFood = f.Id 
