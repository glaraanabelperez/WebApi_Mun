USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_GetWithImages]    Script Date: 17/03/2023 12:15:04 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Product_GetWithImages]
@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     -- Insert statements for procedure here
	SELECT p.ProductId, p.[Name], p.[Description], c.Name as CategoryName, m.Name as MarcaName, d.Amount as DiscountAmount
	, ROUND(p.Price, 2, 1) as Price, p.[State],p.Stock, 
	(
		select '-' + [name]
		from [MundoPanal2].[dbo].[Images]  
		inner join [MundoPanal2].[dbo].[ProductImage]  pim on pim.ProductId_FK=@ProductId
		where images.ImageId = pim.ImageId_FK
		FOR XML PATH ('')
	)
	from [MundoPanal2].[dbo].[Products] p
	left join [MundoPanal2].[dbo].[Discounts] d on d.DiscountId=p.DiscountId_FK
	inner join [MundoPanal2].[dbo].[Marcas]  m on m.MarcaId=p.MarcaId_FK
	inner join [MundoPanal2].[dbo].[Categories]  c on c.CategoryId=p.CategoryId_FK
	where ProductId=@ProductId



END
GO


