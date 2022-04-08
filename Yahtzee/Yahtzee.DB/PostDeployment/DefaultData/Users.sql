BEGIN
	INSERT INTO dbo.tblUser(Id, FirstName, LastName, Email, Username, Password)
	VALUES
	(NEWID(), 'Matthew', 'Lauer', 'ML@gmail.com', 'MLauer', 'lauerm'),
	(NEWID(), 'Riley', 'Wuest', 'RW@gmail.com', 'RWuest', 'wuestr'),
	(NEWID(), 'John', 'Smith', 'smithj@gmail.com', 'JSmith', 'smithj')
END