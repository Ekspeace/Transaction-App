If not exists (Select 1 From Client)
Begin
		Insert Into Client([Name], Surname, ClientBalance) 
		Values('Peter', 'Parker', 1000.00),
					('Tory', 'Stark', 800000.00),
					('Bruce', 'Banner', 254111.00),
					('John', 'Doe', 100.00)
End

If not exists (Select 1 From [Transaction])
Begin
		Insert Into [Transaction](Amount, TransactionTypeID, ClientID, Comment) 
		Values(1000.00, 1, 1, 'Winnings'),
					(-500.00, 2, 3, 'losing'),
					(-9000.00, 2, 2, 'losing'),
					(10.00, 1, 4, 'Winnings')
End

If not exists (Select 1 From TransactionType)
Begin
		Insert Into TransactionType(TransactionTypeName) 
		Values( 'Debit'),( 'Credit')
End