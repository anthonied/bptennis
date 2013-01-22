using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public interface IPlayer
    {
        void InsertPlayer(Player player);
        void UpdatePlayer(Player player);
        void RemovePlayerFromActiveListInSession(int playerId);

    }
}
