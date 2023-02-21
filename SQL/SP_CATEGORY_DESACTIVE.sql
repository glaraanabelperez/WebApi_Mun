USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Category_Desactive]    Script Date: 15/02/2023 04:56:04 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Category_Desactive]
	-- Add the parameters for the stored procedure here
	  @CategoryId int
      
AS
BEGIN


    if((select CategoryId from Categories where CategoryId=@CategoryId) is not null)
	begin
	
	   UPDATE Categories SET 
					    [State]=0
			WHERE  CategoryId=@CategoryId
		RETURN 1;
	end
	else begin return -1 end
END
GO

