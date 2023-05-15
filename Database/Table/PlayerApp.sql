CREATE TABLE [dbo].[PlayerApp]
(
	[Player_Id] INT IDENTITY,
	[Pseudo] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(200) NOT NULL,
	[Birthdate] DATE NULL,
	[Password_Hash] VARCHAR(100) NOT NULL, 

	CONSTRAINT PK_PlayerApp PRIMARY KEY ([Player_Id]),
	CONSTRAINT UK_PlayerApp__Email UNIQUE([Email]),
	
);
