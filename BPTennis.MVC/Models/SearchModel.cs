using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class SearchModel
    {
        public DateTime Date { get; set; }
        public int SessionId { get; set; }
        public int CourtId { get; set; }
        public int PlayerId { get; set; }
    }
}