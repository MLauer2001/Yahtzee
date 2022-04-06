CREATE TABLE [dbo].[tblScorecard]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [GameNo] INT NOT NULL, 
    [Aces] BIT NOT NULL, 
    [Twos] BIT NOT NULL, 
    [Threes] BIT NOT NULL, 
    [Fours] BIT NOT NULL, 
    [Fives] BIT NOT NULL, 
    [Sixes] BIT NOT NULL, 
    [Bonus] BIT NOT NULL, 
    [3ofKind] BIT NOT NULL, 
    [4ofKind] BIT NOT NULL, 
    [FullHouse] BIT NOT NULL, 
    [SmStraight] BIT NOT NULL, 
    [LgStraight] BIT NOT NULL, 
    [Yahtzee] BIT NOT NULL, 
    [Chance] BIT NOT NULL, 
    [GrandTotal] INT NOT NULL
)
