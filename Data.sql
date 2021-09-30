
--CREATE DATABASE Temp
USE Temp
DROP DATABASE QUANLYQUANCAFE
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
		N'staff', -- UserName - nvarchar(100)
		N'staff', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		0  -- Type - bit
	),
	(
		N'Admin', -- UserName - nvarchar(100)
		N'Admin', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		0  -- Type - bit
	),
	(
		N'K9', -- UserName - nvarchar(100)
		N'RongK9', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		1  -- Type - bit
	)
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
DBCC CHECKIDENT ('TableFood', RESEED, 0)
GO
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
CREATE PROC USP_GetTableList
AS
SELECT *
FROM TableFood
GO

-- Add data for FoodCategories, food, Bill, BillInfo
-- Delete old data in table and reset id
DELETE dbo.BillInfo
DBCC CHECKIDENT ('BillInfo', RESEED, 0)
GO
DELETE dbo.Bill
DBCC CHECKIDENT ('Bill', RESEED, 0)
GO
DELETE dbo.Food
DBCC CHECKIDENT ('Food', RESEED, 0)
GO
DELETE dbo.FoodCategory
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
GO
SELECT *
FROM FoodCategory
-- Add food
INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 ),
	( N'Cà phê sữa', 1, 15000 ),
	( N'Trà sữa trân châu', 2, 20000 ),
	( N'Sinh tố bơ', 3, 15000 ),
	( N'Nước ép cam', 4, 10000 )
SELECT *
FROM Bill

-- -- Add bill
-- INSERT	dbo.Bill
-- 	( DateCheckIn , DateCheckOut , IdTable , Status )
-- VALUES
-- 	( GETDATE() , NULL , 1 , 0 ),
-- 	( GETDATE() , NULL , 2 , 0 ),
-- 	( GETDATE() , GETDATE() , 3 , 1 )

-- -- Add bill info
-- INSERT	dbo.BillInfo
-- 	( idBill, idFood, count )
-- VALUES
-- 	( 1, 1, 2 ),
-- 	( 1, 2, 1 ),
-- 	( 2, 3, 3 ),
-- 	( 3, 4, 5 )
-- GO

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

-- UPDATE TableFood
-- SET Status = 0
-- WHERE Id = 6 OR Id = 9

CREATE PROC USP_GetBill
	@idTable INT
AS
BEGIN
	SELECT *
	FROM Bill
	WHERE IdTable = @idTable AND Status = 0
END
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

CREATE PROC USP_GetMenu
	@idTable INT
AS
BEGIN
	SELECT Name, Price, Count, Price * Count as TotalPrice
	FROM Food, Bill, BillInfo
	WHERE Bill.IdTable = @idTable AND Bill.Id = BillInfo.IdBill AND BillInfo.IdFood = Food.Id AND Bill.Status = 0
END
GO

CREATE PROC USP_InsertBill
	@idTable INT
AS
BEGIN
	INSERT	dbo.Bill
		( DateCheckOut , IdTable, Discount)
	VALUES
		( NULL , @idTable, 0  )
END
GO

CREATE PROC USP_InsertBillInfo
	@idBill INT,
	@idFood INT,
	@count INT
