using System;
using System.Collections.Generic;

#nullable disable

namespace Yahtzee.PL
{
    public partial class tblLobby
    {
        public Guid Id { get; set; }
        public int MaxPlayer { get; set; }
        public string LobbyName { get; set; }
    }
}
