/*
   17 June 201121:50:19
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
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT FK_PlayerSeasonStats_Players
GO
ALTER TABLE dbo.Players SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Players', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT FK_PlayerSeasonStats_Seasons
GO
ALTER TABLE dbo.Seasons SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Seasons', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_TotalPoints
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_PointsPerGame
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_TotalFouls
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_FoulsPerGame
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_GamesPlayed
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT DF_PlayerSeasonStats_MvpAwards
GO
CREATE TABLE dbo.Tmp_PlayerSeasonStats
	(
	Id int NOT NULL IDENTITY (1, 1),
	Player_Id int NOT NULL,
	Season_Id int NOT NULL,
	TotalPoints int NOT NULL,
	PointsPerGame decimal(4, 2) NOT NULL,
	TotalFouls int NOT NULL,
	FoulsPerGame decimal(4, 2) NOT NULL,
	GamesPlayed int NOT NULL,
	MvpAwards int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_TotalPoints DEFAULT ((0)) FOR TotalPoints
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_PointsPerGame DEFAULT ((0)) FOR PointsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_TotalFouls DEFAULT ((0)) FOR TotalFouls
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_FoulsPerGame DEFAULT ((0)) FOR FoulsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_GamesPlayed DEFAULT ((0)) FOR GamesPlayed
GO
ALTER TABLE dbo.Tmp_PlayerSeasonStats ADD CONSTRAINT
	DF_PlayerSeasonStats_MvpAwards DEFAULT ((0)) FOR MvpAwards
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerSeasonStats ON
GO
IF EXISTS(SELECT * FROM dbo.PlayerSeasonStats)
	 EXEC('INSERT INTO dbo.Tmp_PlayerSeasonStats (Id, Player_Id, Season_Id, TotalPoints, PointsPerGame, TotalFouls, FoulsPerGame, GamesPlayed, MvpAwards)
		SELECT Id, Player_Id, Season_Id, TotalPoints, PointsPerGame, TotalFouls, FoulsPerGame, GamesPlayed, CONVERT(int, MvpAwards) FROM dbo.PlayerSeasonStats WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerSeasonStats OFF
GO
DROP TABLE dbo.PlayerSeasonStats
GO
EXECUTE sp_rename N'dbo.Tmp_PlayerSeasonStats', N'PlayerSeasonStats', 'OBJECT' 
GO
ALTER TABLE dbo.PlayerSeasonStats ADD CONSTRAINT
	PK_PlayerSeasonStats PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PlayerSeasonStats ADD CONSTRAINT
	FK_PlayerSeasonStats_Seasons FOREIGN KEY
	(
	Season_Id
	) REFERENCES dbo.Seasons
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerSeasonStats ADD CONSTRAINT
	FK_PlayerSeasonStats_Players FOREIGN KEY
	(
	Player_Id
	) REFERENCES dbo.Players
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'CONTROL') as Contr_Per 