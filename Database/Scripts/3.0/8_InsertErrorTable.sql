SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.[Errors](
	[Id] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[StackTrace] [text] NULL,
	[Username] [nvarchar](50) NULL,
	[DateStamp] [datetime] NOT NULL
)

GO

