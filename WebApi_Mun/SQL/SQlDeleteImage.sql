USE [MundoPanal2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].DeleteImage
@ImageId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete  from  ProductImage
	where ImageId_FK=@ImageId

	delete from Images  
	where ImageId=@ImageId
END
