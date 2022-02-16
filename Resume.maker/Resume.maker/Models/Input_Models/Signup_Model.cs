using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.maker.Models.Input_Models
{
    public class Signup_Model
    {
        public string name { get; set; }
        public string family { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}