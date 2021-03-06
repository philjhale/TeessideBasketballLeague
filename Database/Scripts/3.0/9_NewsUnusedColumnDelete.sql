/*
   20 July 201123:13:34
   User: 
   Server: Dell-PC\SQLEXPRESS
   Database: LiveBackup
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.News
	DROP COLUMN Username, UserId
GO
ALTER TABLE dbo.News SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.News', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.News', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.News', 'Object', 'CONTROL') as Contr_Per 