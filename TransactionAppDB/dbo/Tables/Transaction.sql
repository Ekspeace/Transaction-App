CREATE TABLE [dbo].[Transaction]
(
	[TransactionID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Amount] DECIMAL(18, 2) NOT NULL, 
    [TransactionTypeID] SMALLINT NOT NULL, 
    [ClientID] INT NOT NULL, 
    [Comment] NVARCHAR(100) NULL
)
