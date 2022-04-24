using System;
using System.Collections.Generic;

#nullable disable

namespace Yahtzee.PL
{
    public partial class tblScorecard
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Aces { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixes { get; set; }
        public int Bonus { get; set; }
        public int ThreeofKind { get; set; }
        public int FourofKind { get; set; }
        public int FullHouse { get; set; }
        public int SmStraight { get; set; }
        public int LgStraight { get; set; }
        public int Yahtzee { get; set; }
        public int Chance { get; set; }
        public int GrandTotal { get; set; }
    }
}
