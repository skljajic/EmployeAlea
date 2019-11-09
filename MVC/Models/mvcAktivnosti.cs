using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcAktivnosti
    {
        public int id { get; set; }
        public string Naslov { get; set; }
        public string opis { get; set; }
        public Nullable<DateTime> start { get; set; }
        public Nullable<DateTime> startRadnik { get; set; }
        public Nullable<DateTime> end { get; set; }

        public int idProjekta { get; set; }
        public string ProjIme { get; set; }
        public virtual mvcProjekti MvcProjekti { get; set; }

        public int idNadredjenog { get; set; }
        public string NadrIme { get; set; }
        public virtual mvcNadredjeni MvcNadredjeni  { get; set; }

        public int idRadnika { get; set; }
        public string RadnikIme { get; set; }
        public virtual mvcZaposleni MvcZaposleni { get; set; }


    }
}