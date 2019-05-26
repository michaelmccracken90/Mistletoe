CREATE TABLE [dbo].[Licensing]
(
	[Licensing_ID] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
	[User_ID] NVARCHAR (50) NOT NULL,
    [Is_Active] BIT NOT NULL, 
    CONSTRAINT [FK_Licensing_User] FOREIGN KEY ([User_ID]) REFERENCES [User]([User_ID])
)
