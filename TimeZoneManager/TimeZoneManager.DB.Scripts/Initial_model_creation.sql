USE [TimeZoneManager]
GO

/****** Object:  Table [dbo].[TimeZone]    Script Date: 10/27/2015 2:11:37 AM ******/
DROP TABLE [dbo].[TimeZone]
GO

/****** Object:  Table [dbo].[TimeZone]    Script Date: 10/27/2015 2:11:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TimeZone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[City] [nvarchar](250) NOT NULL,
	[Owner] [nvarchar](250) NOT NULL,
	[GMTOffset] [int] NOT NULL,
 CONSTRAINT [PK_TimeZone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


