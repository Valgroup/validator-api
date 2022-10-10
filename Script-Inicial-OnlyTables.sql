CREATE TABLE __EFMigrationsHistory(
	MigrationId nvarchar(150) NOT NULL PRIMARY KEY,
	ProductVersion nvarchar(32) NOT NULL
)

CREATE TABLE AnoBases(
	AnoBaseId uniqueidentifier NOT NULL PRIMARY KEY,
	Ano int NOT NULL,
	Deleted bit NOT NULL
)


CREATE TABLE Divisao(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Nome nvarchar(20) NOT NULL,
	AnoBaseId uniqueidentifier NOT NULL
)


CREATE TABLE Parametro(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	QtdeSugestaoMin int NOT NULL,
	QtdeSugestaoMax int NOT NULL,
	QtdeAvaliador int NOT NULL,
	AnoBaseId uniqueidentifier NOT NULL,
	DhFinalizacao datetime2(7) NOT NULL,
	QtdDiaFinaliza int NOT NULL)


CREATE TABLE Planilhas(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Unidade nvarchar(30) NULL,
	Nome nvarchar(180) NULL,
	Email nvarchar(120) NULL,
	Cargo nvarchar(60) NULL,
	Nivel nvarchar(60) NULL,
	DataAdmissao datetime2(7) NULL,
	CentroCusto nvarchar(60) NULL,
	NumeroCentroCusto nvarchar(60) NULL,
	SuperiorImediato nvarchar(180) NULL,
	EmailSuperior nvarchar(180) NULL,
	Deleted bit NOT NULL,
	AnoBaseId uniqueidentifier NOT NULL,
	Direcao nvarchar(120) NULL,
	CPF nvarchar(20) NULL,
	EhValido bit NOT NULL,
	Validacoes nvarchar(max) NULL,
	GestorCorporativo nvarchar(120) NULL
 )


CREATE TABLE Processos(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Situacao int NOT NULL,
	DhInicio datetime2(7) NULL,
	DhFim datetime2(7) NULL,
	AnoBaseId uniqueidentifier NOT NULL
 )


CREATE TABLE Progresso(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	UsuarioId uniqueidentifier NOT NULL,
	Status int NOT NULL,
	AnoBaseId uniqueidentifier NOT NULL )


CREATE TABLE Setor(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Nome nvarchar(60) NOT NULL,
	AnoBaseId uniqueidentifier NOT NULL
 )


CREATE TABLE UsuarioAvaliador(
	UsuarioId uniqueidentifier NOT NULL PRIMARY KEY,
	AvaliadorId uniqueidentifier NOT NULL,
	Status int NOT NULL,
	Id uniqueidentifier NOT NULL,
	DataHora datetime2(7) NOT NULL )


CREATE TABLE Usuarios(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	AzureId uniqueidentifier NOT NULL,
	Perfil int NOT NULL,
	Nome nvarchar(120) NOT NULL,
	Email nvarchar(120) NOT NULL,
	EmailSuperior nvarchar(120) NULL,
	EhDiretor bit NOT NULL,
	Deleted bit NOT NULL,
	SuperiorId uniqueidentifier NULL,
	AnoBaseId uniqueidentifier NOT NULL,
	DivisaoId uniqueidentifier NULL,
	SetorId uniqueidentifier NULL,
	Cargo nvarchar(30) NULL,
	Senha nvarchar(180) NOT NULL,
	Documento nvarchar(30) NULL,
	Ativo bit NOT NULL,
	EhGestor bit NOT NULL )

GO
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20220825180300_V_1_0_0', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220826164446_V_1_0_1', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220826200213_V_1_0_2', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220829131230_V_1_0_3', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220830140949_V_1_0_4', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220830200339_V_1_0_5', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220831113506_V_1_0_6', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220905114602_V_1_0_7', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220906202407_V_1_0_8', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220909135048_V_1_0_9', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220912163523_V_1_0_10', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220912200250_V_1_0_11', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220913182800_V_1_0_12', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220914164716_V_1_0_13', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220926122508_V_1_0_14', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220926140051_V_1_0_15', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20220928112013_V_1_0_16', N'6.0.8')
INSERT __EFMigrationsHistory (MigrationId, ProductVersion) VALUES (N'20221005143511_V_1_0_17', N'6.0.8')

INSERT AnoBases (AnoBaseId, Ano, Deleted) VALUES (N'3c099378-2e67-480a-b0bb-900cb4b268ec', 2022, 0)



INSERT Usuarios (Id, AzureId, Perfil, Nome, Email, EmailSuperior, EhDiretor, Deleted, SuperiorId, AnoBaseId, DivisaoId, SetorId, Cargo, Senha, Documento, Ativo, EhGestor) VALUES (N'dd2fd0ec-f7ba-4252-8f6a-82f54ffbfb66', N'f2d79d27-df55-4b64-8023-711724b62db2', 1, N'Franciele Lourenço Vicente', N'franciele.vicente@valgroupco.com', N'alfredo.gomes@valgroupco.com', 0, 0, NULL, N'3c099378-2e67-480a-b0bb-900cb4b268ec', N'e1bd2168-b1c7-470e-a16d-1484952b5aa0', N'8e81fd98-ae2b-4b3b-b72d-4da2e80c5c1a', N'', N'df6ca5ed8db2e02abc044fc517df0601', N'20053139046', 1, 0)