/*
   17 June 201121:37:43
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
CREATE TABLE dbo.Tmp_Seasons
	(
	Id int NOT NULL IDENTITY (1, 1),
	StartYear int NOT NULL,
	EndYear int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Seasons SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Season information'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Seasons', N'COLUMN', N'Id'
GO
SET IDENTITY_INSERT dbo.Tmp_Seasons ON
GO
IF EXISTS(SELECT * FROM dbo.Seasons)
	 EXEC('INSERT INTO dbo.Tmp_Seasons (Id, StartYear, EndYear)
		SELECT Id, CONVERT(int, StartYear), CONVERT(int, EndYear) FROM dbo.Seasons WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Seasons OFF
GO
ALTER TABLE dbo.Leagues
	DROP CONSTRAINT FK_League_Season
GO
ALTER TABLE dbo.CupWinners
	DROP CONSTRAINT FK_CupWinner_Seasons
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT FK_PlayerSeasonStats_Seasons
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Seasons
GO
DROP TABLE dbo.Seasons
GO
EXECUTE sp_rename N'dbo.Tmp_Seasons', N'Seasons', 'OBJECT' 
GO
ALTER TABLE dbo.Seasons ADD CONSTRAINT
	PK_Season PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Season ON dbo.Seasons
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats ADD CONSTRAINT
	FK_PlayerLeagueStats_Seasons FOREIGN KEY
	(
	SeasonFk
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerLeagueStats SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerSeasonStats ADD CONSTRAINT
	FK_PlayerSeasonStats_Seasons FOREIGN KEY
	(
	SeasonFk
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerSeasonStats SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CupWinners ADD CONSTRAINT
	FK_CupWinner_Seasons FOREIGN KEY
	(
	SeasonFk
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CupWinners SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CupWinners', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Leagues ADD CONSTRAINT
	FK_League_Season FOREIGN KEY
	(
	SeasonFk
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Leagues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'CONTROL') as Contr_Per 