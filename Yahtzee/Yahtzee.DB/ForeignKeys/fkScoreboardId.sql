ALTER TABLE [dbo].[tblUserLobby]
	ADD CONSTRAINT [fk_tblScorecard_ScorecardId]
	FOREIGN KEY (ScorecardId)
	REFERENCES [tblScorecard] (Id)
