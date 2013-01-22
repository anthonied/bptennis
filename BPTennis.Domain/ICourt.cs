using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public interface ICourt
    {
        void RemovePlayersFromCourt(int courtId);
        void AddPlayersToSessionActivePlayers(int sessionId, List<Player> courtPlayers);
    }
}
