USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Category_Delete]    Script Date: 22/10/2022 16:31:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Category_Delete]
	-- Add the parameters for the stored procedure here
	@CategoryId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   Delete from Categories WHERE  CategoryId= @CategoryId
RETURN 0;

END

