USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 17/03/2023 12:15:18 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[Product_Update]
	-- Add the parameters for the stored procedure here
	@ProductId int,
	@CategoryId int=null, 
	@Name varchar (70)=null,
	@Description varchar (70)=null,
	@MarcaId int=null,
	@DiscountId int=null,
	@Price money=null,
	@Featured tinyint=null,
	@State tinyint=null,
	@Stock tinyint=null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
		if((select ProductId from Products where ProductId=@ProductId) is not null)
		begin
			UPDATE Products SET 
								 CategoryId_FK=IsNull(@CategoryId, CategoryId_FK),
					 [State]=IsNull(@State, [State]),
					 [Stock]=IsNull(@Stock, [Stock]),
					 [Name]=IsNull(@Name, [Name]),
					 [Description]=IsNull(@Description, [Description]),
					 MarcaId_FK=IsNull(@MarcaId, MarcaId_FK),
					 DiscountId_FK=IsNull(@DiscountId, null),
					 [Price]=IsNull(@Price, [Price]),
					 [Featured]=IsNull(@Featured, [Featured]),
					 [PriceWithDiscount] = @Price - (@Price * (select [Amount] from [dbo].[Discounts] where DiscountId=IsNull(@DiscountId, 0))/100)

				WHERE ProductId = @ProductId

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
				
		end 
END
GO


