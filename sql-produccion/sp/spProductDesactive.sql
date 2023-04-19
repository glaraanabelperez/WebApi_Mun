USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_Desactive]    Script Date: 17/03/2023 12:13:58 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Product_Desactive]
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
	  WHERE  ProductImage.ProductId_FK=@ProductId
	OPEN images_cursor
	FETCH NEXT FROM images_cursor INTO @imageId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		delete from ProductImage  where ProductId_FK=@ProductId
		delete from Images where ImageId=@imageId
    FETCH NEXT FROM images_cursor INTO @imageId
	END
	CLOSE images_cursor
	DEALLOCATE images_cursor

	if ((select Products.ProductId from Products where CategoryId_FK=@CategoryId and  MarcaId_FK=@MarcaId)is null)
		begin
			begin
			   UPDATE [dbo].[CategoryMarcas]
				SET [State] =0
				WHERE MarcaId=@MarcaId and CategoryId=@CategoryId;
		end
		end

	RETURN 1;

END
GO


