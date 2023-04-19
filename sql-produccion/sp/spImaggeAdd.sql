USE paalerac_colores
GO

/****** Object:  StoredProcedure [dbo].[Image_Add]    Script Date: 17/03/2023 12:10:45 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE paalerac_colores.dbo.[dbo].[Image_Add]
@Name varchar(250),
@ProductId int
AS
BEGIN

	insert into paalerac_colores.dbo.Images ([Name]) values (@Name)
	insert into paalerac_colores.dbo.ProductImage (ProductId_FK, ImageId_FK) values (@ProductId,  SCOPE_IDENTITY())

END

GO


