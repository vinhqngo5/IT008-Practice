-- DROP DATABASE QuanLyQuanCafe1
CREATE DATABASE QuanLyQuanCafe
GO
USE QuanLyQuanCafe
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
  Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
  Status NVARCHAR(100) NOT NULL DEFAULT N'Trống',
  -- Trống || Có người

)
GO

CREATE TABLE Account
(
  UserName NVARCHAR(100) PRIMARY KEY,
  DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
  Password NVARCHAR(1000) NOT NULL DEFAULT 0,
  Type INT NOT NULL DEFAULT 0,
  -- 1: Admin && 0:staff
)
GO

CREATE TABLE FoodCategory
(
  Id INT IDENTITY PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
)
GO

CREATE TABLE Food
(
  Id INT IDENTITY PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
  IdCategory INT NOT NULL,
  Price FLOAT NOT NULL DEFAULT 0,

  FOREIGN KEY (IdCategory) REFERENCES dbo.FoodCategory(Id)
)
GO

CREATE TABLE Bill
(
  Id INT IDENTITY PRIMARY KEY,
  DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
  DateCheckOut DATE,
  IdTable INT NOT NULL,
  Status INT NOT NULL DEFAULT 0,
  -- 1: đã thanh toán && 0: chưa thanh toán

  FOREIGN KEY (IdTable) REFERENCES dbo.TableFood(Id)

)
GO

CREATE TABLE BillInfo
(
  Id INT IDENTITY PRIMARY KEY,
  IdBill INT NOT NULL,
  IdFood INT NOT NULL,
  Count INT NOT NULL DEFAULT 0,

  FOREIGN KEY (IdBill) REFERENCES dbo.Bill(Id),
  FOREIGN KEY (IdFood) REFERENCES dbo.Food(Id)
)