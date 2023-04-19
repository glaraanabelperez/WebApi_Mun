USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Marca_List_Active]    Script Date: 17/03/2023 12:13:10 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Marca_List_Active]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Marcas where [State]=1
END
GO


