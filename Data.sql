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

EXEC dbo.USP_GetAccountByUserName @userName = N'K9' --nvarchar(100)
GO

SELECT *
FROM dbo.Account
WHERE UserName = N'K9' AND PassWord = N'1'
GO

CREATE PROC USP_Login
  @userName nvarchar(100),
  @passWord nvarchar(1000)
AS
BEGIN
  SELECT *
  FROM dbo.Account
  WHERE UserName = @userName AND PassWord = @passWord
END
GO

EXEC dbo.USP_Login @userName = 'admin', @passWord = '1'
GO

DECLARE @i INT = 1

WHILE @i <= 15
BEGIN
  INSERT dbo.TableFood
    (Name)
  VALUES
    ( N'Bàn ' + CAST(@i AS nvarchar(100)))
  SET @i = @i + 1
END
GO

DECLARE @i INT = 16

WHILE @i <= 20
BEGIN
  INSERT dbo.TableFood
    (Name, Status)
  VALUES
    ( N'Bàn ' + CAST(@i AS nvarchar(100)), 1)
  SET @i = @i + 1
END
GO

SELECT *
FROM TableFood
GO

CREATE PROC USP_GetTableList
AS
SELECT *
FROM TableFood
GO

EXEC USP_GetTableList
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
  ( GETDATE() , NULL , 61 , 0 ),
  ( GETDATE() , NULL , 53 , 0 ),
  ( GETDATE() , GETDATE() , 55 , 1 )

-- Add bill info
INSERT	dbo.BillInfo
  ( idBill, idFood, count )
VALUES
  ( 4, 1, 2 ),
  ( 2, 2, 1 ),
  ( 3, 3, 3 ),
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


