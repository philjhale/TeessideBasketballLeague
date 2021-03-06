/*
   25 July 201220:47:21
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
-- TODO Convert all data to use new forfeit system

SET ANSI_WARNINGS ON

COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Teams SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Teams', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures ADD
	IsForfeit bit NOT NULL DEFAULT 0,
	ForfeitingTeam_Id int NULL 
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixtures_ForfeitingTeam FOREIGN KEY
	(
	ForfeitingTeam_Id
	) REFERENCES dbo.Teams
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per 