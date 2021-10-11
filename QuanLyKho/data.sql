-- USE Temp
-- DROP DATABASE QuanLyKho

CREATE DATABASE QuanLyKho
GO


CREATE TABLE UNIT
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
)
GO

CREATE TABLE Supplier
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Addresses NVARCHAR(MAX),
    Phone NVARCHAR(20),
    Email NVARCHAR(200),
    MoreInfo NVARCHAR(MAX),
    ContractDate DATETIME,
)
GO

CREATE TABLE Customer
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Addresses NVARCHAR(MAX),
    Phone NVARCHAR(20),
    Email NVARCHAR(200),
    MoreInfo NVARCHAR(MAX),
    ContractDate DATETIME,
)
GO

CREATE TABLE Object
(
    Id NVARCHAR(128) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    IdUnit INT NOT NULL,
    IdSupplier INT NOT NULL,
    QRCode NVARCHAR(MAX),
    BarCode NVARCHAR(MAX),

    FOREIGN KEY (IdUnit) REFERENCES UNIT(Id),
    FOREIGN KEY (IdSupplier) REFERENCES Supplier(Id)
)
GO

CREATE TABLE UserRole
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
)
GO

INSERT INTO UserRole
    (DisplayName)
VALUES
    (N'Admin'),
    (N'Nhân viên')
GO

CREATE TABLE Users
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    UserName NVARCHAR(100),
    PassWord NVARCHAR(MAX),
    IdRole INT NOT NULL,

    FOREIGN KEY (IdRole) REFERENCES UserRole(Id),

)
GO

INSERT INTO Users
    (DisplayName, UserName, PassWord, IdRole)
VALUES
    (N'Admin', 'Admin', N'db69fc039dcbd2962cb4d28f5891aae1', 1),
    (N'Nhân viên', N'Staff', N'978aae9bb6bee8fb75de3e4830a1be46', 2 )
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
    OutputPrice FLOAT DEFAULT 0,
    Status NVARCHAR(Max),


    FOREIGN KEY (IdObject) REFERENCES Object(Id),
    FOREIGN KEY (IdInput) REFERENCES Input(Id),
)
GO

CREATE TABLE Output
(
    Id NVARCHAR(128) PRIMARY KEY,
    DateOutput DATETIME
)
GO

CREATE TABLE OutputInfo
(
    Id NVARCHAR(128) PRIMARY KEY,
    IdObject NVARCHAR(128) NOT NULL,
    IdInputInfo NVARCHAR(128) NOT NULL,
    IdCustom INT NOT NULL,
    Count INT,
    InputPrice FLOAT DEFAULT 0,
    OutputPrice FLOAT DEFAULT 0,
    Status NVARCHAR(Max),

    FOREIGN KEY (IdObject) REFERENCES Object(Id),
    FOREIGN KEY (IdInputInfo) REFERENCES InputInfo(Id),
    FOREIGN KEY (IdCustom) REFERENCES Customer(Id),
)
GO
