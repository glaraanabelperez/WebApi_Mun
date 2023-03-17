USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Image_GetByProduct]    Script Date: 17/03/2023 12:11:13 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Image_GetByProduct]
@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ProductImageId, ProductId_FK, ImageId_FK, I.[Name] from  ProductImage
	inner join Images I on ImageId=ImageId_FK
	where ProductId_FK=@ProductId
END
GO


