USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Discount_Get]    Script Date: 17/03/2023 12:09:22 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[Discount_Get]
@DiscountId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Discounts where DiscountId=@DiscountId
END
GO


