using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.maker.Models
{
    public class Users_Tbl : Database_DB
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string login_token { get; set; }
        public string date { get; set; }
        public string brith_date { get; set; }
        public bool enable_flag { get; set; }
        public string resume_json { get; set; }
        public string certificates_json { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public Cites_tbl city { get; set; }
        public string user_Type { get; set; }
        public int work_years { get; set; }
        public string last_edit_date { get; set; }
        public string profile_image_address { get; set; }
        public Categories_Tbl category_id { get; set; }
    }
}