using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Court
    {
        public string CourtName { get; set; }
        public List<Player> Players { get; set; }
        public bool Full
        {
            get
            {
                return Players.Count >= 4 ? true : false;
            }
        }

        public void FinishGame()
        {
            Players = new List<Player>();
        }
        public Court()
        {
            Players = new List<Player>();
        }



    }
}
