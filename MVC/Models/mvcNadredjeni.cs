using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcNadredjeni
    {
        public int id { get; set; }
        public string Ime { get; set; }

        public virtual ICollection<mvcZaposleni> Zaposleni { get; set; }
    }
}