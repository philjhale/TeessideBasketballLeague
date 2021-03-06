/*
   17 June 201121:49:56
   User: 
   Server: Dell-PC\SQLEXPRESS
   Database: BasketballDb
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
EXECUTE sp_rename N'dbo.Penalties.FixtureFk', N'Tmp_Fixture_Id_8', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Penalties.Tmp_Fixture_Id_8', N'Fixture_Id', 'COLUMN' 
GO
ALTER TABLE dbo.Penalties SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'CONTROL') as Contr_Per 