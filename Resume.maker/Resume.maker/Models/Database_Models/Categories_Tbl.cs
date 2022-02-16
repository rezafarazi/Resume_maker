using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.maker.Models
{
    public class Categories_Tbl : Database_DB
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public bool enable_flag { get; set; }
        public int top_cat_id { get; set; }
        public bool delete_flag { get; set; }
    }
}