CREATE TABLE [dbo].[Template_Email_Addresses]
(
	[ID] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[TemplateID] INT NOT NULL, 
	[EmailID] INT NOT NULL,
	[IsSender] BIT NOT NULL,
	CONSTRAINT [FK_Template_Email_Addresses_Template] FOREIGN KEY ([TemplateID]) REFERENCES [Template]([TemplateID]),
	CONSTRAINT [FK_Template_Email_Addresses_Email_Address] FOREIGN KEY ([EmailID]) REFERENCES [Email_Address]([EmailID])
)
