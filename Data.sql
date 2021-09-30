
--CREATE DATABASE Temp
--USE Temp
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
	Status BIT NOT NULL DEFAULT 0, -- 1: đã thanh toán && 0: chưa thanh toán
	Discount INT DEFAULT 0,
	TotalPrice FLOAT DEFAULT 0

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
INSERT dbo.TableFood
	(Name)
VALUES
	(N'Bàn 0')

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
SET Status = 0 
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
INSERT dbo.FoodCategory
	( Name )
VALUES
	( N'Cà phê' ),
	( N'Trà sữa' ),
	( N'Sinh tố' ),
	( N'Nước ép' )
GO
SELECT * FROM FoodCategory
-- Add food
INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 ),
	( N'Cà phê sữa', 1, 15000 ),
	( N'Trà sữa trân châu', 2, 20000 ),
	( N'Sinh tố bơ', 3, 15000 ),
	( N'Nước ép cam', 3, 10000 )
DELETE dbo.BillInfo
DBCC CHECKIDENT ('BillInfo', RESEED, 0)
GO
DELETE dbo.Bill
DBCC CHECKIDENT ('Bill', RESEED, 0)
GO
DELETE dbo.Food
WHERE 1 = 1
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
SELECT * FROM FoodCategory
-- Add food
INSERT dbo.Food
	( Name, IdCategory, Price )
VALUES
	( N'Cà phê đen', 1, 12000 ),
	( N'Cà phê sữa', 1, 15000 ),
	( N'Trà sữa trân châu', 2, 20000 ),
	( N'Sinh tố bơ', 3, 15000 ),
	( N'Nước ép cam', 4, 10000 )
SELECT * FROM Bill

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
	WHERE IdTable = @idTable AND Status = 0
END
GO

EXEC USP_GetBill @idTable = 54
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

EXEC USP_GetMenu @idTable = 53
GO

CREATE PROC USP_InsertBill
	@idTable INT
AS
BEGIN
	INSERT	dbo.Bill
		( DateCheckOut , IdTable)
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


SELECT *
FROM dbo.FoodCategory
SELECT *
FROM dbo.Food
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




SELECT *
FROM FoodCategory
SELECT *
FROM Food
GO
-- proc for insert new bill

-- CREATE PROC USP_InsertBill
-- 	@idTable INT
-- AS
-- BEGIN
-- 	INSERT dbo.Bill
-- 		( DateCheckIn, DateCheckOut, idTable, Status )
-- 	VALUES
-- 		( GETDATE(), NULL, @idTable, 0 )
-- END
-- GO

-- proc for insert new BillInfo
-- CREATE PROC USP_InsertBillInfo
-- 	@idBill INT,
-- 	@idFood int,
-- 	@count INT
-- AS
-- BEGIN
-- 	INSERT	dbo.BillInfo
-- 		( idBill, idFood, count )
-- 	VALUES
-- 		( @idBill, @idFood, @count )
-- END
-- GO
-- -- Alter proc insert new BillInfo
-- ALTER PROC USP_InsertBillInfo
-- 	@idBill INT,
-- 	@idFood int,
-- 	@count INT
-- AS
-- BEGIN

-- 	DECLARE @isExitBillInfo INT;
-- 	DECLARE @foodCount INT = 1;
-- 	SELECT @isExitBillInfo = dbo.BillInfo.Id, @foodCount = Count
-- 	FROM dbo.BillInfo
-- 	WHERE IdBill = @idBill AND IdFood = @idFood

-- 	IF (@isExitBillInfo > 0) 
-- 	BEGIN
-- 		DECLARE @newCount INT = @foodCount + @count;
-- 		IF (@newCount > 0)
-- 			UPDATE dbo.BillInfo SET Count = @foodCount + @count WHERE IdBill = @idBill AND IdFood = @idFood
-- 		ELSE 
-- 			DELETE dbo.BillInfo WHERE IdBill = @idBill AND IdFood = @idFood
-- 	END
-- 	ELSE 
-- 	BEGIN
-- 		INSERT	dbo.BillInfo
-- 			( idBill, idFood, count )
-- 		VALUES
-- 			( @idBill, @idFood, @count )
-- 	END
-- END
-- GO

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

	UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTable
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill AFTER UPDATE
AS
BEGIN
	DECLARE @idTableOld INT
	SELECT @idTableOld = idTable FROM deleted
	DECLARE @countOld INT = 0
	SELECT @countOld = COUNT(*)
	FROM dbo.Bill
	WHERE IdTable = @idTableOld AND Status = 0
	IF (@countOld = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTableOld
	ELSE
		UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTableOld

	DECLARE @idTableNew INT
	SELECT @idTableNew = idTable FROM inserted
	DECLARE @countNew INT = 0
	SELECT @countNew = COUNT(*)
	FROM dbo.Bill
	WHERE IdTable = @idTableNew AND Status = 0
	IF (@countNew = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTableNew
	ELSE
		UPDATE dbo.TableFood SET Status = 1 WHERE Id = @idTableNew
END
GO


CREATE PROC USP_UpdateBillByIdTable
	@idBill INT, @discount INT, @totalPrice FLOAT
AS
BEGIN
	UPDATE dbo.Bill SET DateCheckOut = GETDATE(), Discount = @discount, Status = 1, TotalPrice = @totalPrice WHERE Id = @idBill
END
GO



CREATE PROC USP_SwitchTable
@idTable1 INT, @idTable2 INT
AS
BEGIN
	DECLARE @idBill1 INT 
	DECLARE @idBill2 INT
	SELECT @idBill1 = Id FROM dbo.Bill WHERE IdTable = @idTable1 AND Status = 0
	SELECT @idBill2 = Id FROM dbo.Bill WHERE IdTable = @idTable2 AND Status = 0
	IF (@idBill1 IS NOT NULL) 
	BEGIN
		UPDATE dbo.Bill
		SET IdTable = @idTable2
		WHERE Id = @idBill1
	END
	IF (@idBill2 IS NOT NULL) 
	BEGIN
		UPDATE dbo.Bill
		SET IdTable = @idTable1
		WHERE Id = @idBill2
	END
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo
AFTER DELETE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = IdBill FROM deleted

	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.BillInfo
	WHERE IdBill = @idBill

	IF (@count <= 0)
		DELETE FROM Bill WHERE Id = @idBill AND Status = 0
END
GO

CREATE TRIGGER UTG_DeleteBill
ON dbo.Bill
AFTER DELETE
AS
BEGIN
	DECLARE @idTable INT
	SELECT @idTable = IdTable FROM deleted

	DECLARE @status BIT
	SELECT @status = Status FROM deleted

	IF (@status = 0)
		UPDATE dbo.TableFood SET Status = 0 WHERE Id = @idTable
END
GO

CREATE PROC USP_GetBillListByDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.Name AS [Tên bàn], b.TotalPrice AS [Tổng tiền], DateCheckIn , DateCheckOut , Discount AS [Giảm giá]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut and b.Status = 1
	AND t.Id = b.IdTable
END
GO

SELECT * FROM BIll
