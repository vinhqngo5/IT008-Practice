-- USE TEMP
-- DROP DATABASE IF EXISTS QUANLYKHO


CREATE DATABASE QUANLYKHO
GO

USE QUANLYKHO
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
    BarCode NVARCHAR(MAX)

        FOREIGN KEY(IdUnit) REFERENCES Unit(Id),
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
    IdSupplier INT NOT NULL,
    DateInput DATETIME

        FOREIGN KEY(IdSupplier) REFERENCES Supplier(Id)
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
    IdInputInfo NVARCHAR(128) NOT NULL,
    Count INT,
    OutputPrice FLOAT DEFAULT 0,
    Status NVARCHAR(MAX)

        FOREIGN KEY (IdOutput) REFERENCES Output(Id),
    FOREIGN KEY (IdInputInfo) REFERENCES InputInfo(Id)
)
GO
