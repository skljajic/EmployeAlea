using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Project
    {
        public int id { get; set; }
        public string nazivProjekta { get; set; }
        public string Opis { get; set; }
        public Nullable<bool> status { get; set; }


        // Foreign Key
        public Nullable<int> idNadredjenog { get; set; }
        // Navigation property
        public string NadIme { get; set; }
    }
}