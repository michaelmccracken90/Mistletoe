CREATE TABLE [dbo].[User]
(
	[UserID]		NVARCHAR (50) NOT NULL PRIMARY KEY,
	[UserName]		NVARCHAR (15) NOT NULL,
	[Email]		NVARCHAR (255) NOT NULL,
	[IsActive]		BIT NOT NULL,
	[IsAdmin]		BIT NOT NULL
)
