/*
   07 June 201222:33:28
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
ALTER TABLE dbo.Referees SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Referees', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Referees', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Referees', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixtures_Referees1 FOREIGN KEY
	(
	Referee1_Id
	) REFERENCES dbo.Referees
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixtures_Referees2 FOREIGN KEY
	(
	Referee2_Id
	) REFERENCES dbo.Referees
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per 