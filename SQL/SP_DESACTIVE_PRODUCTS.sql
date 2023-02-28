USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Product_Desactive]    Script Date: 25/02/2023 01:54:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Product_Desactive]
	-- Add the parameters for the stored procedure here
	  @ProductId int
      
AS
BEGIN

declare @CategoryId int, @MarcaId int;
select @CategoryId=CategoryId_FK, @MarcaId=MarcaId_FK from Products where ProductId=@ProductId;
	
	UPDATE Products SET 
			[State]=0
			WHERE  ProductId=@ProductId;


	DECLARE @imageId AS int
	DECLARE images_cursor CURSOR FOR
	SELECT ImageId_FK
	  FROM ProductImage 
	  WHERE  ProductImage.ProductId_FK=7
	OPEN ImageId_FK
	FETCH NEXT FROM images_cursor INTO @imageId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		delete from ProductImage  where ProductId_FK=@ProductId
		delete from Images where ImageId=@imageId
    FETCH NEXT FROM images_cursor INTO @imageId
	END
	CLOSE images_cursor
	DEALLOCATE images_cursor


	RETURN 1;

END
