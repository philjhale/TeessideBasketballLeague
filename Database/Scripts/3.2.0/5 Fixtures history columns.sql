/*
   10 June 201210:48:30
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
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Users', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures ADD
	LastUpdated datetime NULL,
	LastUpdatedBy_Id int NULL
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixtures_Users FOREIGN KEY
	(
	LastUpdatedBy_Id
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per 

SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	DF_Fixtures_LastUpdated DEFAULT GetDate() FOR LastUpdated
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per 