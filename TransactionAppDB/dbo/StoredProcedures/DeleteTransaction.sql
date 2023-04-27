CREATE PROCEDURE [dbo].[DeleteTransaction]
@TransactionID int
As
Begin
	Delete [Transaction] where TransactionID = @TransactionID;
End