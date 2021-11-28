create database ShopDB

DROP DATABASE ShopDB

use ShopDB


CREATE TABLE Products
(
[Id] int primary key IDENTITY (1,1) NOT NULL,
[Name]  nvarchar(100) NOT NULL,
Constraint CK_Name_of_Products Check([Name] <>' ')
)


Insert INTO Products([Name])
VALUES
('DROBIMEX 250GR turkey'),
('ICELAND ESKIMO PLOMBİR 15% 80QR'),
('ANKARA 500GR MAKARON FIYONK'),
('3 JELANIYA 450GR KETCUP KABABLIG'),
('Bizim Tarla 500GR TOMAT PASTASI S/Q'),
('BONDUELLE 212ML KRASNAYA FASOL'),
('AZOVSKAYA 250GR XALVA PODSOLN.S ARAXISOM'),
('MPRO Sevimli Dad 1KG Kərə yağı'),
('Azərçay 100QR'),
('Karmen Un 1000QR')


CREATE TABLE Customers
(
[Id] int primary key IDENTITY (1,1) NOT NULL,
[Name]  NVARCHAR(40) NOT NULL,
Constraint CK_Name_of_Customers Check([Name] <>' ')
)


Insert INTO Customers([Name])
VALUES
('Ruslan Mustafayev'),
('Kamran Aliyev'),
('Rafael Xelilzade'),
('Huesyn Rustemli'),
('Teymur Elizade'),
('Murad Ismayilzade'),
('Zireddin Gulumcanli'),
('Senan Aliyev'),
('Semistan Elizamanli'),
('Mehdi Yaminli')


CREATE TABLE DetailsofOrder
(
[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
[isCash] BIT NOT NULL,
[DateOrder] DATETIME NOT NULL
)


Insert INTO DetailsofOrder([isCash],[DateOrder])
VALUES
(1,'2020-12-12'),
(0,'2021-12-12'),
(1,'2020-02-10'),
(0,'2021-01-11'),
(1,'2020-10-01'),
(0,'2021-11-02'),
(1,'2020-05-10'),
(1,'2021-12-18'),
(0,'2020-12-09'),
(0,'2021-08-22')


CREATE TABLE Orders
(
[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,

[CustomersId] INT  NOT NULL,
[ProductsId] INT  NOT NULL,
[DetailsofOrder] INT  NOT NULL,
Constraint FK_CustomersIdforOrders Foreign key ([CustomersId]) References Customers(Id) On Delete CASCADE On Update CASCADE,
Constraint FK_ProductsIdforOrders Foreign key ([ProductsId]) References Products(Id) On Delete CASCADE On Update CASCADE,
Constraint FK_DetailsofOrders Foreign key ([DetailsofOrder]) References DetailsofOrder(Id) On Delete CASCADE On Update CASCADE,
)


INSERT INTO Orders([CustomersId],[ProductsId],[DetailsofOrder])
VALUES
(1,1,1),
(1,2,3),
(2,3,2),
(2,4,4),
(3,5,5),
(3,6,7),
(4,7,6),
(4,8,8),
(5,9,1),
(5,10,3),
(6,1,4),
(6,2,3),
(7,3,2),
(7,4,1),
(8,6,1),
(8,5,4),
(9,6,10),
(10,5,5)



CREATE OR ALTER Procedure sp_UpdateProduct
@ProductId int,
@ProductName NVARCHAR(max)
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *FROM Products ))
	BEGIN
	   UPDATE  Products 
       SET  Products.[Name] = @ProductName
       WHERE Products.Id=@ProductId
	END
END



CREATE OR ALTER Procedure sp_InsertProduct
@ProductName NVARCHAR(max)
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *FROM Products ))
	BEGIN
      INSERT INTO Products([Name])
      VALUES(@ProductName)
	END
    ELSE
	BEGIN
	  delete from Products;
      DBCC CHECKIDENT ('ShopDB.dbo.Products', RESEED, 0);
      Insert into Products([Name])
      values(@ProductName)
	END
END







CREATE OR ALTER Procedure sp_MaxIdProducts
@MaxId int output
AS
SET NOCOUNT ON
BEGIN
set @MaxId =(
  SELECT Top(1) Products.Id 
  FROM Products
  ORDER BY Id DESC
  )
END


CREATE OR ALTER Procedure sp_MinIdProducts
@MinId int output
AS
SET NOCOUNT ON
BEGIN
set @MinId =(
  SELECT Top(1) Products.Id 
  FROM Products
  ORDER BY Id ASC
  )
END





CREATE OR ALTER Procedure sp_deleteProduct
@ProductId int
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *FROM Products ))
	BEGIN
	  DELETE FROM  Products 
	  WHERE Products.Id= @ProductId
	END
END




CREATE OR ALTER Procedure sp_updateCustomer
@CustomerId INT,
@CustomerName NVARCHAR(max)
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *from Customers ))
	BEGIN
	   UPDATE Customers 
       SET Customers.[Name] = @CustomerName
       where Customers.Id=@CustomerId
	END
END



CREATE OR ALTER Procedure sp_InsertCustomer
@CustomerName nvarchar(max)
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(SELECT *FROM Customers))
	BEGIN
      Insert into Customers([Name])
      values(@CustomerName)
	END
	ELSE
	BEGIN
	  delete from Customers;
      DBCC CHECKIDENT ('ShopDB.dbo.Customers', RESEED, 0);
      Insert into Customers([Name])
      values(@CustomerName)
	END
END




