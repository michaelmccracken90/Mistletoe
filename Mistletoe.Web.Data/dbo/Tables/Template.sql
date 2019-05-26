CREATE TABLE [dbo].[Template] (
    [TemplateID] INT           IDENTITY (1, 1) NOT NULL,
	[CampaignID] INT NOT NULL, 
	[TemplateName] NVARCHAR(255) NOT NULL,
	[Subject]         NVARCHAR (255)           NOT NULL,
    [Body]            TEXT					   NOT NULL,
    PRIMARY KEY CLUSTERED ([TemplateID] ASC), 
    CONSTRAINT [FK_Template_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [Campaign]([CampaignID])
);