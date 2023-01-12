USE [MundoPanal2]
GO
/****** Object:  StoredProcedure [dbo].[Payments_ADD]    Script Date: 11/01/2023 11:18:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Payments_ADD]
	-- Add the parameters for the stored procedure here
	@PaymentId int,
        @BuyerName varchar(250),
		@BuyerLastName varchar (250),
		@Direction varchar (250),
		@Phone varchar (250) ,
		@Email varchar (250),
		@Date dateTime2,
		@TotalAmount money ,
		@MethodPayment varchar(250),
		@PaymentState varchar(250),
		@Delivered tinyint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

    -- Insert statements for procedure here
	INSERT INTO [dbo].Payments
           (
		   PaymentId,
		    BuyerName,
	        BuyerLastName,
	        Direction ,
	        Phone,
	        Email,
	        [Date],
            TotalAmount ,
	        MethodPayment ,
	        PaymentState,
	        Delivered
           )
     VALUES
           (	 
		   @PaymentId,
            @BuyerName,
		@BuyerLastName,
		@Direction ,
		@Phone,
		@Email,
		@Date,
		@TotalAmount,
		@MethodPayment,
		@PaymentState ,
		@Delivered 
		   )

	select SCOPE_IDENTITY();

END

