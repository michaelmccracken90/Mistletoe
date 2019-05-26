CREATE TABLE [dbo].[Campaign]
(
	[CampaignID] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [UserID] NVARCHAR (50) NOT NULL,
    [CampaignName] NVARCHAR(255) NOT NULL,
	[StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
	[Frequency] NVARCHAR(100) NOT NULL,
	[DataFilePath] NVARCHAR(255) NULL,
    [IsActive] BIT NOT NULL, 
    [IsArchived] BIT NOT NULL, 
    CONSTRAINT [FK_Campaign_User] FOREIGN KEY ([UserID]) REFERENCES  [User]([UserID])
)
