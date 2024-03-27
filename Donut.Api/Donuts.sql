
USE Donuts

DROP TABLE IF EXISTS OrderItem;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS Customer;


CREATE TABLE Customer (
 CustomerId int auto_increment primary key,
 CustomerName varchar(150) NOT NULL
);

CREATE TABLE Product (
 ProductId int auto_increment primary key,
 ProductName" varchar(150) NOT NULL
);

CREATE TABLE Orders (
 OrderId int auto_increment primary key,
 CustomerId int NOT NULL,
 CONSTRAINT FK_Orders_CustomerId foreign key (CustomerId) references Customer (CustomerId)
);

CREATE TABLE OrderItems (
 OrderItemId int auto_increment primary key,
 OrderId int NOT NULL,
 ProductId int NOT NULL,
 Quantity int NOT NULL,
 CONSTRAINT FK_OrderItems_OrderId foreign key (OrderId) references Orders (OrderId),
 CONSTRAINT FK_OrderItems_ProductId foreign key (ProductId) references Product (ProductId)
);


