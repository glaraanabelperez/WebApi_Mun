 use master 
 
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MundoPanal2')
DROP DATABASE MundoPanal2
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Product_Category]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Product] DROP CONSTRAINT FK_Product_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Product_Discount]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Product] DROP CONSTRAINT FK_Product_Discount
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Product_Marca]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Product] DROP CONSTRAINT FK_Product_Marca
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Product_User]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Product] DROP CONSTRAINT FK_Product_User
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductImage_Produc]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].ProductImage DROP CONSTRAINT FK_ProductImage_Produc
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductImage_Image]') 
and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].ProductImage DROP CONSTRAINT FK_ProductImage_Image
GO



CREATE DATABASE MundoPanal2
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

-- IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MundoPanal2')
--alter database MundoPanal2 set single_user with rollback immediate
--GO



----------------------------------------------------Init

use MundoPanal2
GO
DECLARE @Error int
BEGIN TRAN

CREATE TABLE [dbo].[Users] (
	UserId [int] IDENTITY (1, 1) NOT NULL ,
	UserName varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Password] varchar (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
) ON [PRIMARY]
GO

CREATE TABLE [dbo].Products (
	ProductId [int] IDENTITY (1, 1) NOT NULL ,
	[Name] varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] varchar (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	CategoryId_FK [int] NOT NULL ,
	MarcaId_FK int not null,
	Price Money  NOT NULL ,
	DiscountId_FK int NULL ,
	[State] tinyint  NOT NULL ,
	Featured tinyint  NOT NULL,
	CreatedBy_Fk int not null

) ON [PRIMARY] 
GO

CREATE TABLE [dbo].Marcas (
	MarcaId [int] IDENTITY (1, 1) NOT NULL ,
	[Name] varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[State] tinyint null
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].Discounts (
	DiscountId [int] IDENTITY (1, 1) NOT NULL ,
	[Amount] decimal NOT NULL ,
	CreatedBy int not null,
	[State] tinyint null
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].Categories (
	CategoryId [int] IDENTITY (1, 1) NOT NULL ,
	[Name] varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[State] tinyint null
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].Images (
	ImageId [int] IDENTITY (1, 1) NOT NULL ,
	[Name] varchar (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].ProductImage (
	ProductImageId int IDENTITY (1, 1) NOT NULL ,
	ImageId_FK [int]  NOT NULL ,
	ProductId_FK [int]  NOT NULL ,
) ON [PRIMARY] 
GO


--PK
ALTER TABLE [dbo].Users WITH NOCHECK ADD 
	CONSTRAINT [PK_User] PRIMARY KEY   
	( UserId )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Products WITH NOCHECK ADD 
	CONSTRAINT [PK_Product] PRIMARY KEY   
	( ProductId)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Categories WITH NOCHECK ADD 
	CONSTRAINT [PK_Category] PRIMARY KEY   
	( CategoryId )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Discounts WITH NOCHECK ADD 
	CONSTRAINT [PK_Discount] PRIMARY KEY   
	( DiscountId)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Marcas WITH NOCHECK ADD 
	CONSTRAINT [PK_Marca] PRIMARY KEY   
	( MarcaId)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Images WITH NOCHECK ADD 
	CONSTRAINT [PK_Image] PRIMARY KEY   
	( ImageId)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].ProductImage WITH NOCHECK ADD 
	CONSTRAINT [PK_ProductImage] PRIMARY KEY   
	( ProductImageId)  ON [PRIMARY] 
GO



--FK
ALTER TABLE [dbo].Products ADD 
	CONSTRAINT [FK_Product_Category] FOREIGN KEY 
	(
		CategoryId_FK
	) REFERENCES [dbo].[Categories] (
		CategoryId
	)
GO

ALTER TABLE [dbo].Products ADD 
	CONSTRAINT [FK_Product_Discount] FOREIGN KEY 
	(
		DiscountId_FK
	) REFERENCES [dbo].Discounts (
		DiscountId
	)
GO

ALTER TABLE [dbo].Products ADD 
	CONSTRAINT [FK_Product_Marca] FOREIGN KEY 
	(
		MarcaId_FK
	) REFERENCES [dbo].Marcas (
		MarcaId
	)
GO

ALTER TABLE [dbo].Products ADD 
	CONSTRAINT [FK_Product_User] FOREIGN KEY 
	(
		CreatedBy_FK
	) REFERENCES [dbo].Users (
		UserId
	)
GO

ALTER TABLE [dbo].ProductImage ADD 
	CONSTRAINT [FK_ProductImage_Produc] FOREIGN KEY 
	(
		ProductId_FK

	) REFERENCES [dbo].Products (
		productId
	)
GO

ALTER TABLE [dbo].ProductImage ADD 
	CONSTRAINT [FK_ProductImage_Image] FOREIGN KEY 
	(
		ImageId_FK

	) REFERENCES [dbo].Images (
		ImageId
	)
GO



--Check
--ALTER TABLE Users
--ADD CONSTRAINT 
--   CHK_email CHECK (user_email like '%_@__%.__%')


ALTER TABLE [dbo].Products WITH NOCHECK ADD 
	CONSTRAINT Price_check DEFAULT (0) FOR Price
GO
ALTER TABLE [dbo].Categories WITH NOCHECK ADD 
	CONSTRAINT category_State DEFAULT (1) FOR [State]
GO

ALTER TABLE [dbo].MarcasWITH NOCHECK ADD 
	CONSTRAINT Marca_State DEFAULT (1) FOR [State]
GO

ALTER TABLE [dbo].Discounts WITH NOCHECK ADD 
	CONSTRAINT Discount_State DEFAULT (1) FOR [State]
GO
--SET @Error=@@ERROR

COMMIT TRAN

If @@Error<>0 
	BEGIN
	PRINT 'Ha ecorrido un error. Abortamos la transacción'
	--Se lo comunicamos al usuario y deshacemos la transacción
	--todo volverá a estar como si nada hubiera ocurrido
	ROLLBACK TRAN
	END
