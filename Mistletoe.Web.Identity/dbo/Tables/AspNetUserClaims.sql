CREATE TABLE [dbo].[AspNetUserClaims] (
   [Id] [int] NOT NULL
      IDENTITY (1,1),
   [UserId] [nvarchar](128) NOT NULL,
   [ClaimType] [nvarchar](max) NULL,
   [ClaimValue] [nvarchar](max) NULL

   ,CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id])
)
GO
ALTER TABLE [dbo].[AspNetUserClaims] WITH CHECK ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
   FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
   ON DELETE CASCADE
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims] ([UserId])