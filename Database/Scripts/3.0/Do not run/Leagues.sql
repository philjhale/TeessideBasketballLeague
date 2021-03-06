/*
   17 June 201121:49:45
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
ALTER TABLE dbo.Leagues
	DROP CONSTRAINT FK_League_Season
GO
ALTER TABLE dbo.Seasons SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Leagues
	(
	Id int NOT NULL IDENTITY (1, 1),
	Season_Id int NOT NULL,
	LeagueDescription varchar(50) NOT NULL,
	DivisionNo int NULL,
	DisplayOrder int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Leagues SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Leagues for each season'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Leagues', N'COLUMN', N'Id'
GO
SET IDENTITY_INSERT dbo.Tmp_Leagues ON
GO
IF EXISTS(SELECT * FROM dbo.Leagues)
	 EXEC('INSERT INTO dbo.Tmp_Leagues (Id, Season_Id, LeagueDescription, DivisionNo, DisplayOrder)
		SELECT Id, Season_Id, LeagueDescription, DivisionNo, CONVERT(int, DisplayOrder) FROM dbo.Leagues WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Leagues OFF
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Leagues
GO
ALTER TABLE dbo.CupLeagues
	DROP CONSTRAINT FK_CupLeague_League
GO
ALTER TABLE dbo.Penalties
	DROP CONSTRAINT FK_Penalty_League
GO
ALTER TABLE dbo.LeagueWinners
	DROP CONSTRAINT FK_LeagueWinners_Leagues
GO
DROP TABLE dbo.Leagues
GO
EXECUTE sp_rename N'dbo.Tmp_Leagues', N'Leagues', 'OBJECT' 
GO
ALTER TABLE dbo.Leagues ADD CONSTRAINT
	PK_League PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_League ON dbo.Leagues
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_League_SeasonId ON dbo.Leagues
	(
	Season_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_League_LeagueTypeId ON dbo.Leagues
	(
	LeagueDescription
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Leagues ADD CONSTRAINT
	FK_League_Season FOREIGN KEY
	(
	Season_Id
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.LeagueWinners ADD CONSTRAINT
	FK_LeagueWinners_Leagues FOREIGN KEY
	(
	League_Id
	) REFERENCES dbo.Leagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LeagueWinners SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.LeagueWinners', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.LeagueWinners', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.LeagueWinners', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Penalties ADD CONSTRAINT
	FK_Penalty_League FOREIGN KEY
	(
	League_Id
	) REFERENCES dbo.Leagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Penalties SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CupLeagues ADD CONSTRAINT
	FK_CupLeague_League FOREIGN KEY
	(
	League_Id
	) REFERENCES dbo.Leagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CupLeagues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats ADD CONSTRAINT
	FK_PlayerLeagueStats_Leagues FOREIGN KEY
	(
	League_Id
	) REFERENCES dbo.Leagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerLeagueStats SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'CONTROL') as Contr_Per 