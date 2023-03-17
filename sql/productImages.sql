USE [MundoPanal2]
GO

/****** Object:  Table [dbo].[ProductImage]    Script Date: 17/03/2023 12:05:15 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductImage](
	[ProductImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageId_FK] [int] NOT NULL,
	[ProductId_FK] [int] NOT NULL,
 CONSTRAINT [PK_ProductImage] PRIMARY KEY CLUSTERED 
(
	[ProductImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_ProductImage_Image] FOREIGN KEY([ImageId_FK])
REFERENCES [dbo].[Images] ([ImageId])
GO

ALTER TABLE [dbo].[ProductImage] CHECK CONSTRAINT [FK_ProductImage_Image]
GO

ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_ProductImage_Produc] FOREIGN KEY([ProductId_FK])
REFERENCES [dbo].[Products] ([ProductId])
GO

ALTER TABLE [dbo].[ProductImage] CHECK CONSTRAINT [FK_ProductImage_Produc]
GO


