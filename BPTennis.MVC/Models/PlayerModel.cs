using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BPTennis.MVC.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        public SelectList Genders
        {
            get
            {
                var items = new List<string>{"Male", "Female"};
                return new SelectList(items, "Male");
            }
        }
    }
}