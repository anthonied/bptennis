using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Pool
    {
        public List<Player> Players { get; set; }

        
        public Pool()
        {
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}
