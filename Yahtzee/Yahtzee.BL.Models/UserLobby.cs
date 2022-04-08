using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class UserLobby
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }
        public Guid ScorecardId { get; set; }
    }
}
