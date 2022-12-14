USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 22/10/2022 18:54:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
ALTER PROCEDURE [dbo].[Product_Update]
	-- Add the parameters for the stored procedure here
	@ProductId int,
	@CategoryId int, 
	@Name varchar (70),
	@Description varchar (70),
	@MarcaId int,
	@DiscountId int,
	@Price money,
	@Featured tinyint,
	@State tinyint
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		if((select ProductId from Products where ProductId=@ProductId) is not null)
		begin
			UPDATE Products SET 
								 CategoryId_FK=IsNull(@CategoryId, CategoryId_FK),
					 [State]=IsNull(@State, [State]),
					 [Name]=IsNull(@Name, [Name]),
					 [Description]=IsNull(@Description, [Description]),
					 MarcaId_FK=IsNull(@MarcaId, MarcaId_FK),
					 DiscountId_FK=IsNull(@DiscountId, DiscountId_FK),
					 [Price]=IsNull(@Price, [Price]),
					 [Featured]=IsNull(@Featured, [Featured])

				WHERE ProductId = @ProductId

		end
		else begin return -2 end
    -- Insert statements for procedure here
	   
END
