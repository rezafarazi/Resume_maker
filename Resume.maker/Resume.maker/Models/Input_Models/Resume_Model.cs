using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.maker.Models.Input_Models
{
    public class Resume_Model
    {
        public HttpPostedFileBase profile_image { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string phone { get; set; }
        public string category_id { get; set; }
        public string state_id { get; set; }
        public string city_id { get; set; }
        public string work_years { get; set; }
        public string brith_date { get; set; }
        public string address { get; set; }
        public string resume_json { get; set; }
        public string certificates_json { get; set; }
    }
}