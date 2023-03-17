USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Marca_Desactive]    Script Date: 17/03/2023 12:12:24 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Marca_Desactive]
	-- Add the parameters for the stored procedure here
	  @MarcaId int
      
AS
BEGIN


    if((select MarcaId from Marcas where MarcaId=@MarcaId) is not null
	and (select MarcaId_FK from Products where MarcaId_FK=@MarcaId) is  null)
	begin
	
	   UPDATE Marcas 
	   SET [State]=0
	   WHERE  MarcaId=@MarcaId
	   if((select MarcaId from CategoryMarcas where MarcaId=@MarcaId) is not null)
		begin
			   UPDATE [dbo].[CategoryMarcas]
				SET [State] =0
				WHERE MarcaId=@MarcaId;
		end
		RETURN 1;
	end
	else begin return -1 end
END
GO