CREATE OR ALTER Procedure sp_MaxIdCustomer
@MaxId int output
AS
SET NOCOUNT ON
BEGIN
set @MaxId =(
  SELECT Top(1) Customers.Id 
  FROM Customers
  ORDER BY Id DESC
  )
END



CREATE OR ALTER Procedure sp_MinIdCustomer
@MinId int output
AS
SET NOCOUNT ON
BEGIN
set @MinId =(
  SELECT Top(1) Customers.Id 
  FROM Customers
  ORDER BY Id ASC
  )
END



CREATE OR ALTER Procedure sp_deleteCustomer
@CustomerId int
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *FROM Customers ))
	BEGIN
	  DELETE FROM  Customers 
	  WHERE Customers.Id= @CustomerId
	END
END



CREATE OR ALTER Procedure sp_updateDetailsofOrder
@DateorderId int,
@iscash int,
@dateorder datetime
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(Select *FROM DetailsofOrder ))
	BEGIN
	   UPDATE DetailsofOrder 
       SET  DetailsofOrder.isCash = @iscash,DetailsofOrder.DateOrder = @dateorder
       WHERE DetailsofOrder.Id=@DateorderId
	END
END




CREATE OR ALTER Procedure sp_InsertDetailsofOrder
@iscash bit,
@dateorder datetime
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS(SELECT * FROM DetailsofOrder ))
	BEGIN
      Insert INTO DetailsofOrder([isCash], [DateOrder])
      VALUES(@iscash, @dateorder)
	END
    ELSE
	BEGIN
	  delete from DetailsofOrder;
      DBCC CHECKIDENT ('ShopDB.dbo.DetailsofOrder', RESEED, 0);
      Insert into DetailsofOrder([isCash], [DateOrder])
      values(@iscash, @dateorder)
	END
END




CREATE OR ALTER Procedure sp_MaxIdDetailsofOrder
@MaxId int output
AS
SET NOCOUNT ON
BEGIN
set @MaxId =(
  SELECT Top(1) DetailsofOrder.Id 
  FROM  DetailsofOrder 
  ORDER BY Id DESC
  )
END



CREATE OR ALTER Procedure sp_MinIdDetailsofOrder
@MinId int output
AS
SET NOCOUNT ON
BEGIN
set @MinId =(
  SELECT Top(1) DetailsofOrder.Id 
  FROM DetailsofOrder
  ORDER BY Id ASC
  )
END



CREATE OR ALTER Procedure sp_deleteDetailsofOrder
@DetailsofOrderId int
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS( SELECT * FROM DetailsofOrder))
	BEGIN
	  DELETE FROM  DetailsofOrder 
	  WHERE DetailsofOrder.Id= @DetailsofOrderId
	END
END



CREATE OR ALTER Procedure sp_updateOrder
@OrderId int,
@CustumerId int,
@ProductId int,
@DetailsofOrderId int
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS
	(
	SELECT * FROM Orders 
	Inner Join Customers
	 On Customers.Id=Orders.[CustomersId]
	 Inner Join Products
	 On Products.Id=Orders.[ProductsId]
	 Inner Join DetailsofOrder
	 On DetailsofOrder.Id=Orders.[DetailsofOrder]
    )
    )
	BEGIN
	   UPDATE  Orders 
       SET  
	   Orders.[CustomersId] = @CustumerId,
	   Orders.[ProductsId] = @ProductId,
	   Orders.[DetailsofOrder] = @DetailsofOrderId
       WHERE Orders.Id=@OrderId
	END
END



CREATE OR ALTER Procedure sp_InsertOrder
@CustumerId int,
@ProductId int,
@DetailsofOrderId int
AS
SET NOCOUNT ON
BEGIN
  IF(EXISTS(
     SELECT *
     FROM Orders 
	 Inner Join Customers
	 On Customers.Id=Orders.[CustomersId]
	 Inner Join Products
	 On Products.Id=Orders.[ProductsId]
	 Inner Join DetailsofOrder
	 On DetailsofOrder.Id=Orders.[DetailsofOrder]
    )
    )
	BEGIN
      Insert into Orders([CustomersId],[ProductsId],[DetailsofOrder] )
      values(@CustumerId, @ProductId, @DetailsofOrderId)
	END
	ELSE
	BEGIN
	  delete from Orders;
      DBCC CHECKIDENT ('ShopDB.dbo.Orders', RESEED, 0);
      Insert into Orders([CustomersId],[ProductsId], [DetailsofOrder] )
      values(@CustumerId, @ProductId, @DetailsofOrderId)
	END
END






CREATE OR ALTER Procedure sp_MaxIdOrder
@MaxId int output
AS
SET NOCOUNT ON
BEGIN
set @MaxId =(
  SELECT 
  Top(1) 
  Orders.Id 
  FROM 
  Orders ORDER BY Id DESC
  )
END




CREATE OR ALTER Procedure sp_MinIdOrder
@MinId int output
AS
SET NOCOUNT ON
BEGIN
set @MinId =(
  SELECT 
  Top(1) 
  Orders.Id 
  FROM 
  Orders ORDER BY Id ASC
  )
END



CREATE OR ALTER Procedure sp_deleteOrder
@OrderId int
AS
SET NOCOUNT ON
BEGIN
    IF(EXISTS
	(
     Select *
     from
     ShopDB.dbo.Orders
    )
    )
	BEGIN
	  DELETE FROM  ShopDB.dbo.Orders 
	  WHERE ShopDB.dbo.Orders.Id= @OrderId
	END
END







