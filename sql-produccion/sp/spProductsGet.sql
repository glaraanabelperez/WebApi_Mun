USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_Get]    Script Date: 17/03/2023 12:14:45 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Product_Get]
@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ProductId, [Name], [Description], CategoryId_FK as CategoryId, MarcaId_FK as MarcaId, DiscountId_FK
	as DiscountId, ROUND(Price, 2, 1) as Price, [State],Stock, Featured from Products where ProductId=@ProductId 
END
GO


