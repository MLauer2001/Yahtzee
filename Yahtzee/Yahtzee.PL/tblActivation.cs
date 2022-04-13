using System;
using System.Collections.Generic;

#nullable disable

namespace Yahtzee.PL
{
    public partial class tblActivation
    {
        public Guid Id { get; set; }
        public Guid LobbyId { get; set; }
        public string KeyCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
