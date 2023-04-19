USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Discount_Add]    Script Date: 17/03/2023 12:08:09 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Discount_Add]
@Amount tinyint,
@State varchar (250) =null,
@CreatedBy int=null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    if((select DiscountId from Discounts where Amount=@Amount)is null)
	begin
		INSERT INTO [dbo].Discounts
           ([Amount], [State], CreatedBy
           )
     VALUES
           (	  
           @Amount, 1, 1
		   )

		   RETURN 1;


	end 
	 RETURN 0;
	
END
GO


