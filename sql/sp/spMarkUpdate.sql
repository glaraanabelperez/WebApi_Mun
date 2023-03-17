USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Marca_Update]    Script Date: 17/03/2023 12:13:24 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[Marca_Update]
	-- Add the parameters for the stored procedure here
	  @MarcaId int
      ,@Name varchar (150)   	
AS
BEGIN


    -- Insert statements for procedure here
	if((select MarcaId from Marcas where MarcaId=@MarcaId) is not null)
	begin
		UPDATE [dbo].[Marcas] SET 
					 [Name]=IsNull(@Name, [Name])
			WHERE  MarcaId=@MarcaId

			return 3
	end
	

END
GO


