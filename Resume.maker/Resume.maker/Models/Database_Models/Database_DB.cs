using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resume.maker.Models
{
    public class Database_DB:DbContext
    {
        public Database_DB():base("Database_Connection")
        {

        }

        public DbSet<Users_Tbl> Users_Tbl { get; set; }
        public DbSet<Categories_Tbl> Categories_Tbl { get; set; }
        public DbSet<Cites_tbl> Cites_tbl { get; set; }
        public DbSet<Viewers_tbl> Viewers_tbl { get; set; }
    }
}