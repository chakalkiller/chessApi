CREATE TABLE [dbo].[Match]
(
	[Match_Id] INT NOT NULL IDENTITY,
	[Tournament_Id] INT NOT NULL,
    [Round] INT NOT NULL DEFAULT 1, 
	[White] INT NOT NULL,
	[Black] INT NOT NULL,
	[Match_Status] VARCHAR(50) NOT NULL DEFAULT 'Announced',
    CONSTRAINT PK_Match PRIMARY KEY ([Match_Id]),
	CONSTRAINT FK_Match__Tournament_Id FOREIGN KEY ([Tournament_Id]) REFERENCES [Tournament] ([Tournament_Id]),
    CONSTRAINT FK_Match__White FOREIGN KEY ([White]) REFERENCES [PlayerApp] ([Player_Id]),
	CONSTRAINT FK_Match__Black FOREIGN KEY ([Black]) REFERENCES [PlayerApp] ([Player_Id])
)
