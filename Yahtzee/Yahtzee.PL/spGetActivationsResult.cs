using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.PL
{
    public class spGetActivationsResult
    {
        public Guid Id { get; set; }
        public Guid LobbyId { get; set; }
        public string KeyCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
