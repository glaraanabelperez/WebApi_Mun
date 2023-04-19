USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Product_Featured]    Script Date: 17/03/2023 12:14:12 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Product_Featured]

AS
BEGIN

    -- Insert statements for procedure here
	SELECT p.ProductId, p.[Name], p.[Description], p.Price, d.Amount as DiscountAmount, 
	m.[Name] as MarcaName, c.[Name] as CatgoryName,  ( 
														SELECT  TOP 1  ii.[Name] 
														 FROM    ProductImage pim
														 inner join  images ii on ImageId=pim.ImageId_FK
														 WHERE   pim.ProductId_FK= p.ProductId 
														) as ImageName
	from Products p
	inner join Marcas m on m.MarcaId=p.MarcaId_FK
	LEFT join Discounts d on d.DiscountId=p.DiscountId_FK
	inner join Categories c on c.CategoryId=p.CategoryId_FK
	where p.Featured=1 and p.[State]=1 and Stock>0
END
GO


