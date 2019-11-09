using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcProjekti
    {
        public int id { get; set; }
        public string nazivProjekta { get; set; }
        public string Opis { get; set; }
        public Nullable<bool> status { get; set; }


        // Foreign Key
        public Nullable<int> idNadredjenog { get; set; }
        // Navigation property
        public string NadIme { get; set; }
        public virtual mvcNadredjeni nad { get; set; }
        
    }
}