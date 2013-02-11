using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTennis.Domain
{
    public class PlayerGames
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Displayname
        {
            get
            {
                return string.Format("{0}, {1}", Surname, Name);
            }
        }
        public int NumberOfGames { get; set; }  
    }
}
