USE [MundoPanal2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Marcas_Update]
	-- Add the parameters for the stored procedure here
	  @MarcaId int
      ,@Name varchar (150),
	  @State tinyint
   	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   UPDATE [dbo].[Marcas] SET 
					   [Name] = @Name,
					   [State]= @State 
			WHERE  MarcaId=@MarcaId
RETURN 0;
END
