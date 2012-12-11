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

        public Pool Pool { get; set; }

        public SessionModel()
        {
            Pool = new Pool();
        }
    }
}