using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class CourtModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Court Name is required")]
        [Display(Name = "Name")]
        public string CourtName { get; set; }
    }
}