CREATE PROCEDURE [dbo].[UpdateTransaction]
		@TransactionID int,
		@Comment nvarchar(100)
As
Begin
	Update [Transaction] Set Comment = @Comment where TransactionID = @TransactionID;
End
