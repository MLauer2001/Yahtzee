using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahtzee.BL.Models;

namespace Yahtzee.Web.ViewModels
{
    public class PlayerVM
    {
        public User User { get; set; }
        public List<Scorecard> Scorecards { get; set; }
    }
}
