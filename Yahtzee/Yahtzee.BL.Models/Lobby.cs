using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class Lobby
    {
        public Guid Id { get; set; }
        public int MaxPlayers { get; set; }
        public string LobbyName { get; set; }
    }
}
