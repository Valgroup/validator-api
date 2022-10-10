ALTER TABLE Divisao  WITH CHECK ADD  CONSTRAINT FK_Divisao_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)
ON DELETE CASCADE

ALTER TABLE Divisao CHECK CONSTRAINT FK_Divisao_AnoBases_AnoBaseId

ALTER TABLE Parametro  WITH CHECK ADD  CONSTRAINT FK_Parametro_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)

ALTER TABLE Parametro CHECK CONSTRAINT FK_Parametro_AnoBases_AnoBaseId

ALTER TABLE Planilhas  WITH CHECK ADD  CONSTRAINT FK_Planilhas_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)

ALTER TABLE Planilhas CHECK CONSTRAINT FK_Planilhas_AnoBases_AnoBaseId

ALTER TABLE Processos  WITH CHECK ADD  CONSTRAINT FK_Processos_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)
ON DELETE CASCADE

ALTER TABLE Processos CHECK CONSTRAINT FK_Processos_AnoBases_AnoBaseId

ALTER TABLE Progresso  WITH CHECK ADD  CONSTRAINT FK_Progresso_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)

ALTER TABLE Progresso CHECK CONSTRAINT FK_Progresso_AnoBases_AnoBaseId

ALTER TABLE Progresso  WITH CHECK ADD  CONSTRAINT FK_Progresso_Usuarios_UsuarioId FOREIGN KEY(UsuarioId)
REFERENCES Usuarios (Id)

ALTER TABLE Progresso CHECK CONSTRAINT FK_Progresso_Usuarios_UsuarioId

ALTER TABLE Setor  WITH CHECK ADD  CONSTRAINT FK_Setor_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)
ON DELETE CASCADE

ALTER TABLE Setor CHECK CONSTRAINT FK_Setor_AnoBases_AnoBaseId

ALTER TABLE UsuarioAvaliador  WITH CHECK ADD  CONSTRAINT FK_UsuarioAvaliador_Usuarios_AvaliadorId FOREIGN KEY(AvaliadorId)
REFERENCES Usuarios (Id)

ALTER TABLE UsuarioAvaliador CHECK CONSTRAINT FK_UsuarioAvaliador_Usuarios_AvaliadorId

ALTER TABLE UsuarioAvaliador  WITH CHECK ADD  CONSTRAINT FK_UsuarioAvaliador_Usuarios_UsuarioId FOREIGN KEY(UsuarioId)
REFERENCES Usuarios (Id)

ALTER TABLE UsuarioAvaliador CHECK CONSTRAINT FK_UsuarioAvaliador_Usuarios_UsuarioId

ALTER TABLE Usuarios  WITH CHECK ADD  CONSTRAINT FK_Usuarios_AnoBases_AnoBaseId FOREIGN KEY(AnoBaseId)
REFERENCES AnoBases (AnoBaseId)

ALTER TABLE Usuarios CHECK CONSTRAINT FK_Usuarios_AnoBases_AnoBaseId

ALTER TABLE Usuarios  WITH CHECK ADD  CONSTRAINT FK_Usuarios_Divisao_DivisaoId FOREIGN KEY(DivisaoId)
REFERENCES Divisao (Id)

ALTER TABLE Usuarios CHECK CONSTRAINT FK_Usuarios_Divisao_DivisaoId

ALTER TABLE Usuarios  WITH CHECK ADD  CONSTRAINT FK_Usuarios_Setor_SetorId FOREIGN KEY(SetorId)
REFERENCES Setor (Id)

ALTER TABLE Usuarios CHECK CONSTRAINT FK_Usuarios_Setor_SetorId

ALTER TABLE Usuarios  WITH CHECK ADD  CONSTRAINT FK_Usuarios_Usuarios_SuperiorId FOREIGN KEY(SuperiorId)
REFERENCES Usuarios (Id)

ALTER TABLE Usuarios CHECK CONSTRAINT FK_Usuarios_Usuarios_SuperiorId