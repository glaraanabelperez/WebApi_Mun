USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Product_Add]    Script Date: 22/10/2022 16:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Product_Add]
	-- Add the parameters for the stored procedure here
	@CategoryId int, 
	@Name varchar (70),
	@Description varchar (70),
	@MarcaId int,
	@DiscountId int,
	@Price money,
	@Featured tinyint,
	@State tinyint,
	@UserId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Products]
           (
		      CategoryId_FK
						  ,[Name] 
						  ,[Description] 
						  , MarcaId_FK
						  ,DiscountId_FK 
						  ,[Price] 
						  ,[Featured] 
						  ,[State]
						  ,CreatedBy_Fk
						  )
     VALUES
           (
		      @CategoryId
				,@State
				,@Name
				,@Description
				,@MarcaId
				,@DiscountId
				,@Price
				,@Featured
				,@UserId
		   )

END
RETURN 1

