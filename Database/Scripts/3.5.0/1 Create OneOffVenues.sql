SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.[OneOffVenues](
	[Id] [int] NOT NULL,
	[Venue] [varchar](50) NOT NULL,
	[AddressLine1] [varchar](50) NULL,
	[AddressLine2] [varchar](50) NULL,
	[AddressLine3] [varchar](50) NULL,
	[AddressTown] [varchar](50) NULL,
	[AddressCounty] [varchar](50) NULL,
	[AddressPostCode] [varchar](9) NULL
)

GO

