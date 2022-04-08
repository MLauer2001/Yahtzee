BEGIN
	INSERT INTO dbo.tblLobby(Id, MaxPlayer, LobbyName)
	VALUES
	(NEWID(), 3, 'HelloWorld'),
	(NEWID(), 2, 'TestGame'),
	(NEWID(), 4, 'GameHere')
END