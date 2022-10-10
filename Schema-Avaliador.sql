GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnoBases]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnoBases](
	[AnoBaseId] [uniqueidentifier] NOT NULL,
	[Ano] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_AnoBases] PRIMARY KEY CLUSTERED 
(
	[AnoBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Divisao]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Divisao](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Divisao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametro]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametro](
	[Id] [uniqueidentifier] NOT NULL,
	[QtdeSugestaoMin] [int] NOT NULL,
	[QtdeSugestaoMax] [int] NOT NULL,
	[QtdeAvaliador] [int] NOT NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
	[DhFinalizacao] [datetime2](7) NOT NULL,
	[QtdDiaFinaliza] [int] NOT NULL,
 CONSTRAINT [PK_Parametro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planilhas]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planilhas](
	[Id] [uniqueidentifier] NOT NULL,
	[Unidade] [nvarchar](30) NULL,
	[Nome] [nvarchar](180) NULL,
	[Email] [nvarchar](120) NULL,
	[Cargo] [nvarchar](60) NULL,
	[Nivel] [nvarchar](60) NULL,
	[DataAdmissao] [datetime2](7) NULL,
	[CentroCusto] [nvarchar](60) NULL,
	[NumeroCentroCusto] [nvarchar](60) NULL,
	[SuperiorImediato] [nvarchar](180) NULL,
	[EmailSuperior] [nvarchar](180) NULL,
	[Deleted] [bit] NOT NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
	[Direcao] [nvarchar](120) NULL,
	[CPF] [nvarchar](20) NULL,
	[EhValido] [bit] NOT NULL,
	[Validacoes] [nvarchar](max) NULL,
	[GestorCorporativo] [nvarchar](120) NULL,
 CONSTRAINT [PK_Planilhas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Processos]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Processos](
	[Id] [uniqueidentifier] NOT NULL,
	[Situacao] [int] NOT NULL,
	[DhInicio] [datetime2](7) NULL,
	[DhFim] [datetime2](7) NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Processos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Progresso]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Progresso](
	[Id] [uniqueidentifier] NOT NULL,
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Progresso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setor]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setor](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [nvarchar](60) NOT NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Setor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioAvaliador]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioAvaliador](
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[AvaliadorId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[DataHora] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UsuarioAvaliador] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 07/10/2022 16:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [uniqueidentifier] NOT NULL,
	[AzureId] [uniqueidentifier] NOT NULL,
	[Perfil] [int] NOT NULL,
	[Nome] [nvarchar](120) NOT NULL,
	[Email] [nvarchar](120) NOT NULL,
	[EmailSuperior] [nvarchar](120) NULL,
	[EhDiretor] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[SuperiorId] [uniqueidentifier] NULL,
	[AnoBaseId] [uniqueidentifier] NOT NULL,
	[DivisaoId] [uniqueidentifier] NULL,
	[SetorId] [uniqueidentifier] NULL,
	[Cargo] [nvarchar](30) NULL,
	[Senha] [nvarchar](180) NOT NULL,
	[Documento] [nvarchar](30) NULL,
	[Ativo] [bit] NOT NULL,
	[EhGestor] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Divisao] ADD  DEFAULT (N'') FOR [Nome]
GO
ALTER TABLE [dbo].[Parametro] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DhFinalizacao]
GO
ALTER TABLE [dbo].[Parametro] ADD  DEFAULT ((0)) FOR [QtdDiaFinaliza]
GO
ALTER TABLE [dbo].[Planilhas] ADD  DEFAULT (CONVERT([bit],(0))) FOR [EhValido]
GO
ALTER TABLE [dbo].[Setor] ADD  DEFAULT (N'') FOR [Nome]
GO
ALTER TABLE [dbo].[UsuarioAvaliador] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Id]
GO
ALTER TABLE [dbo].[UsuarioAvaliador] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DataHora]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Perfil]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (N'') FOR [Nome]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (N'') FOR [Senha]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Ativo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (CONVERT([bit],(0))) FOR [EhGestor]
GO
ALTER TABLE [dbo].[Divisao]  WITH CHECK ADD  CONSTRAINT [FK_Divisao_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Divisao] CHECK CONSTRAINT [FK_Divisao_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Parametro]  WITH CHECK ADD  CONSTRAINT [FK_Parametro_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
GO
ALTER TABLE [dbo].[Parametro] CHECK CONSTRAINT [FK_Parametro_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Planilhas]  WITH CHECK ADD  CONSTRAINT [FK_Planilhas_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
GO
ALTER TABLE [dbo].[Planilhas] CHECK CONSTRAINT [FK_Planilhas_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Processos]  WITH CHECK ADD  CONSTRAINT [FK_Processos_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Processos] CHECK CONSTRAINT [FK_Processos_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Progresso]  WITH CHECK ADD  CONSTRAINT [FK_Progresso_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
GO
ALTER TABLE [dbo].[Progresso] CHECK CONSTRAINT [FK_Progresso_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Progresso]  WITH CHECK ADD  CONSTRAINT [FK_Progresso_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Progresso] CHECK CONSTRAINT [FK_Progresso_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[Setor]  WITH CHECK ADD  CONSTRAINT [FK_Setor_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Setor] CHECK CONSTRAINT [FK_Setor_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[UsuarioAvaliador]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioAvaliador_Usuarios_AvaliadorId] FOREIGN KEY([AvaliadorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[UsuarioAvaliador] CHECK CONSTRAINT [FK_UsuarioAvaliador_Usuarios_AvaliadorId]
GO
ALTER TABLE [dbo].[UsuarioAvaliador]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioAvaliador_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[UsuarioAvaliador] CHECK CONSTRAINT [FK_UsuarioAvaliador_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_AnoBases_AnoBaseId] FOREIGN KEY([AnoBaseId])
REFERENCES [dbo].[AnoBases] ([AnoBaseId])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_AnoBases_AnoBaseId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Divisao_DivisaoId] FOREIGN KEY([DivisaoId])
REFERENCES [dbo].[Divisao] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Divisao_DivisaoId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Setor_SetorId] FOREIGN KEY([SetorId])
REFERENCES [dbo].[Setor] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Setor_SetorId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Usuarios_SuperiorId] FOREIGN KEY([SuperiorId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Usuarios_SuperiorId]
GO
