using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Court
    {
        public ICourt Repository;
        public int Id { get; set; }
        public string CourtName { get; set; }
        public List<Player> Players { get; set; }
        public bool Full
        {
            get
            {
                return Players.Count >= 4 ? true : false;
            }
        }

        public Court()
        {
            Players = new List<Player>();
        }

        public void FinishGame(int sessionId)
        {            
            Repository.AddPlayersToSessionActivePlayers(sessionId, Players);
            Players = new List<Player>();
            Repository.RemovePlayersFromCourt(this.Id);            
        }
        
    }
}
