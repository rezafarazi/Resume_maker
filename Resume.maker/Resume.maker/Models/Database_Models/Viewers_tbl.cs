using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.maker.Models
{
    public class Viewers_tbl : Database_DB
    {
        [Key]
        public int id { get; set; }
        public Users_Tbl user_id { get; set; }
        public Users_Tbl user_viewer { get; set; }
        public string date_time { get; set; }
    }
}