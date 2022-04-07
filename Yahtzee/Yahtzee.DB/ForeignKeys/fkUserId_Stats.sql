ALTER TABLE [dbo].[tblStats]
	ADD CONSTRAINT [fk_tblStats_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id)
