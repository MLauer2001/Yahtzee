BEGIN
	DECLARE @LobbyId_Act UNIQUEIDENTIFIER;

	SELECT @LobbyId_Act = Id FROM tblLobby WHERE LobbyName = 'HelloWorld';

	INSERT INTO dbo.tblActivation (Id, LobbyId, KeyCode, StartDate, EndDate)
	VALUES
	(NEWID(), @LobbyId_Act, 'star123456', '2022-01-01', '2022-12-12')

	SELECT @LobbyId_Act = Id FROM tblLobby WHERE LobbyName = 'TestGame';

	INSERT INTO dbo.tblActivation (Id, LobbyId, KeyCode, StartDate, EndDate)
	VALUES
	(NEWID(), @LobbyId_Act, 'sun1234567', '2022-02-02', '2022-12-13')

	SELECT @LobbyId_Act = Id FROM tblLobby WHERE LobbyName = 'GameHere';

	INSERT INTO dbo.tblActivation (Id, LobbyId, KeyCode, StartDate, EndDate)
	VALUES
	(NEWID(), @LobbyId_Act, 'moon123456', '2022-03-03', '2022-12-14')

END