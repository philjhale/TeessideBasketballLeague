SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Phil Hale
-- Create date: 10/06/2012
-- Description:	Populate the fixture history table
-- =============================================
CREATE TRIGGER dbo.[PopulateFixtureHistory]
   ON  [Fixtures]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @change VARCHAR(10)
	
	-- This is a bit pants but who cares
	IF(EXISTS(SELECT 1 FROM inserted)) 
	BEGIN
		SET @change = 'Updated'
		INSERT INTO [dbo].[FixtureHistories]
           ([Fixture_Id]
           ,[HomeTeamLeague_Id]
           ,[AwayTeamLeague_Id]
           ,[FixtureDate]
           ,[HomeTeamScore]
           ,[AwayTeamScore]
           ,[IsCupFixture]
           ,[CupRoundNo]
           ,[Cup_Id]
           ,[IsPlayed]
           ,[ResultAddedDate]
           ,[IsCancelled]
           ,[TipOffTime]
           ,[HasPlayerStats]
           ,[IsPenaltyAllowed]
           ,[Referee1_Id]
           ,[Referee2_Id]
           ,[LastUpdated]
           ,[LastUpdatedBy_Id]
           ,[Change]
           ,[IsForfeit]
           ,[ForfeitingTeam_Id]
           ,[CupRoundName])
     SELECT
           i.Id,
           i.HomeTeamLeague_Id,
           i.AwayTeamLeague_Id,
           i.FixtureDate,
           i.HomeTeamScore,
           i.AwayTeamScore,
           i.IsCupFixture,
           i.CupRoundNo,
           i.Cup_Id,
           i.IsPlayed,
           i.ResultAddedDate,
           i.IsCancelled,
           i.TipOffTime,
           i.HasPlayerStats,
           i.IsPenaltyAllowed,
           i.Referee1_Id,
           i.Referee2_Id,
           i.LastUpdated,
           i.LastUpdatedBy_Id,
           @change,
           i.IsForfeit,
           i.ForfeitingTeam_Id,
           i.CupRoundName
     FROM inserted i
	END
	ELSE
	BEGIN
		SET @change = 'Deleted'INSERT INTO [dbo].[FixtureHistories]
           ([Fixture_Id]
           ,[HomeTeamLeague_Id]
           ,[AwayTeamLeague_Id]
           ,[FixtureDate]
           ,[HomeTeamScore]
           ,[AwayTeamScore]
           ,[IsCupFixture]
           ,[CupRoundNo]
           ,[Cup_Id]
           ,[IsPlayed]
           ,[ResultAddedDate]
           ,[IsCancelled]
           ,[TipOffTime]
           ,[HasPlayerStats]
           ,[IsPenaltyAllowed]
           ,[Referee1_Id]
           ,[Referee2_Id]
           ,[LastUpdated]
           ,[LastUpdatedBy_Id]
           ,[Change]
           ,[IsForfeit]
           ,[ForfeitingTeam_Id]
           ,[CupRoundName])
     SELECT
           i.Id,
           i.HomeTeamLeague_Id,
           i.AwayTeamLeague_Id,
           i.FixtureDate,
           i.HomeTeamScore,
           i.AwayTeamScore,
           i.IsCupFixture,
           i.CupRoundNo,
           i.Cup_Id,
           i.IsPlayed,
           i.ResultAddedDate,
           i.IsCancelled,
           i.TipOffTime,
           i.HasPlayerStats,
           i.IsPenaltyAllowed,
           i.Referee1_Id,
           i.Referee2_Id,
           i.LastUpdated,
           i.LastUpdatedBy_Id,
           @change,
           i.IsForfeit,
           i.ForfeitingTeam_Id,
           i.CupRoundName
     FROM deleted i
		
	END

    

END

GO

