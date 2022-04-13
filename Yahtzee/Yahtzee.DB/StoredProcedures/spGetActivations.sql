CREATE PROCEDURE [dbo].[spGetActivations]
AS
	SELECT a.Id,
	a.LobbyId, a.KeyCode, a.StartDate, a.EndDate
	FROM tblActivation a
	INNER JOIN tblLobby l on a.LobbyId = l.Id
RETURN 0