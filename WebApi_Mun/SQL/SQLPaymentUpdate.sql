USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[dbo.Update_Payments]    Script Date: 11/01/2023 11:19:04 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[dbo.Update_Payments]
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
