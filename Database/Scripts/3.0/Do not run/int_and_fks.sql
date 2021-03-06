/*
   17 June 201121:48:48
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
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT FK_Fixture_Cup
GO
ALTER TABLE dbo.Cups SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Cups', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Cups', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Cups', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
	GamesLostHome tinyint NOT NULL,
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
		SELECT Id, LeagueFk, TeamFk, TeamName, TeamNameLong, CONVERT(int, GamesWonTotal), CONVERT(int, GamesLostTotal), GamesPct, CONVERT(int, GamesWonHome), GamesLostHome, CONVERT(int, GamesWonAway), CONVERT(int, GamesLostAway), CONVERT(int, GamesPlayed), CONVERT(int, PointsLeague), CONVERT(int, PointsScoredFor), CONVERT(int, PointsScoredAgainst), CONVERT(int, PointsScoredDifference), Streak, PointsScoredPerGameAvg, PointsAgainstPerGameAvg, PointsScoredPerGameAvgDifference, CONVERT(int, PointsPenalty) FROM dbo.TeamLeagues WITH (HOLDLOCK TABLOCKX)')
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
ALTER TABLE dbo.Penalties
	DROP CONSTRAINT FK_Penalty_Team
GO
ALTER TABLE dbo.Teams SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Teams', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Teams', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Players
	(
	Id int NOT NULL IDENTITY (1, 1),
	TeamFk int NULL,
	Forename varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	MiddleName varchar(50) NULL,
	Dob date NULL,
	Email varchar(50) NULL,
	HeightFeet int NULL,
	HeightInches int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Players SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Players ON
GO
IF EXISTS(SELECT * FROM dbo.Players)
	 EXEC('INSERT INTO dbo.Tmp_Players (Id, TeamFk, Forename, Surname, MiddleName, Dob, Email, HeightFeet, HeightInches)
		SELECT Id, TeamFk, Forename, Surname, MiddleName, Dob, Email, CONVERT(int, HeightFeet), CONVERT(int, HeightInches) FROM dbo.Players WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Players OFF
GO
ALTER TABLE dbo.Contacts
	DROP CONSTRAINT FK_Contact_Player
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_Player
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT FK_PlayerCareerStats_Players
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT FK_PlayerSeasonStats_Players
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Players
GO
DROP TABLE dbo.Players
GO
EXECUTE sp_rename N'dbo.Tmp_Players', N'Players', 'OBJECT' 
GO
ALTER TABLE dbo.Players ADD CONSTRAINT
	PK_Player PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Player ON dbo.Players
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Player_TeamId ON dbo.Players
	(
	TeamFk
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Players', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_TotalPoints
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_PointsPerGame
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_Fouls
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_FoulsPerGame
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_GamesPlayed
GO
ALTER TABLE dbo.PlayerCareerStats
	DROP CONSTRAINT DF_PlayerCareerStats_MvpAwards
GO
CREATE TABLE dbo.Tmp_PlayerCareerStats
	(
	Id int NOT NULL IDENTITY (1, 1),
	Player_Id int NOT NULL,
	TotalPoints int NOT NULL,
	PointsPerGame decimal(4, 2) NOT NULL,
	TotalFouls int NOT NULL,
	FoulsPerGame decimal(4, 2) NOT NULL,
	GamesPlayed int NOT NULL,
	MvpAwards smallint NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_TotalPoints DEFAULT ((0)) FOR TotalPoints
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_PointsPerGame DEFAULT ((0)) FOR PointsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_Fouls DEFAULT ((0)) FOR TotalFouls
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_FoulsPerGame DEFAULT ((0)) FOR FoulsPerGame
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_GamesPlayed DEFAULT ((0)) FOR GamesPlayed
GO
ALTER TABLE dbo.Tmp_PlayerCareerStats ADD CONSTRAINT
	DF_PlayerCareerStats_MvpAwards DEFAULT ((0)) FOR MvpAwards
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerCareerStats ON
GO
IF EXISTS(SELECT * FROM dbo.PlayerCareerStats)
	 EXEC('INSERT INTO dbo.Tmp_PlayerCareerStats (Id, Player_Id, TotalPoints, PointsPerGame, TotalFouls, FoulsPerGame, GamesPlayed, MvpAwards)
		SELECT Id, PlayerFk, CONVERT(int, TotalPoints), PointsPerGame, CONVERT(int, TotalFouls), FoulsPerGame, CONVERT(int, GamesPlayed), MvpAwards FROM dbo.PlayerCareerStats WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerCareerStats OFF
GO
DROP TABLE dbo.PlayerCareerStats
GO
EXECUTE sp_rename N'dbo.Tmp_PlayerCareerStats', N'PlayerCareerStats', 'OBJECT' 
GO
ALTER TABLE dbo.PlayerCareerStats ADD CONSTRAINT
	PK_PlayerCareerStats PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PlayerCareerStats ADD CONSTRAINT
	FK_PlayerCareerStats_Players FOREIGN KEY
	(
	Player_Id
	) REFERENCES dbo.Players
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerCareerStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerCareerStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerCareerStats', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT FK_Fixture_Contact
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT FK_Fixture_Contact1
GO
ALTER TABLE dbo.Contacts ADD CONSTRAINT
	FK_Contact_Player FOREIGN KEY
	(
	PlayerId
	) REFERENCES dbo.Players
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Contacts SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Contacts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Contacts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Contacts', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT DF_Fixture_CupFixture
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT DF_Fixture_IsPlayed
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT DF__Fixtures__IsCanc__26CFC035
GO
ALTER TABLE dbo.Fixtures
	DROP CONSTRAINT DF_Fixtures_IsWalkover
GO
CREATE TABLE dbo.Tmp_Fixtures
	(
	Id int NOT NULL IDENTITY (1, 1),
	HomeTeamLeague_Id int NOT NULL,
	AwayTeamLeague_Id int NOT NULL,
	FixtureDate date NOT NULL,
	HomeTeamScore int NULL,
	AwayTeamScore int NULL,
	Report ntext NULL,
	IsCupFixture char(1) NULL,
	CupRoundNo int NULL,
	CupFk int NULL,
	IsPlayed char(1) NOT NULL,
	ResultAddedDate datetime NULL,
	IsCancelled char(1) NOT NULL,
	TipOffTime varchar(5) NULL,
	HasPlayerStats char(1) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Fixtures SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Fixture details'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Fixtures', N'COLUMN', N'Id'
GO
ALTER TABLE dbo.Tmp_Fixtures ADD CONSTRAINT
	DF_Fixture_CupFixture DEFAULT ('N') FOR IsCupFixture
GO
ALTER TABLE dbo.Tmp_Fixtures ADD CONSTRAINT
	DF_Fixture_IsPlayed DEFAULT ('N') FOR IsPlayed
GO
ALTER TABLE dbo.Tmp_Fixtures ADD CONSTRAINT
	DF__Fixtures__IsCanc__26CFC035 DEFAULT ('N') FOR IsCancelled
GO
ALTER TABLE dbo.Tmp_Fixtures ADD CONSTRAINT
	DF_Fixtures_IsWalkover DEFAULT ('N') FOR HasPlayerStats
GO
SET IDENTITY_INSERT dbo.Tmp_Fixtures ON
GO
IF EXISTS(SELECT * FROM dbo.Fixtures)
	 EXEC('INSERT INTO dbo.Tmp_Fixtures (Id, HomeTeamLeague_Id, AwayTeamLeague_Id, FixtureDate, HomeTeamScore, AwayTeamScore, Report, IsCupFixture, CupRoundNo, CupFk, IsPlayed, ResultAddedDate, IsCancelled, TipOffTime, HasPlayerStats)
		SELECT Id, HomeTeamLeagueFk, AwayTeamLeagueFk, FixtureDate, CONVERT(int, HomeTeamScore), CONVERT(int, AwayTeamScore), Report, IsCupFixture, CONVERT(int, CupRoundNo), CupFk, IsPlayed, ResultAddedDate, IsCancelled, TipOffTime, HasPlayerStats FROM dbo.Fixtures WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Fixtures OFF
GO
ALTER TABLE dbo.Penalties
	DROP CONSTRAINT FK_Penalties_Fixtures
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_Fixture
GO
DROP TABLE dbo.Fixtures
GO
EXECUTE sp_rename N'dbo.Tmp_Fixtures', N'Fixtures', 'OBJECT' 
GO
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	PK_Fixture PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Fixture ON dbo.Fixtures
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Fixture_HomeTeamLeagueId ON dbo.Fixtures
	(
	HomeTeamLeague_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Fixture_AwayTeamLeagueId ON dbo.Fixtures
	(
	AwayTeamLeague_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE dbo.Fixtures ADD CONSTRAINT
	FK_Fixture_Cup FOREIGN KEY
	(
	CupFk
	) REFERENCES dbo.Cups
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT DF_PlayerFixtures_PointsScored
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT DF_PlayerFixtures_Fouls
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT DF_PlayerFixtures_IsMvp
GO
CREATE TABLE dbo.Tmp_PlayerFixtures
	(
	Id int NOT NULL IDENTITY (1, 1),
	TeamLeague_Id int NOT NULL,
	Fixture_Id int NOT NULL,
	Player_Id int NOT NULL,
	PointsScored int NOT NULL,
	Fouls tinyint NOT NULL,
	IsMvp char(1) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PlayerFixtures SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Holds player stats for a fixture'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_PlayerFixtures', N'COLUMN', N'Id'
GO
ALTER TABLE dbo.Tmp_PlayerFixtures ADD CONSTRAINT
	DF_PlayerFixtures_PointsScored DEFAULT ((0)) FOR PointsScored
GO
ALTER TABLE dbo.Tmp_PlayerFixtures ADD CONSTRAINT
	DF_PlayerFixtures_Fouls DEFAULT ((0)) FOR Fouls
GO
ALTER TABLE dbo.Tmp_PlayerFixtures ADD CONSTRAINT
	DF_PlayerFixtures_IsMvp DEFAULT ('N') FOR IsMvp
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerFixtures ON
GO
IF EXISTS(SELECT * FROM dbo.PlayerFixtures)
	 EXEC('INSERT INTO dbo.Tmp_PlayerFixtures (Id, TeamLeague_Id, Fixture_Id, Player_Id, PointsScored, Fouls, IsMvp)
		SELECT Id, TeamLeagueFk, FixtureFk, PlayerFk, CONVERT(int, PointsScored), Fouls, IsMvp FROM dbo.PlayerFixtures WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PlayerFixtures OFF
GO
DROP TABLE dbo.PlayerFixtures
GO
EXECUTE sp_rename N'dbo.Tmp_PlayerFixtures', N'PlayerFixtures', 'OBJECT' 
GO
ALTER TABLE dbo.PlayerFixtures ADD CONSTRAINT
	PK_PlayerFixture PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_PlayerFixture ON dbo.PlayerFixtures
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_PlayerFixture_TeamLeagueId ON dbo.PlayerFixtures
	(
	TeamLeague_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_PlayerFixture_FixtureId ON dbo.PlayerFixtures
	(
	Fixture_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_PlayerFixture_PlayerId ON dbo.PlayerFixtures
	(
	Player_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE dbo.PlayerFixtures ADD CONSTRAINT
	FK_PlayerFixture_Fixture FOREIGN KEY
	(
	Fixture_Id
	) REFERENCES dbo.Fixtures
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PlayerFixtures ADD CONSTRAINT
	FK_PlayerFixture_Player FOREIGN KEY
	(
	Player_Id
	) REFERENCES dbo.Players
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerLeagueStats
	DROP CONSTRAINT FK_PlayerLeagueStats_Seasons
GO
ALTER TABLE dbo.PlayerSeasonStats
	DROP CONSTRAINT FK_PlayerSeasonStats_Seasons
GO
ALTER TABLE dbo.Leagues
	DROP CONSTRAINT FK_League_Season
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
	MvpAwards smallint NOT NULL
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
		SELECT Id, PlayerFk, SeasonFk, CONVERT(int, TotalPoints), PointsPerGame, CONVERT(int, TotalFouls), FoulsPerGame, CONVERT(int, GamesPlayed), MvpAwards FROM dbo.PlayerSeasonStats WITH (HOLDLOCK TABLOCKX)')
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
select Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerSeasonStats', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Leagues
	(
	Id int NOT NULL IDENTITY (1, 1),
	Season_Id int NOT NULL,
	LeagueDescription varchar(50) NOT NULL,
	DivisionNo int NULL,
	DisplayOrder tinyint NOT NULL
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
		SELECT Id, SeasonFk, LeagueDescription, CONVERT(int, DivisionNo), DisplayOrder FROM dbo.Leagues WITH (HOLDLOCK TABLOCKX)')
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
EXECUTE sp_rename N'dbo.LeagueWinners.LeagueFk', N'Tmp_League_Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.LeagueWinners.Tmp_League_Id', N'League_Id', 'COLUMN' 
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
ALTER TABLE dbo.Penalties
	DROP CONSTRAINT DF_Penaltys_Points
GO
CREATE TABLE dbo.Tmp_Penalties
	(
	Id int NOT NULL IDENTITY (1, 1),
	League_Id int NOT NULL,
	Team_Id int NOT NULL,
	Reason varchar(200) NOT NULL,
	PenaltyDate datetime NOT NULL,
	Points int NOT NULL,
	FixtureFk int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Penalties SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Penalties ADD CONSTRAINT
	DF_Penaltys_Points DEFAULT ((1)) FOR Points
GO
SET IDENTITY_INSERT dbo.Tmp_Penalties ON
GO
IF EXISTS(SELECT * FROM dbo.Penalties)
	 EXEC('INSERT INTO dbo.Tmp_Penalties (Id, League_Id, Team_Id, Reason, PenaltyDate, Points, FixtureFk)
		SELECT Id, LeagueFk, TeamFk, Reason, PenaltyDate, CONVERT(int, Points), FixtureFk FROM dbo.Penalties WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Penalties OFF
GO
DROP TABLE dbo.Penalties
GO
EXECUTE sp_rename N'dbo.Tmp_Penalties', N'Penalties', 'OBJECT' 
GO
ALTER TABLE dbo.Penalties ADD CONSTRAINT
	PK_Penaltys PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Penalties ADD CONSTRAINT
	FK_Penalties_Fixtures FOREIGN KEY
	(
	FixtureFk
	) REFERENCES dbo.Fixtures
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
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
ALTER TABLE dbo.Penalties ADD CONSTRAINT
	FK_Penalty_Team FOREIGN KEY
	(
	Team_Id
	) REFERENCES dbo.Teams
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Penalties', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
	MvpAwards smallint NOT NULL
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
		SELECT Id, PlayerFk, SeasonFk, LeagueFk, CONVERT(int, TotalPoints), PointsPerGame, CONVERT(int, TotalFouls), FoulsPerGame, CONVERT(int, GamesPlayed), MvpAwards FROM dbo.PlayerLeagueStats WITH (HOLDLOCK TABLOCKX)')
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
select Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerLeagueStats', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.CupLeagues.CupFk', N'Tmp_Cup_Id_1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupLeagues.LeagueFk', N'Tmp_League_Id_2', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupLeagues.Tmp_Cup_Id_1', N'Cup_Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.CupLeagues.Tmp_League_Id_2', N'League_Id', 'COLUMN' 
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
select Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CupLeagues', 'Object', 'CONTROL') as Contr_Per 