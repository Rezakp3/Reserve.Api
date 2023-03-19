
CREATE TABLE [dbo].[Reserves](
	[Id] [uniqueidentifier] NOT NULL,
	[CreateAt] [datetime] NULL,
	[ReserveDate] [date] NULL,
	[ReserverId] [uniqueidentifier] NOT NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Reserves_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
