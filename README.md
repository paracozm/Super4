Create database Super3_Db



create table Customer
(
    Id int NOT NULL PRIMARY KEY IDENTITY,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
    Document varchar(14) NOT NULL,
    Street varchar(255),
    AddressNumber varchar(255) NOT NULL,
	Neighborhood varchar(255),
	City varchar(255) NOT NULL,
	Province varchar(255) NOT NULL,
	CEP varchar(8) NOT NULL
)

create table [Order]
(
    Id varchar(255) NOT NULL PRIMARY KEY,
    CustomerId int,
    OrderNumber varchar(255) NOT NULL,
    OrderDate datetime NOT NULL,
    TotalPrice decimal(8,2), 
    CONSTRAINT FK_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
)

create table Product
(
	Id varchar(255) NOT NULL PRIMARY KEY,
	ProductName varchar(255) NOT NULL,
)

create table OrderItem
(

    OrderId varchar(255),
    ProductId varchar(255),
    ProductPrice decimal(8 ,2) NOT NULL,
    TotalAmount int,
    CONSTRAINT FK_OrderId FOREIGN KEY (OrderId) REFERENCES [Order](Id),
    CONSTRAINT FK_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)
)

create table Stock
(
	ProductId varchar(255),
	Quantity int,
    CONSTRAINT FK_StockProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)
)


