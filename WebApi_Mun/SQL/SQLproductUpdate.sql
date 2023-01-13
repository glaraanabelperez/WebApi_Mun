USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 13/01/2023 12:19:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
ALTER PROCEDURE [dbo].[Product_Update]
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
					 DiscountId_FK=IsNull(@DiscountId, DiscountId_FK),
					 [Price]=IsNull(@Price, [Price]),
					 [Featured]=IsNull(@Featured, [Featured])

				WHERE ProductId = @ProductId


			Update  [dbo].CategoryMarcas SET
						CategoryId=IsNull(@CategoryId, CategoryId),
						MarcaId=IsNull(@MarcaId, MarcaId)
						WHERE MarcaId = @MarcaId and CategoryId=@CategoryId


			select * from products where ProductId= @ProductId;
		end
END
