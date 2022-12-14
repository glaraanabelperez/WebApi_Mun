USE [MundoPanal2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[GetImageByProduct]
@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from  ProductImage
	inner join Images on ImageId=ImageId_FK
	where ProductId_FK=@ProductId
END
