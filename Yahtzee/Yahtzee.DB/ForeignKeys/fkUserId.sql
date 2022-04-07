ALTER TABLE [dbo].[tblScorecard]
	ADD CONSTRAINT [fk_tblScorecard_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id)
