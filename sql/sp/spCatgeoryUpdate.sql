USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 17/03/2023 12:07:38 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Category_Update]
	-- Add the parameters for the stored procedure here
	  @CategoryId int
      ,@Name varchar (150)   	
AS
BEGIN


    if((select CategoryId from Categories where CategoryId=@CategoryId) is not null)
	begin
	select @Name
	   UPDATE Categories SET 
					    [Name]=@Name
			WHERE  CategoryId=@CategoryId
		RETURN 1;
	end
	else begin return -1 end
END
GO


