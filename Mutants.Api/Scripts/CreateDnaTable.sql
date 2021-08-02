USE [Mutants]
GO

/****** Object:  Table [dbo].[Dna]    Script Date: 2/8/2021 2:29:13 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Dna](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DnaStrings] [varchar](max) NOT NULL,
	[IsMutant] [bit] NOT NULL,
	[Date] [date] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


