
CREATE TABLE [dbo].[Locations](
	[Id] [uniqueidentifier] NOT NULL,
	[LocationType] [smallint] NULL,
	[Title] [nvarchar](50) NULL,
	[Adres] [nvarchar](200) NULL,
	[CreateAt] [datetime] NULL,
	[CreatorId] [uniqueidentifier] NOT NULL,
	[Longitude] [decimal](9, 6) NULL,
	[latitude] [decimal](9, 6) NULL,
 CONSTRAINT [PK_Locations_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]