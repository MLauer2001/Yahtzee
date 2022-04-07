ALTER TABLE [dbo].[tblActivation]
	ADD CONSTRAINT [fk_tblActivation_LobbyId]
	FOREIGN KEY (LobbyId)
	REFERENCES [tblLobby] (Id)
