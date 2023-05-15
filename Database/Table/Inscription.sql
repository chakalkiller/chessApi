CREATE TABLE [dbo].[Inscription]
(
	[Inscription_Id] INT NOT NULL PRIMARY KEY, 	
	[Player_Id] INT NOT NULL,
	[Tournament_Id] INT NOT NULL,

	
	constraint FK_Inscription_PlayerApp foreign key ([Player_Id]) references PlayerApp ([Player_Id]),
	constraint FK_Inscription_tournament foreign key ([Tournament_Id]) references Tournament ([Tournament_Id])

	)
