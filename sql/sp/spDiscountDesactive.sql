USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Discount_Desactive]    Script Date: 17/03/2023 12:09:07 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Discount_Desactive]
	-- Add the parameters for the stored procedure here
	  @DiscountId int
      
AS
BEGIN


    if((select DiscountId from Discounts where DiscountId=@DiscountId) is not null
	and (select DiscountId_FK from Products where DiscountId_FK=@DiscountId) is  null)
	begin
	
			UPDATE Discounts 
			SET  [State]=0
			WHERE  DiscountId=@DiscountId

	end
	
END
GO


