using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Pool
    {
        public List<Player> Player { get; set; }

        public Pool()
        {
            Player = new List<Player>();
        }
    }
}
