USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Image_Delete]    Script Date: 17/03/2023 12:10:56 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Image_Delete]
@ImageId int, @ProductImageId int
AS
BEGIN

	if( (select ProductImageId from ProductImage where ProductImageId=@ProductImageId) is not null )
	begin
		delete  from  ProductImage where ProductImageId=@ProductImageId
		delete from Images where ImageId=@ImageId
		return 1
	end else begin return -1  end 
	
END
GO


