USE paalerac_colores
GO

/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 17/03/2023 12:15:34 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[User_Get]
	-- Add the parameters for the stored procedure here
      @UserName varchar (150), @Password  varchar (150)
    
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select UserName, [Password] from [dbo].Users
          where UserName=@UserName  and [Password]=@Password
       


END
GO


