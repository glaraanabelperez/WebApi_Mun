USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[DiscountGet]    Script Date: 22/10/2022 19:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].CategoryGet
@CategoryId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Categories where CategoryId=@CategoryId
END
