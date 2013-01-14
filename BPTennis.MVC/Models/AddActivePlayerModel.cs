using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class AddActivePlayerModel
    {
        public List<Player> AvailablePlayers { get; set; }
        public int SessionId { get; set; }
    }
}