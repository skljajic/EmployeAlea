using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;



namespace MVC.Models

{

    public class Login

    {

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Ime { get; set; }
        public string level { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> nadredjen { get; set; }

    }

}