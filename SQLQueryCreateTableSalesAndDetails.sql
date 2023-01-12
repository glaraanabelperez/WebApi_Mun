
CREATE TABLE Sales (
    SalesId int NOT NULL,
    BuyerName varchar (250)  NOT NULL,
	BuyerLastName varchar (250)  NOT NULL,
	Direction varchar (250)  NOT NULL,
	Phone varchar (250)  NOT NULL,
	Email varchar (250)  NOT NULL,
	[Date] dateTime2 not null,
    TotalAmount money not null,
	MethodPayment varchar(250) not null,
	[PaymentState] varchar (250) not null,
	Delivered tinyint not null,
    PRIMARY KEY (SalesId),
);

CREATE TABLE Details (
    SalesId int NOT NULL,
	ProductId int not null,
	quantity int not null,
	Amount money not null,
	FOREIGN KEY (SalesId) REFERENCES Sales(SalesId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

   


