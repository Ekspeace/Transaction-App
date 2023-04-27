CREATE PROCEDURE [dbo].[InsertTransaction]
		@Amount decimal(18,2),
		@TransactionTypeID smallint,
		@ClientID int,
		@Comment nvarchar(100)
As
Begin
	Begin Transaction
	
	if @TransactionTypeID = 2
		Begin
				Update Client Set ClientBalance = ClientBalance - @Amount where ClientID = @ClientID
				set @Amount = -@Amount
		 End
	 Else
	     Begin
				Update Client Set ClientBalance = ClientBalance + @Amount where ClientID = @ClientID
		 End
	Insert Into [Transaction](Amount, TransactionTypeID, ClientID, Comment) Values(@Amount, @TransactionTypeID, @ClientID, @Comment)
	If (Select ClientBalance from  Client where ClientID = @ClientID) >= 0
		 Begin
				Commit
		 End
	 Else
	     Begin
				Rollback
		 End
End
