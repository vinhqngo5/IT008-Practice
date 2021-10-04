--CREATE DATABASE Temp
-- USE Temp
-- DROP DATABASE QUANLYQUANCAFE
-- CREATE DATABASE QUANLYQUANCAFE
-- GO

USE QUANLYQUANCAFE
GO

-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo


BEGIN
	-- CREATE TABLE --
	CREATE TABLE TableFood
	(
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
		Status BIT NOT NULL DEFAULT 0,
		-- Trống: 0 || Có người: 1
	)

	CREATE TABLE Account
	(
		UserName NVARCHAR(100) PRIMARY KEY,
		DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Staff',
		PassWord NVARCHAR(100) NOT NULL DEFAULT 0,
		Type BIT NOT NULL DEFAULT 0
		-- 1: admin && 0: staff
	)

	CREATE TABLE FoodCategory
	(
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
	)

	CREATE TABLE Food
	(
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
		IdCategory INT NOT NULL,
		Price FLOAT NOT NULL DEFAULT 0,
		Status BIT NOT NULL DEFAULT 1,
		-- 1: còn phục vụ, ngưng phục vụ

		FOREIGN KEY
		(IdCategory) REFERENCES dbo.FoodCategory
		(Id)
	)

	CREATE TABLE Bill
	(
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
		DateCheckOut DATE,
		IdTable INT NOT NULL,
		Status BIT NOT NULL DEFAULT 0,
		-- 1: đã thanh toán && 0: chưa thanh toán
		Discount INT NOT NULL DEFAULT 0,
		TotalPrice FLOAT NOT NULL DEFAULT 0,

		FOREIGN KEY
		(IdTable) REFERENCES dbo.TableFood
		(Id)
	)

	CREATE TABLE BillInfo
	(
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		IdBill INT NOT NULL,
		IdFood INT NOT NULL,
		Count INT NOT NULL DEFAULT 0

			FOREIGN KEY (IdBill) REFERENCES dbo.Bill(Id),
		FOREIGN KEY (IdFood) REFERENCES dbo.Food(Id)
	)
END
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
		N'1', -- PassWord - nvarchar(100)
		0  -- Type - bit
	),
	(
		N'Admin', -- UserName - nvarchar(100)
		N'Admin', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(100)
		0  -- Type - bit
	),
	(
		N'K9', -- UserName - nvarchar(100)
		N'RongK9', -- DisplayName - nvarchar(100)
		N'1', -- PassWord - nvarchar(1000)
		0  -- Type - bit
	)

-- Insert for reseed
INSERT dbo.TableFood
	(Name)
VALUES
	(N'Bàn 1' )

DELETE FROM dbo.TableFood
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

-- Add data for FoodCategories, food, Bill, BillInfo

-- insert before reseed
INSERT dbo.FoodCategory
	( Name )
VALUES
	( N'Cà phê' )

INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 )

INSERT	dbo.Bill
	( DateCheckIn , DateCheckOut , IdTable , Status )
VALUES
	( GETDATE() , NULL , 1 , 0 )

INSERT	dbo.BillInfo
	( idBill, idFood, count )
VALUES
	( 1, 1, 2 )


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

SELECT *
FROM FoodCategory
GO

-- Add food
INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 ),
	( N'Cà phê sữa', 1, 15000 ),
	( N'Trà sữa trân châu', 2, 20000 ),
	( N'Sinh tố bơ', 3, 15000 ),
	( N'Nước ép cam', 4, 10000 )

GO

-- Add bill
-- INSERT	dbo.Bill
-- 	( DateCheckIn , DateCheckOut , IdTable , Status )
-- VALUES
-- 	( GETDATE() , NULL , 1 , 0 ),
-- 	( GETDATE() , NULL , 2 , 0 ),
-- 	( GETDATE() , GETDATE() , 3 , 1 )
-- GO

-- Add bill info
-- INSERT	dbo.BillInfo
-- 	( idBill, idFood, count )
-- VALUES
-- 	( 1, 1, 2 ),
-- 	( 1, 2, 1 ),
-- 	( 2, 3, 3 ),
-- 	( 3, 4, 5 )
--  GO

-- SELECT *
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
GO

-- STORED PROCEDURES --
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

CREATE PROC USP_GetTableList
AS
SELECT *
FROM TableFood

EXEC USP_GetTableList
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
		( DateCheckOut , IdTable )
	VALUES
		( NULL , @idTable  )
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
			UPDATE BillInfo SET Count = @newCount WHERE IdFood = @idFood AND IdBill = @idBill
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
	WHERE @idCategory = IdCategory AND STATUS = 1
END
GO

