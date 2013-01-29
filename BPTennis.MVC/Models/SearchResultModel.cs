﻿using BPTennis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPTennis.MVC.Models
{
    public class SearchResultModel
    {
        public List<Player> Players { get; set; }
        public DateTime Date { get; set; }        
    }
}