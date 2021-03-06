/*
   25 July 201223:09:21
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
ALTER TABLE dbo.FixtureHistories ADD
	IsForfeit bit NOT NULL CONSTRAINT DF_FixtureHistories_IsForfeit DEFAULT 0,
	ForfeitingTeam_Id int NULL
GO
ALTER TABLE dbo.FixtureHistories SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.FixtureHistories', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.FixtureHistories', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.FixtureHistories', 'Object', 'CONTROL') as Contr_Per 