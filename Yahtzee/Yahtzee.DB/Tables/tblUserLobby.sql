﻿CREATE TABLE [dbo].[tblUserLobby]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [LobbyId] UNIQUEIDENTIFIER NOT NULL
)
