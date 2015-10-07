SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.[FixtureHistories](
	[Id] [int] NOT NULL,
	[Fixture_Id] [int] NOT NULL,
	[HomeTeamLeague_Id] [int] NOT NULL,
	[AwayTeamLeague_Id] [int] NOT NULL,
	[FixtureDate] [date] NOT NULL,
	[HomeTeamScore] [int] NULL,
	[AwayTeamScore] [int] NULL,
	[CupRoundNo] [int] NULL,
	[Cup_Id] [int] NULL,
	[IsPlayed] [char](1) NOT NULL,
	[ResultAddedDate] [datetime] NULL,
	[IsCancelled] [char](1) NOT NULL,
	[TipOffTime] [varchar](5) NULL,
	[HasPlayerStats] [char](1) NOT NULL,
	[IsPenaltyAllowed] [bit] NOT NULL,
	[Referee1_Id] [int] NULL,
	[Referee2_Id] [int] NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy_Id] [int] NULL,
	[Change] [nvarchar](10) NOT NULL,
	[IsForfeit] [bit] NOT NULL,
	[ForfeitingTeam_Id] [int] NULL,
	[IsCupFixture] [bit] NOT NULL,
	[CupRoundName] [varchar](20) NULL
)

GO

