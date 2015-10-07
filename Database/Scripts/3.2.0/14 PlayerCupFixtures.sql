SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.[PlayerCupStats](
	[Id] [int] NOT NULL,
	[Player_Id] [int] NOT NULL,
	[Season_Id] [int] NOT NULL,
	[Cup_Id] [int] NOT NULL,
	[TotalPoints] [int] NOT NULL,
	[PointsPerGame] [decimal](4, 2) NOT NULL,
	[TotalFouls] [int] NOT NULL,
	[FoulsPerGame] [decimal](4, 2) NOT NULL,
	[GamesPlayed] [int] NOT NULL,
	[MvpAwards] [int] NOT NULL
)

GO

