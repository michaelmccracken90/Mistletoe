CREATE TABLE [dbo].[AspNetRoles] (
   [Id] [nvarchar](128) NOT NULL,
   [Name] [nvarchar](256) NOT NULL

   ,CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([Name])