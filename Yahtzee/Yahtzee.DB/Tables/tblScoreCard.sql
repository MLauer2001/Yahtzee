CREATE TABLE [dbo].[tblScorecard]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Aces] INT NOT NULL, 
    [Twos] INT NOT NULL, 
    [Threes] INT NOT NULL, 
    [Fours] INT NOT NULL, 
    [Fives] INT NOT NULL, 
    [Sixes] INT NOT NULL, 
    [Bonus] INT NOT NULL, 
    [ThreeofKind] INT NOT NULL, 
    [FourofKind] INT NOT NULL, 
    [FullHouse] INT NOT NULL, 
    [SmStraight] INT NOT NULL, 
    [LgStraight] INT NOT NULL, 
    [Yahtzee] INT NOT NULL, 
    [Chance] INT NOT NULL, 
    [GrandTotal] INT NOT NULL
)
