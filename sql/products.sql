USE [MundoPanal2]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 17/03/2023 12:05:27 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[CategoryId_FK] [int] NOT NULL,
	[MarcaId_FK] [int] NOT NULL,
	[Price] [decimal](6, 2) NULL,
	[DiscountId_FK] [int] NULL,
	[State] [tinyint] NOT NULL,
	[Featured] [tinyint] NOT NULL,
	[CreatedBy_Fk] [int] NOT NULL,
	[Stock] [tinyint] NULL,
	[PriceWithDiscount] [decimal](6, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] ADD  DEFAULT ((1)) FOR [State]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId_FK])
REFERENCES [dbo].[Categories] ([CategoryId])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Product_Category]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Product_Discount] FOREIGN KEY([DiscountId_FK])
REFERENCES [dbo].[Discounts] ([DiscountId])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Product_Discount]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Product_Marca] FOREIGN KEY([MarcaId_FK])
REFERENCES [dbo].[Marcas] ([MarcaId])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Product_Marca]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Product_User] FOREIGN KEY([CreatedBy_Fk])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Product_User]
GO


