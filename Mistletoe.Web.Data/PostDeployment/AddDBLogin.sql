/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'IIS APPPOOL\DefaultAppPool')
BEGIN
	CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE=[Mistletoe.Data]
END

GO

USE [Mistletoe.Data];  

IF NOT EXISTS
    (SELECT name
     FROM sys.database_principals
     WHERE name = 'IIS APPPOOL\DefaultAppPool')
BEGIN
	CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA = dbo;  
	EXEC sp_addrolemember @rolename =  'db_datareader', @membername = 'IIS APPPOOL\DefaultAppPool';  
	EXEC sp_addrolemember @rolename =  'db_datawriter', @membername = 'IIS APPPOOL\DefaultAppPool';  
	EXEC sp_addrolemember @rolename =  'db_ddladmin', @membername = 'IIS APPPOOL\DefaultAppPool';  
END

GO