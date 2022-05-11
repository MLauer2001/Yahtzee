using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahtzee.BL.Models;

namespace Yahtzee.Web.ViewModels
{
    public class GameVM
    {
        public User User { get; set; }
        public Lobby Lobby { get; set; }
    }
}
