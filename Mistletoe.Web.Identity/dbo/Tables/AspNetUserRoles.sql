CREATE TABLE [dbo].[AspNetUserRoles] (
   [UserId] [nvarchar](128) NOT NULL,
   [RoleId] [nvarchar](128) NOT NULL

   ,CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId])
)
GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
   FOREIGN KEY([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id])
   ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
   FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
   ON DELETE CASCADE
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId])
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] ([UserId])