using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Pool Pool { get; set; }
        public int TopPlayerOrder { get; set; }
        public int LastPlayerOrder { get; set; }
        public List<Court> Courts { get; set; }

        public SessionModel()
        {
            Pool = new Pool();
        }
    }
}