USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Product_Delete]    Script Date: 22/10/2022 16:31:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Product_Disable]
	-- Add the parameters for the stored procedure here
	@ProductId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   update Products set State=0 where ProductId=@ProductId  
RETURN 0;

END
