using System;
using System.Collections.Generic;

#nullable disable

namespace Yahtzee.PL
{
    public partial class tblScorecard
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Aces { get; set; }
        public bool Twos { get; set; }
        public bool Threes { get; set; }
        public bool Fours { get; set; }
        public bool Fives { get; set; }
        public bool Sixes { get; set; }
        public bool Bonus { get; set; }
        public bool ThreeofKind { get; set; }
        public bool FourofKind { get; set; }
        public bool FullHouse { get; set; }
        public bool SmStraight { get; set; }
        public bool LgStraight { get; set; }
        public bool Yahtzee { get; set; }
        public bool Chance { get; set; }
        public int GrandTotal { get; set; }
    }
}
