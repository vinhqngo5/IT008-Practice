-- USE TEMP
-- DROP DATABASE IF EXISTS QuanLyKho


CREATE DATABASE QuanLyKho
GO

USE QuanLyKho
GO

CREATE TABLE Unit
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX)
)
GO

CREATE TABLE Supplier
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Address NVARCHAR(MAX),
    Phone NVARCHAR(20),
    Email NVARCHAR(200),
    MoreInfo NVARCHAR(MAX),
    ContractDate DATETIME
)
GO

CREATE TABLE Customer
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Address NVARCHAR(MAX),
    Phone NVARCHAR(20),
    Email NVARCHAR(200),
    MoreInfo NVARCHAR(MAX),
    ContractDate DATETIME
)
GO

CREATE TABLE Object
(
    Id NVARCHAR(128) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    IdUnit INT NOT NULL,
    QrCode NVARCHAR(MAX),
    BarCode NVARCHAR(MAX),
    IdSupplier INT NOT NULL,
    OutputPrice FLOAT DEFAULT 0,

    FOREIGN KEY
    (IdSupplier) REFERENCES Supplier
    (Id),
    FOREIGN KEY
    (IdUnit) REFERENCES Unit
    (Id),
)
GO

CREATE TABLE UserRole
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX)
)
GO

INSERT INTO UserRole
    (DisplayName)
VALUES(N'ADMIN')
GO
INSERT INTO UserRole
    (DisplayName)
VALUES(N'NHÂN VIÊN')
GO

CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    UserName NVARCHAR(100),
    PassWord NVARCHAR(MAX),
    IdRole INT NOT NULL

        FOREIGN KEY (IdRole) REFERENCES UserRole(Id)
)
GO
INSERT INTO Users
    (DisplayName, UserName, PassWord, IdRole)
VALUES(N'RONGK9', N'ADMIN', N'1', 1)
GO
INSERT INTO Users
    (DisplayName, UserName, PassWord, IdRole)
VALUES(N'NHÂN VIÊN', N'STAFF', N'1', 2)
GO

CREATE TABLE Input
(
    Id NVARCHAR(128) PRIMARY KEY,
    DateInput DATETIME
)
GO

CREATE TABLE InputInfo
(
    Id NVARCHAR(128) PRIMARY KEY,
    IdObject NVARCHAR(128) NOT NULL,
    IdInput NVARCHAR(128) NOT NULL,
    Count INT,
    InputPrice FLOAT DEFAULT 0,
    Status NVARCHAR(MAX)

        FOREIGN KEY (IdObject) REFERENCES Object(Id),
    FOREIGN KEY (IdInput) REFERENCES Input(Id)
)
GO

CREATE TABLE Output
(
    Id NVARCHAR(128) PRIMARY KEY,
    IdCustomer INT NOT NULL,
    DateOutput DATETIME

        FOREIGN KEY (IdCustomer) REFERENCES Customer(Id)
)
GO

CREATE TABLE OutputInfo
(
    Id NVARCHAR(128) PRIMARY KEY,
    IdOutput NVARCHAR(128) NOT NULL,
    IdObject NVARCHAR(128) NOT NULL,
    Count INT,
    OutputPrice FLOAT DEFAULT 0,
    Status NVARCHAR(MAX)

        FOREIGN KEY (IdOutput) REFERENCES Output(Id),
    FOREIGN KEY (IdObject) REFERENCES Object(Id)
)
GO

INSERT INTO Unit
    (DisplayName )
VALUES
    (N'Kg'),
    (N'Thùng'),
    (N'Bao')
GO

INSERT INTO Supplier
    (DisplayName, Address, Phone, Email)
VALUES
    (N'Kteam1', N'Vũng Tàu', N'12324215', N'email@gmail.com')
GO


INSERT INTO Object
    (Id, DisplayName, IdUnit, IdSupplier, OutputPrice)
VALUES
    (N'1', N'Xi măng', 1, 1, 20000),
    (N'2', N'Gạch', 1, 1, 500)
GO

INSERT INTO Input
    (Id, DateInput)
VALUES
    (N'1', '10-14-2021'),
    (N'2', '10-14-2021')
GO

INSERT INTO InputInfo
    (Id, IdObject, IdInput, Count, InputPrice)
VALUES
    (N'1', N'1', N'1', 50, 200000),
    (N'2', N'2', N'1', 1000, 200)
GO

INSERT INTO Customer
    (DisplayName, Address, Phone, Email)
VALUES
    (N'K9', N'address', N'1231231', N'email@gmail.com'),
    (N'ProVip', N'address2', N'364356', N'email2@gmail.com')
GO

INSERT INTO Output
    (Id, DateOutput, IdCustomer)
VALUES
    (N'1', '10-14-2021', 1),
    (N'2', '10-14-2021', 2)
GO

INSERT INTO OutputInfo
    (Id, IdOutput, IdObject, Count, OutputPrice)
VALUES
    (N'1', N'1', N'1', 50, 240000),
    (N'2', N'2', N'1', 1000, 500)
GO
