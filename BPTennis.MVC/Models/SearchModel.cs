using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class SearchModel
    {
        public Player Player { get; set; }
        public int NumberOfGames { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}