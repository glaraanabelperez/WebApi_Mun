USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Payments_ByDate]    Script Date: 11/01/2023 11:19:02 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Payments_ByDate]
	-- Add the parameters for the stored procedure here
		@Date dateTime2
AS
BEGIN

	Select * from Payments where [Date]=@Date

END
