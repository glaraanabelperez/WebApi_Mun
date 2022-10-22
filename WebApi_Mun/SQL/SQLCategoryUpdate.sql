USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 22/10/2022 16:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[Category_Update]
	-- Add the parameters for the stored procedure here
	  @CategoryId int
      ,@Name varchar (150),
	  @State tinyint
   	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   UPDATE Categories SET 
					   [Name] = @Name,
					   [State]= @State 
			WHERE  CategoryId=@CategoryId
RETURN 0;
END
