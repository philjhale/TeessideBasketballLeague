/*
   17 June 201121:50:23
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
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesWonTotal
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesLostTotal
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesPct
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesWonHome
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesLostHome
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesWonAway
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesLostAway
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_GamesPlayed
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_PointsLeague
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_PointsScoredFor
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_PointsScoredAgainst
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeague_PointsScoredDifference
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeagues_PointsScorePerGameAvg
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeagues_PointsAgainstPerGameAvg
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF_TeamLeagues_PointsScoredPerGameAvgDifference
GO
ALTER TABLE dbo.TeamLeagues
	DROP CONSTRAINT DF__TeamLeagu__Point__4460231C
GO
CREATE TABLE dbo.Tmp_TeamLeagues
	(
	Id int NOT NULL IDENTITY (1, 1),
	League_Id int NOT NULL,
	Team_Id int NOT NULL,
	TeamName varchar(20) NOT NULL,
	TeamNameLong varchar(50) NOT NULL,
	GamesWonTotal int NOT NULL,
	GamesLostTotal int NOT NULL,
	GamesPct decimal(3, 2) NOT NULL,
	GamesWonHome int NOT NULL,
	GamesLostHome int NOT NULL,
	GamesWonAway int NOT NULL,
	GamesLostAway int NOT NULL,
	GamesPlayed int NOT NULL,
	PointsLeague int NOT NULL,
	PointsScoredFor int NOT NULL,
	PointsScoredAgainst int NOT NULL,
	PointsScoredDifference int NOT NULL,
	Streak varchar(3) NULL,
	PointsScoredPerGameAvg decimal(5, 2) NULL,
	PointsAgainstPerGameAvg decimal(5, 2) NULL,
	PointsScoredPerGameAvgDifference decimal(5, 2) NULL,
	PointsPenalty int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TeamLeagues SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Holds team information for a particular league/season'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TeamLeagues', N'COLUMN', N'Id'
GO
DECLARE @v sql_variant 
SET @v = N'This data is stored for historical purposes. i.e. if a team name changes from one season to the next'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TeamLeagues', N'COLUMN', N'TeamName'
GO
DECLARE @v sql_variant 
SET @v = N'This data is stored for historical purposes. i.e. if a team name changes from one season to the next'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TeamLeagues', N'COLUMN', N'TeamNameLong'
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesWonTotal DEFAULT ((0)) FOR GamesWonTotal
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesLostTotal DEFAULT ((0)) FOR GamesLostTotal
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesPct DEFAULT ((0.00)) FOR GamesPct
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesWonHome DEFAULT ((0)) FOR GamesWonHome
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesLostHome DEFAULT ((0)) FOR GamesLostHome
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesWonAway DEFAULT ((0)) FOR GamesWonAway
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesLostAway DEFAULT ((0)) FOR GamesLostAway
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_GamesPlayed DEFAULT ((0)) FOR GamesPlayed
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_PointsLeague DEFAULT ((0)) FOR PointsLeague
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_PointsScoredFor DEFAULT ((0)) FOR PointsScoredFor
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_PointsScoredAgainst DEFAULT ((0)) FOR PointsScoredAgainst
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeague_PointsScoredDifference DEFAULT ((0)) FOR PointsScoredDifference
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeagues_PointsScorePerGameAvg DEFAULT ((0.00)) FOR PointsScoredPerGameAvg
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeagues_PointsAgainstPerGameAvg DEFAULT ((0.00)) FOR PointsAgainstPerGameAvg
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF_TeamLeagues_PointsScoredPerGameAvgDifference DEFAULT ((0.00)) FOR PointsScoredPerGameAvgDifference
GO
ALTER TABLE dbo.Tmp_TeamLeagues ADD CONSTRAINT
	DF__TeamLeagu__Point__4460231C DEFAULT ((0)) FOR PointsPenalty
GO
SET IDENTITY_INSERT dbo.Tmp_TeamLeagues ON
GO
IF EXISTS(SELECT * FROM dbo.TeamLeagues)
	 EXEC('INSERT INTO dbo.Tmp_TeamLeagues (Id, League_Id, Team_Id, TeamName, TeamNameLong, GamesWonTotal, GamesLostTotal, GamesPct, GamesWonHome, GamesLostHome, GamesWonAway, GamesLostAway, GamesPlayed, PointsLeague, PointsScoredFor, PointsScoredAgainst, PointsScoredDifference, Streak, PointsScoredPerGameAvg, PointsAgainstPerGameAvg, PointsScoredPerGameAvgDifference, PointsPenalty)
		SELECT Id, League_Id, Team_Id, TeamName, TeamNameLong, GamesWonTotal, GamesLostTotal, GamesPct, GamesWonHome, CONVERT(int, GamesLostHome), GamesWonAway, GamesLostAway, GamesPlayed, PointsLeague, PointsScoredFor, PointsScoredAgainst, PointsScoredDifference, Streak, PointsScoredPerGameAvg, PointsAgainstPerGameAvg, PointsScoredPerGameAvgDifference, PointsPenalty FROM dbo.TeamLeagues WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TeamLeagues OFF
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT FK_Fixture_TeamLeague
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT FK_Fixture_TeamLeagueAway
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_TeamLeague
GO
DROP TABLE dbo.TeamLeagues
GO
EXECUTE sp_rename N'dbo.Tmp_TeamLeagues', N'TeamLeagues', 'OBJECT' 
GO
ALTER TABLE dbo.TeamLeagues ADD CONSTRAINT
	PK_TeamLeague PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_TeamLeague ON dbo.TeamLeagues
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_TeamLeague_League ON dbo.TeamLeagues
	(
	League_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_TeamLeague_Team ON dbo.TeamLeagues
	(
	Team_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerFixtures ADD CONSTRAINT
	FK_PlayerFixture_TeamLeague FOREIGN KEY
	(
	TeamLeague_Id
	) REFERENCES dbo.TeamLeagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerFixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixture_TeamLeague FOREIGN KEY
	(
	HomeTeamLeague_Id
	) REFERENCES dbo.TeamLeagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixture_TeamLeagueAway FOREIGN KEY
	(
	AwayTeamLeague_Id
	) REFERENCES dbo.TeamLeagues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per 