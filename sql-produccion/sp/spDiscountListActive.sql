USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Discount_List_Active]    Script Date: 17/03/2023 12:09:53 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Discount_List_Active]

AS
BEGIN


    -- Insert statements for procedure here
	SELECT * from Discounts where [State]=1
END
GO


