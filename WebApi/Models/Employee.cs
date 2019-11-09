using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Ime { get; set; }
        public string level { get; set; }
        public Nullable<bool> status { get; set; }


        // Foreign Key
        public Nullable<int> nadredjen { get; set; }
        // Navigation property
        public string NadIme  { get; set; }
    }
}