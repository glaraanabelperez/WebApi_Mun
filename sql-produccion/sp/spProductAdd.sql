USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_Add]    Script Date: 17/03/2023 12:13:40 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[Product_Add]
	-- Add the parameters for the stored procedure here
	@CategoryId int, 
	@Name varchar (70),
	@Description varchar (70),
	@MarcaId int,
	@DiscountId int=null,
	@Price money,
	@Featured tinyint=null,
	@State tinyint=null,
	@Stock tinyint,
	@UserId int=null
	
AS
BEGIN
	--SET NOCOUNT ON 
	-- interfering with SELECT statements.

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
			  ,Stock
			  ,CreatedBy_Fk
			  ,PriceWithDiscount
			  )
     VALUES
           (
		      @CategoryId
				,@Name
				,@Description
				,@MarcaId
				, IsNull(@DiscountId, null)
				,@Price
				, IsNull(@Featured, 1)
				,IsNull(@State, 1)
				,IsNull(@Stock, 1)
				,IsNull(@UserId, 1)
                , (case when @DiscountId is null then 0 
                else (@Price - (@Price * (select [Amount] from [dbo].[Discounts] where DiscountId=IsNull(@DiscountId, 0))/100)) end)
		   )

     if((select CategoryId from CategoryMarcas where CategoryId=@CategoryId and MarcaId= @MarcaId) is null)
			begin
				INSERT INTO [dbo].[CategoryMarcas]
				   ([CategoryId]
				   ,[MarcaId]
				   ,[State])
					VALUES
				   (@CategoryId
				   ,@MarcaId
				   ,1)
			end
		  -- select * from products where ProductId= @@identity;
END

GO


