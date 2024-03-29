USE [Validator_RH]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/10/2022 11:41:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[AnoBases]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Divisao]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Parametro]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Planilhas]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Processos]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Progresso]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Setor]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[UsuarioAvaliador]    Script Date: 10/10/2022 11:41:28 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 10/10/2022 11:41:28 ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220825180300_V_1_0_0', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220826164446_V_1_0_1', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220826200213_V_1_0_2', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220829131230_V_1_0_3', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220830140949_V_1_0_4', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220830200339_V_1_0_5', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220831113506_V_1_0_6', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220905114602_V_1_0_7', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220906202407_V_1_0_8', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220909135048_V_1_0_9', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220912163523_V_1_0_10', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220912200250_V_1_0_11', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220913182800_V_1_0_12', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220914164716_V_1_0_13', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220926122508_V_1_0_14', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220926140051_V_1_0_15', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220928112013_V_1_0_16', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221005143511_V_1_0_17', N'6.0.8')
GO
INSERT [dbo].[AnoBases] ([AnoBaseId], [Ano], [Deleted]) VALUES (N'3c099378-2e67-480a-b0bb-900cb4b268ec', 2022, 0)
GO
INSERT [dbo].[Divisao] ([Id], [Nome], [AnoBaseId]) VALUES (N'e1bd2168-b1c7-470e-a16d-1484952b5aa0', N'SP1', N'3c099378-2e67-480a-b0bb-900cb4b268ec')
GO
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'49b75ab0-3f0b-4b9e-ad32-26f0c22235ce', N'MG1', N'Filippo de Gara Geronimi', N'', N'Diretoria', N'Diretoria', CAST(N'2013-04-08T00:00:00.0000000' AS DateTime2), N'Diretoria', NULL, N'', N'', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'', N'34677443890', 0, N'Email é obrigatório | Superior Imediato é obrigatório | E-mail do Superior é obrigatório', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'fd7150c5-dec7-46f0-b9d9-919d004a73cc', N'SP3', N'Carlo Bergamaschi', N'carlo@valgroupco.com', N'Diretoria', N'Diretoria', CAST(N'2012-07-27T00:00:00.0000000' AS DateTime2), N'', NULL, N'', N'', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'x', N'31976923816', 0, N'Centro de Custo é obrigatório', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'0c087454-2f64-4aee-bea0-9a9b732de0b6', N'SP3', N'Federico de Gara Geronimi', N'federico.geronimi@valgroupco.com', N'Diretoria', N'Diretoria', CAST(N'2019-05-22T00:00:00.0000000' AS DateTime2), N'', NULL, N'Carlo Bergamaschi', N'carlo@valgroupco.com', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'', N'34677442819', 0, N'Centro de Custo é obrigatório', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'a04cb9b4-9868-4467-9bbe-9f602c7e21ea', N'TI', N'Paola de Gara Geronimi', N'paola.degara@valgroupco.com', N'Diretoria', N'Diretoria', NULL, N'Diretoria', NULL, N'', N'', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'x', N'12188253833', 1, N'', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'89598bf4-6316-4852-884f-a05279fbcdc6', N'', N'Laercio Sponchiado', N'laercio.sponchiado@valgroupco.com', N'Diretoria', N'Diretoria', CAST(N'2013-09-02T00:00:00.0000000' AS DateTime2), N'Diretoria', NULL, N'Carlo Bergamaschi', N'carlo@valgroupco.com', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'', N'07484242888', 0, N'Unidade é obrigatório', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'3ed96563-0a34-4d0b-93d2-cd5005580b1c', N'', N'', N'sara.geronimi@valgroupco.com', N'Diretoria', N'Diretoria', NULL, N'Diretoria', NULL, N'', N'', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'x', N'34677447888', 0, N'Unidade é obrigatório | Nome é obrigatório', N'')
INSERT [dbo].[Planilhas] ([Id], [Unidade], [Nome], [Email], [Cargo], [Nivel], [DataAdmissao], [CentroCusto], [NumeroCentroCusto], [SuperiorImediato], [EmailSuperior], [Deleted], [AnoBaseId], [Direcao], [CPF], [EhValido], [Validacoes], [GestorCorporativo]) VALUES (N'7cdc92d7-4c38-4962-827b-d0583d1bcc44', N'SP1', N'Vania Auxiliadora Alves da Silva Ferraz', N'vania.ferraz@valgroupco.com', N'', N'', CAST(N'2005-03-15T00:00:00.0000000' AS DateTime2), N'Diretoria Administrativa', NULL, N'Paola de Gara Geronimi', N'paola.degara@valgroupco.com', 0, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'', N'12189483883', 0, N'Nivel é obrigatório', N'')
GO
INSERT [dbo].[Processos] ([Id], [Situacao], [DhInicio], [DhFim], [AnoBaseId]) VALUES (N'c32a7602-ca67-4412-96af-c4f70a71f1e5', 0, NULL, NULL, N'3c099378-2e67-480a-b0bb-900cb4b268ec')
GO
INSERT [dbo].[Setor] ([Id], [Nome], [AnoBaseId]) VALUES (N'8e81fd98-ae2b-4b3b-b72d-4da2e80c5c1a', N'RH', N'3c099378-2e67-480a-b0bb-900cb4b268ec')
GO
INSERT [dbo].[Usuarios] ([Id], [AzureId], [Perfil], [Nome], [Email], [EmailSuperior], [EhDiretor], [Deleted], [SuperiorId], [AnoBaseId], [DivisaoId], [SetorId], [Cargo], [Senha], [Documento], [Ativo], [EhGestor]) VALUES (N'dd2fd0ec-f7ba-4252-8f6a-82f54ffbfb66', N'f2d79d27-df55-4b64-8023-711724b62db2', 1, N'Franciele Lourenço Vicente', N'franciele.vicente@valgroupco.com', N'alfredo.gomes@valgroupco.com', 0, 0, NULL, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'e1bd2168-b1c7-470e-a16d-1484952b5aa0', N'8e81fd98-ae2b-4b3b-b72d-4da2e80c5c1a', N'', N'df6ca5ed8db2e02abc044fc517df0601', N'20053139046', 1, 0)
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
