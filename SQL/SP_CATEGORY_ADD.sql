USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Category_Add]    Script Date: 15/02/2023 04:55:03 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[Category_Add]
	-- Add the parameters for the stored procedure here
      @Name varchar (150)
    
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    -- Insert statements for procedure here
	INSERT INTO [dbo].Categories
           ([Name], [State]
           )
     VALUES
           (	  
           @Name, 1
		   )

		   RETURN 1;

END
GO

