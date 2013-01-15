using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Session
    {
        public int Id { get; set; }
        public List<Player> ActivePlayers { get; set; }
        public DateTime Date { get; set; }
        public List<Court> Courts { get; set; }
    }
}
