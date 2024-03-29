USE [MapPolygon]
GO
/****** Object:  Table [dbo].[Polygons]    Script Date: 9/1/2019 12:32:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Polygons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationCoordinates] [nvarchar](max) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Polygons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
