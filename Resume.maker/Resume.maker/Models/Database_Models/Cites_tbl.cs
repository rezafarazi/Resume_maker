using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.maker.Models
{
    public class Cites_tbl : Database_DB
    {
        [Key]
        public int id { get; set; }
        public string city_name { get; set; }
        public string subcity { get; set; }
        public bool enable_flag { get; set; }
        public bool delete_flag { get; set; }
    }
}