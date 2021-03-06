/*
   17 June 201121:13:46
   User: 
   Server: Dell-PC\SQLEXPRESS
   Database: E:\DROPBOX\PROGRAMMING\DEVELOPMENT\BASKETBALL\BASKETBALL\DB\BASKETBALLDB.MDF
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
EXECUTE sp_rename N'dbo.Teams.GameDayFk', N'Tmp_GameDay_Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Teams.Tmp_GameDay_Id', N'GameDay_Id', 'COLUMN' 
GO
ALTER TABLE dbo.Teams SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Teams', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'CONTROL') as Contr_Per 