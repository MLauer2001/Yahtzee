using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class Scorecard
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
        public bool ThreeOfKind { get; set; }
        public bool FourOfKind { get; set; }
        public bool FullHouse { get; set; }
        public bool SmStraight { get; set; }
        public bool LgStraight { get; set; }
        public bool Yahtzee { get; set; }
        public bool Chance { get; set; }
        public int GrandTotal { get; set; }
    }
}
