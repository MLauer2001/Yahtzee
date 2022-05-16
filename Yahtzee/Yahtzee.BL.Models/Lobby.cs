using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class Lobby
    {
        public Guid Id { get; set; }
        [DisplayName("Maximum Players")]
        public int MaxPlayers { get; set; }
        [DisplayName("Lobby Name")]
        public string LobbyName { get; set; }
    }
}
