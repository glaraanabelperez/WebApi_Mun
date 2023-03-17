USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[User_Add]    Script Date: 17/03/2023 12:15:34 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[User_Add]
	-- Add the parameters for the stored procedure here
      @UserName varchar (150), @Password  varchar (150)
    
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].Users
           (UserName, [Password]
           )
     VALUES
           (	  
           @UserName, @Password
		   )


END
GO


