CREATE TABLE [dbo].[AspNetUserLogins] (
   [LoginProvider] [nvarchar](128) NOT NULL,
   [ProviderKey] [nvarchar](128) NOT NULL,
   [UserId] [nvarchar](128) NOT NULL

   ,CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId])
)
GO
ALTER TABLE [dbo].[AspNetUserLogins] WITH CHECK ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
   FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
   ON DELETE CASCADE
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] ([UserId])