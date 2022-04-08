Begin
	Declare @UserId uniqueidentifier;

	Select @UserId = Id from tblUser where Username = 'MLauer';

	Insert into dbo.tblScorecard (Id, UserId, Aces, Twos, Threes, Fours, Fives, Sixes, 
								  Bonus, ThreeofKind, FourofKind, FullHouse, SmStraight, 
								  LgStraight, Yahtzee, Chance, GrandTotal)
	Values
	(NEWID(), @UserId, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 244),
	(NEWID(), @UserId, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 266),
	(NEWID(), @UserId, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 290)

	Select @UserId = Id from tblUser where Username = 'RWuest'

	Insert into dbo.tblScorecard (Id, UserId, Aces, Twos, Threes, Fours, Fives, Sixes, 
								  Bonus, ThreeofKind, FourofKind, FullHouse, SmStraight, 
								  LgStraight, Yahtzee, Chance, GrandTotal)
	Values 
	(NEWID(), @UserId, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 270),
	(NEWID(), @UserId, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1, 239),
	(NEWID(), @UserId, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 295)
End