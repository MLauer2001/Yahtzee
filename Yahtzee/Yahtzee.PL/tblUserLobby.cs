using System;
using System.Collections.Generic;

#nullable disable

namespace Yahtzee.PL
{
    public partial class tblUserLobby
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }
        public Guid ScorecardId { get; set; }
    }
}
