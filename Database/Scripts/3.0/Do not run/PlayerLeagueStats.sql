/*
   17 June 201121:50:10
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
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Players
GO
ALTER TABLE dbo.Players SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Players', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Seasons
GO
ALTER TABLE dbo.Seasons SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Leagues
GO
ALTER TABLE dbo.Leagues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Leagues', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_TotalPoints
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_PointsPerGame
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_TotalFouls
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_FoulsPerGame
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_GamesPlayed
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT DF_PlayerLeagueStats_MvpAwards
GO
CREATE TABLE dbo.Tmp_PlayerLeagueStats
	(
	Id int NOT NULL IDENTITY (1, 1),
	Player_Id int NOT NULL,
	Season_Id int NOT NULL,
	League_Id int NOT NULL,
	TotalPoints int NOT NULL,
	PointsPerGame decimal(4, 2) NOT NULL,
	TotalFouls int NOT NULL,
	FoulsPerGame decimal(4, 2) NOT NULL,
	GamesPlayed int NOT NULL,
	MvpAwards int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_TotalPoints DEFAULT ((0)) FOR TotalPoints
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_PointsPerGame DEFAULT ((0)) FOR PointsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_TotalFouls DEFAULT ((0)) FOR TotalFouls
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_FoulsPerGame DEFAULT ((0)) FOR FoulsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_GamesPlayed DEFAULT ((0)) FOR GamesPlayed
GO
ALTER TABLE dbo.Tmp_PlayerLeagueStats ADD CONSTRAINT
	DF_PlayerLeagueStats_MvpAwards DEFAULT ((0)) FOR MvpAwards
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerLeagueStats ON
GO
IF EXISTS(SELECT * FROM dbo.PlayerLeagueStats)
	 EXEC('INSERT INTO dbo.Tmp_PlayerLeagueStats (Id, Player_Id, Season_Id, League_Id, TotalPoints, PointsPerGame, TotalFouls, FoulsPerGame, GamesPlayed, MvpAwards)
		SELECT Id, Player_Id, Season_Id, League_Id, TotalPoints, PointsPerGame, TotalFouls, FoulsPerGame, GamesPlayed, CONVERT(int, MvpAwards) FROM dbo.PlayerLeagueStats WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerLeagueStats OFF
GO
DROP TABLE dbo.PlayerLeagueStats
GO
EXECUTE sp_rename N'dbo.Tmp_PlayerLeagueStats', N'PlayerLeagueStats', 'OBJECT' 
GO
ALTER TABLE dbo.PlayerLeagueStats ADD CONSTRAINT
	PK_PlayerLeagueStats PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

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
ALTER TABLE dbo.PlayerLeagueStats ADD CONSTRAINT
	FK_PlayerLeagueStats_Seasons FOREIGN KEY
	(
	Season_Id
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerLeagueStats ADD CONSTRAINT
	FK_PlayerLeagueStats_Players FOREIGN KEY
	(
	Player_Id
	) REFERENCES dbo.Players
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'CONTROL') as Contr_Per 