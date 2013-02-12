using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Player
    {
        public IPlayer Repository;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool AvailableToPlay { get; set; }
        public string Status { get; set; }
        public int? PlayerOrder { get; set; }
        public string DisplayName
        {
            get
            {
                return string.Format("{0} {1}", Name, Surname);
            }
        }
        public Player()
        {
       
        }

        public void AddToPool(Pool pool)
        {
            if (AvailableToPlay)
            pool.Players.Add(this);
        }
        public void SendToCourt(Court court, Session session)
        {
            if (!court.Full)
             court.Players.Add(this);

            session.ActivePlayers.Remove(session.ActivePlayers.Find(player => player.Id == this.Id));
            Repository.RemovePlayerFromActiveListInSession(this.Id);
        }
        public void RemoveFromPool(Pool pool)
        {
            pool.Players.Remove(this);
        }

        public void SetToNotAvailableToPlay()
        {
            AvailableToPlay = false;
        }
        public void SetToAvailableToPlay()
        {
            AvailableToPlay = true;
        }
       
    }
}
