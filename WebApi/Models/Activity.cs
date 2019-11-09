using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Activity
    {

        public int id { get; set; }
        public string Naslov { get; set; }
        public string opis { get; set; }
        public Nullable<DateTime> start { get; set; }
        public Nullable<DateTime> startRadnik { get; set; }
        public Nullable<DateTime> end { get; set; }
        
        //public Nullable<int> idProjekta { get; set; }
        //public string ProjIme { get; set; }

        public Nullable<int> idNadredjenog { get; set; }
        public string NadrIme { get; set; }

        public Nullable<int> idRadnika { get; set; }
        public string RadnikIme { get; set; }

        public Nullable<int> idProjekta { get; set; }
        public string ProjIme { get; set; }
    }
}