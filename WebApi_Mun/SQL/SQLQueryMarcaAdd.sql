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
create PROCEDURE [dbo].[Marca_Add]
	-- Add the parameters for the stored procedure here
      @Name varchar (150)
    
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].Marcas
           ([Name]
           )
     VALUES
           (	  
           @Name
		   )

		   RETURN 0;

END
