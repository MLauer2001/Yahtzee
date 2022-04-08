BEGIN
	DECLARE @UserId_UL UNIQUEIDENTIFIER;
	DECLARE @LobbyId_UL UNIQUEIDENTIFIER;
	DECLARE @ScorecardId_UL UNIQUEIDENTIFIER;

	SELECT @UserId_UL = Id FROM tblUser WHERE Username = 'MLauer';
	SELECT @LobbyId_UL = Id  FROM tblLobby WHERE LobbyName = 'HelloWorld';
	SELECT @ScorecardId_UL FROM tblScorecard WHERE (ADDCODE);

	INSERT INTO dbo.tblUserLobby (Id, UserId, LobbyId, ScorecardId)
	VALUES
	(NEWID(), @UserId_UL, @LobbyId_UL, @ScorecardId_UL)

	SELECT @UserId_UL = Id FROM tblUser WHERE Username = 'RWuest';
	SELECT @LobbyId_UL = Id  FROM tblLobby WHERE LobbyName = 'GameHere';
	SELECT @ScorecardId_UL FROM tblScorecard WHERE (ADDCODE);

	INSERT INTO dbo.tblUserLobby (Id, UserId, LobbyId, ScorecardId)
	VALUES
	(NEWID(), @UserId_UL, @LobbyId_UL, @ScorecardId_UL)

	SELECT @UserId_UL = Id FROM tblUser WHERE Username = 'JSmith';
	SELECT @LobbyId_UL = Id  FROM tblLobby WHERE LobbyName = 'HelloWorld';
	SELECT @ScorecardId_UL FROM tblScorecard WHERE (ADDCODE);

	INSERT INTO dbo.tblUserLobby (Id, UserId, LobbyId, ScorecardId)
	VALUES
	(NEWID(), @UserId_UL, @LobbyId_UL, @ScorecardId_UL)
END