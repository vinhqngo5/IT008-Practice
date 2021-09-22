﻿CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO

CREATE TABLE TableFood
(
	Id INT IDENTITY PRIMARY KEY, 
	nameTable Nvarchar(100) NOT NULL, 
	statusTable Nvarchar(100) DEFAULT N'Trống' NOT NULL
)
GO

CREATE TABLE Account
(
	Id INT IDENTITY PRIMARY KEY, 
	DisplayName NVARCHAR(100) NOT NULL, 
	UserName NVARCHAR(100) NOT NULL, 
	PassWord NVARCHAR(100) NOT NULL, 
	Type INT NOT NULL DEFAULT 0
)
GO

CREATE TABLE FoodCategory
(
	Id INT IDENTITY PRIMARY KEY, 
	Name NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE Food
(
	Id INT IDENTITY PRIMARY KEY, 
	Name NVARCHAR(100) NOT NULL,
	IdCategory INT NOT NULL, 
	Price INT NOT NULL

	FOREIGN KEY (IdCategory) REFERENCES dbo.FoodCategory(Id)
)
GO 

CREATE TABLE Bill
(
	Id INT IDENTITY PRIMARY KEY, 
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(), 
	DateCheckOut DATE,
	IdTable INT NOT NULL, 
	Status INT NOT NULL DEFAULT 0

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

