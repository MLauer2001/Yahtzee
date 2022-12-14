using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class Die
    {
        public Die() 
        {
            this.Value = 6;
            this.Hold = false;
        }

        public int Value { get; set; }
        public bool Hold { get; set; }

        public int Compare(Die other)
        {
            if(this.Value == other.Value)
                return 0;
            if (this.Value == other.Value)
                return 1;
            return -1;
        }
    }
}
