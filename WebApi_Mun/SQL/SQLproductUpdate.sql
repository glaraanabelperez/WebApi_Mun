USE [MundoPanal2]
go
/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 22/10/2022 16:31:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
create PROCEDURE [dbo].[Product_Update]
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
						   CategoryId_FK= @CategoryId
						  ,[State] = @State
						  ,[Name] = @Name
						  ,[Description] = @Description
						  , MarcaId_FK= @MarcaId
						  ,DiscountId_FK =@DiscountId
						  ,[Price] = @Price
						  ,[Featured] = @Featured
				WHERE ProductId = @ProductId

		end
		else begin return -2 end
    -- Insert statements for procedure here
	   
END