CREATE PROC USP_CheckOut
	@idBill INT,
	@discount INT,
	@totalPrice FLOAT
AS
BEGIN
	UPDATE Bill SET DateCheckOut = GETDATE(), Status = 1, Discount = @discount, TotalPrice = @totalPrice WHERE Id = @idBill
END
GO

CREATE PROC USP_SwitchTable
	@idFirstTable INT,
	@idSecondTable INT
AS
BEGIN
	DECLARE @idFirstBill INT, @idSecondBill INT
	DECLARE @dateCheckInFirstBill DATE, @dateCheckInSecondBill DATE

	SELECT @idFirstBill = Id, @dateCheckInFirstBill = DateCheckIn
	FROM Bill
	WHERE IdTable = @idFirstTable AND Status = 0
	SELECT @idSecondBill = Id, @dateCheckInSecondBill = DateCheckIn
	FROM Bill
	WHERE IdTable = @idSecondTable AND Status = 0

	IF @idFirstBill IS NULL
	BEGIN
		UPDATE Bill SET IdTable = @idFirstTable WHERE Id = @idSecondBill
		UPDATE TableFood SET Status = 0 WHERE Id = @idSecondTable
	END
	ELSE IF @idSecondBill IS NULL
	BEGIN
		UPDATE Bill SET IdTable = @idSecondTable WHERE Id = @idFirstBill
		UPDATE TableFood SET Status = 0 WHERE Id = @idFirstTable
	END
	ELSE
	BEGIN
		UPDATE Bill SET IdTable = @idSecondTable, DateCheckIn = @dateCheckInSecondBill WHERE Id = @idFirstBill
		UPDATE Bill SET IdTable = @idFirstTable, DateCheckIn = @dateCheckInFirstBill WHERE Id = @idSecondBill
	END
END
GO

CREATE PROC USP_GetListBillByDate
	@dateCheckIn DATE,
	@dateCheckOut DATE
AS
BEGIN
	SELECT Name AS [Tên bàn], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], Discount AS [Giảm giá], TotalPrice AS [Tổng tiền]
	FROM TableFood
		JOIN Bill ON Bill.IdTable = TableFood.Id
		JOIN BillInfo ON Bill.Id = BillInfo.IdBill
	WHERE Bill.Status = 1 AND DateCheckIn >= @dateCheckIn AND DateCheckOut <= @dateCheckOut
	ORDER BY DateCheckIn, Name
END
GO

CREATE PROC USP_UpdateAccount
	@userName NVARCHAR(100),
	@displayName NVARCHAR(100),
	@passWord NVARCHAR(100),
	@newPassWord NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0

	SELECT @isRightPass = COUNT(*)
	FROM dbo.Account
	WHERE UserName = @userName AND PassWord = @passWord

	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassWord = NULL OR @newPassWord = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassWord WHERE UserName = @userName
	END
END
GO

CREATE PROC USP_GetListFood
AS
BEGIN
	SELECT Id AS ID, Name, IdCategory, Price
	FROM Food WHERE Status = 1
END
GO

CREATE PROC USP_GetListCategoryFoodById
	@id INT
AS
BEGIN
	SELECT *
	FROM FoodCategory
	WHERE Id = @id
END
GO

-- TRIGGER --
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

	UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTable
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo AFTER DELETE
AS
BEGIN
	DECLARE @tableIdBill TABLE(Id INT)

	INSERT INTO @tableIdBill
	SELECT IdBill
	FROM Deleted
	SELECT *
	FROM @tableIdBill

	DECLARE @count INT

	SELECT @count = COUNT(*)
	FROM BillInfo
		JOIN @tableIdBill AS t
		ON t.Id = BillInfo.IdBill

	IF @count = 0  
		DELETE FROM Bill
		FROM dbo.Bill
		INNER JOIN @tableIdBill AS t
		ON Bill.Id = t.Id
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
	ELSE
		UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTable
END
GO

CREATE TRIGGER UTG_DeleteBill
ON dbo.Bill AFTER DELETE
AS
BEGIN
	DECLARE @tableIdBill TABLE(Id INT,
		Status INT)

	INSERT INTO @tableIdBill
	SELECT IdTable, Status
	FROM Deleted

	UPDATE TableFood 
	SET Status = 0 
	FROM TableFood
		JOIN @tableIdBill AS t
		ON TableFood.Id = t.Id
END
GO

UPDATE dbo.Account SET Type = 1 WHERE userName = N'admin'

UPDATE dbo.Food SET Name = N'', IdCategory = 5, Price = 0 WHERE Id = 6


UPDATE dbo.Food SET Status = 1 WHERE Id = 1
