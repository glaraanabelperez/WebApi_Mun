use  [MundoPanal2]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE DiscountUpdate
@DiscountId int,
@Amount int=null,
@State varchar (250) =null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Discounts set 
		@Amount=IsNull(@Amount, Amount),
		 @State=IsNull(@State, [State])		 
		 where DiscountId=@DiscountId
END
GO
