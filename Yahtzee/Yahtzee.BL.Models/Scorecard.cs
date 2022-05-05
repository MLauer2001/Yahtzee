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
        public string Username { get; set; }
        public int Aces { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixes { get; set; }
        public int Bonus { get; set; }
        public int ThreeOfKind { get; set; }
        public int FourOfKind { get; set; }
        public int FullHouse { get; set; }
        public int SmStraight { get; set; }
        public int LgStraight { get; set; }
        public int Yahtzee { get; set; }
        public int Chance { get; set; }
        public int GrandTotal { get; set; }
    }
}
