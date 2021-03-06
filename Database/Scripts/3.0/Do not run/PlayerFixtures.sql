/*
   17 June 201121:50:04
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
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_Player
GO
ALTER TABLE dbo.Players SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Players', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Players', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_Fixture
GO
ALTER TABLE dbo.Fixtures SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Fixtures', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PlayerFixtures
	DROP CONSTRAINT FK_PlayerFixture_TeamLeague
GO
ALTER TABLE dbo.TeamLeagues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TeamLeagues', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
	Fouls int NOT NULL,
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
		SELECT Id, TeamLeague_Id, Fixture_Id, Player_Id, PointsScored, CONVERT(int, Fouls), IsMvp FROM dbo.PlayerFixtures WITH (HOLDLOCK TABLOCKX)')
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
select Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PlayerFixtures', 'Object', 'CONTROL') as Contr_Per 