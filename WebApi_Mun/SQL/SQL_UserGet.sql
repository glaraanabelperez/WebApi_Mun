USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 22/10/2022 16:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[User_Get]
	-- Add the parameters for the stored procedure here
	  @UserId int 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Users where UserId=@UserId

	RETURN 0;

END
