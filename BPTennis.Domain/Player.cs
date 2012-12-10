using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPTennis.Domain
{
    public class Player
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool IsAvailableToPlay { get; set; }

        public Player()
        {
       
        }

        public void AddToPool(Pool pool)
        {
            if (IsAvailableToPlay)
            pool.Player.Add(this);
        }
        public void SendToCourt(Court court)
        {
            if (!court.Full)
             court.Players.Add(this);
        }
        public void RemoveFromPool(Pool pool)
        {
            pool.Player.Remove(this);
        }

        public void SetToNotAvailableToPlay()
        {
            IsAvailableToPlay = false;
        }
        public void SetToAvailableToPlay()
        {
            IsAvailableToPlay = true;
        }
       
    }
}
