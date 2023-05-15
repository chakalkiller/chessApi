CREATE TABLE [dbo].[Tournament]
(
	[Tournament_Id] INT NOT NULL Identity ,
	[Tournament_Name] NVARCHAR(50) NOT NULL,
	[Description]  NVARCHAR(2000) NULL,
	[Max_Player] int NOT NULL, 
    [Tournament_State] BIT NOT NULL DEFAULT 0,
	[UserID_creator] INT NOT NULL,

	CONSTRAINT PK_Tournament PRIMARY KEY ([Tournament_Id])
);
