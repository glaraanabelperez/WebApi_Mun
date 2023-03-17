USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Discount_Update]    Script Date: 17/03/2023 12:10:27 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Discount_Update]
@DiscountId int,
@Amount int=null,
@State varchar (250) =null,
@CreatedBy int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    if((select DiscountId from Discounts where DiscountId=@DiscountId)is not null)
	begin
		Update Discounts set 
		Amount=IsNull(@Amount, Amount),
		 [State]=IsNull(@State, [State]),
		 CreatedBy=@CreatedBy
		 where DiscountId=@DiscountId

	end 

	
END
GO


