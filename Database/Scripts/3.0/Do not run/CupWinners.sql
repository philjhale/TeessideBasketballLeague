/*
   17 June 201121:49:26
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
EXECUTE sp_rename N'dbo.CupWinners.SeasonFk', N'Tmp_Season_Id_3', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupWinners.CupFk', N'Tmp_Cup_Id_4', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupWinners.TeamFk', N'Tmp_Team_Id_5', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupWinners.Tmp_Season_Id_3', N'Season_Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupWinners.Tmp_Cup_Id_4', N'Cup_Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupWinners.Tmp_Team_Id_5', N'Team_Id', 'COLUMN' 
GO
ALTER TABLE dbo.CupWinners SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'CONTROL') as Contr_Per 