USE paalerac_colores
GO

/****** Object:  Table [dbo].[CategoryMarcas]    Script Date: 17/03/2023 12:37:45 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CategoryMarcas](
	[CategoryMarcaId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[MarcaId] [int] NOT NULL,
	[State] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryMarcaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CategoryMarcas]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO

ALTER TABLE [dbo].[CategoryMarcas]  WITH CHECK ADD FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marcas] ([MarcaId])
GO


