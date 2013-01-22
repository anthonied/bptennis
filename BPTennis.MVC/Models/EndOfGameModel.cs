using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.MVC.Models
{
    public class EndOfGameModel
    {
        public int SessionId { get; set; }
        public int CourtId { get; set; }
        public List<Player> CourtPlayers { get; set; }
    }
}
