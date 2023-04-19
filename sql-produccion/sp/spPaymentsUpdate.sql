USE [MundoPanal2]
GO

/****** Object:  StoredProcedure [dbo].[dbo.PaymentsUpdate]    Script Date: 17/03/2023 12:07:51 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[dbo.PaymentsUpdate]
	-- Add the parameters for the stored procedure here
		@Delivered tinyint,
		@PaymentId int
AS
BEGIN

	update [dbo].Payments 
	set Delivered = @Delivered 
	where PaymentId=@PaymentId

	RETURN 1;

END
GO


