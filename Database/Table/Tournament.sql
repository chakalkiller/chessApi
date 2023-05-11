CREATE TABLE [dbo].[Tournament]
(
	[Tournament_Id] INT NOT NULL ,
	[Tournament_Name] NVARCHAR(50) NOT NULL,
	[Description]  NVARCHAR(2000) NULL,
	[Max_player] int NOT NULL, 
    [Tournament_State] BIT NOT NULL DEFAULT 0,

	CONSTRAINT PK_Tournament PRIMARY KEY ([Tournament_Id])
);