AS
BEGIN
	DECLARE @isExistBillInfo INT,
			@foodCount INT = 1

	SELECT @isExistBillInfo = Id, @foodCount = Count
	FROM dbo.BillInfo
	WHERE IdBill = @idBill AND IdFood = @idFood

	IF (@isExistBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @count + @foodCount
		IF @newCount > 0
			UPDATE BillInfo SET Count = @newCount WHERE IdFood = @idFood
		ELSE
			DELETE FROM BillInfo WHERE IdFood = @idFood AND IdBill = @idBill
	END
	ELSE
	BEGIN
		IF @count > 0
			INSERT	BillInfo
			( IdBill, IdFood, Count )
		VALUES
			( @idBill, @idFood, @count )
	END
END
GO

CREATE PROC USP_GetFoodByCategoryId
	@idCategory INT
AS
BEGIN
	SELECT *
	FROM dbo.Food
	WHERE @idCategory = IdCategory
END
GO
USP_GetFoodByCategoryId @idCategory = 1
GO

CREATE PROC USP_InsertBill
	@idTable INT
AS
BEGIN
	INSERT dbo.Bill
		(DateCheckOut,IdTable)
	VALUES
		(NULL, @idTable)
END
GO
USP_InsertBill @idTable =5
GO


-- Create trigger for update BillInfo
CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill = IdBill
	FROM Inserted

	DECLARE @idTable INT

	SELECT @idTable = IdTable
	FROM dbo.Bill
	WHERE Id = @idBill AND Status = 0

	DECLARE @countBillInfo Int
	SELECT @countBillInfo = COUNT(*)
	FROM dbo.BillInfo
	WHERE idBill = @idBill

	UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTable
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill = Id
	FROM Inserted

	DECLARE @idTable INT

	SELECT @idTable = IdTable
	FROM dbo.Bill
	WHERE Id = @idBill

	DECLARE @count int = 0

	SELECT @count = COUNT(*)
	FROM dbo.Bill
	WHERE IdTable = @idTable AND Status = 0

	IF (@count = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTable
END
GO


CREATE PROC USP_UpdateBillByIdTable
	@idBill INT,
	@discount INT
AS
BEGIN
	UPDATE Bill SET DateCheckOut = GETDATE(), Status = 1, Discount = @discount WHERE Id = @idBill
END
GO

USP_UpdateBillByIdTable @idBill = 1, @discount = 5
GO


ALTER TABLE dbo.Bill
ADD Discount INT DEFAULT 0

UPDATE dbo.Bill SET Discount = 0

SELECT *
FROM dbo.Bill
GO

ALTER PROC USP_SwitchTable
	@idTable1 INT,
	@idTable2 INT
AS
BEGIN

	DECLARE @idFirstBill INT
	DECLARE @idSecondBill INT
	DECLARE @isFirstTableEmpty INT  = 1
	DECLARE @isSecondTableEmpty INT  = 1


	-- Find bill id of table
	SELECT @idFirstBill = Id
	FROM Bill
	WHERE IdTable = @idTable1 AND Status = 0


	SELECT @idSecondBill = Id
	FROM Bill
	WHERE IdTable = @idTable2 AND Status = 0

	-- If not exist bill in table -> create bill
	IF (@idFirstBill IS NULL) 
	BEGIN
		EXEC USP_InsertBill @idTable = @idTable1
		SELECT @idFirstBill = Id
		FROM Bill
		WHERE IdTable = @idTable1 AND Status = 0

	END

	IF (@idSecondBill IS NULL) 
	BEGIN
		EXEC USP_InsertBill @idTable = @idTable2
		SELECT @idSecondBill = Id
		FROM Bill
		WHERE IdTable = @idTable2 AND Status = 0

	END

	PRINT @idFirstBill
	PRINT @idSecondBill

	-- declare table instead of create new table then drop
	DECLARE @billInfoIdRange TABLE(Id INT)

	INSERT INTO @billInfoIdRange
	SELECT Id
	FROM dbo.BillInfo
	WHERE IdBill = @IdSecondBill

	SELECT *
	FROM @billInfoIdRange

	UPDATE dbo.BillInfo SET IdBill = @IdSecondBill WHERE IdBill = @IdFirstBill

	UPDATE dbo.BillInfo SET IdBill = @IdFirstBill WHERE Id
	IN (SELECT *
	FROM @billInfoIdRange)

	SELECT @isFirstTableEmpty = COUNT(*)
	FROM dbo.BillInfo
	WHERE idBill = @idFirstBill

	SELECT @isSecondTableEmpty = COUNT(*)
	FROM dbo.BillInfo
	WHERE idBill = @idSecondBill

	IF(@isFirstTableEmpty = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTable1
	IF(@isSecondTableEmpty = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTable2

END
GO
