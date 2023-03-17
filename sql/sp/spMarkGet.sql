USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[Marca_Get]    Script Date: 17/03/2023 12:12:41 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER PROCEDURE [dbo].[Marca_Get]
@MarcaId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Marcas where MarcaId=@MarcaId
END
GO


