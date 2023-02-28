USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Category_Desactive]    Script Date: 25/02/2023 01:03:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Category_Desactive]
	-- Add the parameters for the stored procedure here
	  @CategoryId int
      
AS
BEGIN


    if((select CategoryId from Categories where CategoryId=@CategoryId) is not null
	and (select CategoryId_FK from Products where CategoryId_FK=@CategoryId) is  null)
	begin
	
			UPDATE Categories 
			SET  [State]=0
			WHERE  CategoryId=@CategoryId

		if((select CategoryId from CategoryMarcas where CategoryId=@CategoryId) is not null)
		begin
			UPDATE [dbo].[CategoryMarcas]
			SET [State] =0
			WHERE CategoryId=@CategoryId;
		end
	end
	
END
