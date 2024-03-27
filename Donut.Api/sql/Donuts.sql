
USE Donuts

if object_id('dbo.OrderItems', 'U') is not null begin DROP TABLE OrderItems; end
if object_id('dbo.Orders', 'U') is not null begin DROP TABLE IF EXISTS Orders; end
if object_id('dbo.Product', 'U') is not null begin DROP TABLE IF EXISTS Product; end
if object_id('dbo.Customer', 'U') is not null begin DROP TABLE IF EXISTS Customer; end


CREATE TABLE Customer (
 CustomerId int IDENTITY(1,1) PRIMARY KEY,
 CustomerName varchar(150) NOT NULL
);

INSERT INTO Customer (CustomerName) VALUES ('Kevin');
INSERT INTO Customer (CustomerName) VALUES ('Bill');


CREATE TABLE Product (
 ProductId int IDENTITY(1,1) PRIMARY KEY,
 ProductName varchar(150) NOT NULL,
 Price decimal(10,2) NOT NULL
);

INSERT INTO Product (ProductName, Price) VALUES ('Chocolate Donut', 2);
INSERT INTO Product (ProductName, Price) VALUES ('Glazed Donut', 2);

CREATE TABLE Orders (
 OrderId int IDENTITY(1,1) PRIMARY KEY,
 CustomerId int NOT NULL,
 Total decimal(10,2) NOT NULL,
 OrderStatus varchar(50) NOT NULL,
 CONSTRAINT CHK_OrderStatus CHECK (OrderStatus in ('Pending', 'Completed', 'Cancelled')),
 CONSTRAINT FK_Orders_CustomerId foreign key (CustomerId) references Customer (CustomerId)
);

CREATE TABLE OrderItems (
 OrderItemId int IDENTITY(1,1) PRIMARY KEY,
 OrderId int NOT NULL,
 ProductId int NOT NULL,
 Quantity int NOT NULL,
 Price decimal(10,2) NOT NULL,
 Total decimal(10,2) NOT NULL,
 CONSTRAINT FK_OrderItems_OrderId foreign key (OrderId) references Orders (OrderId),
 CONSTRAINT FK_OrderItems_ProductId foreign key (ProductId) references Product (ProductId)
);





