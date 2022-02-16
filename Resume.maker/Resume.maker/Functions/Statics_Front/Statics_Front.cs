using Newtonsoft.Json.Linq;
using Resume.maker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Resume.maker.Functions.Statics_Front
{
    public class Statics_Front
    {



        //Get Category Title With Class Start
        public static string Get_Category_Name_From_Class_Value(int User_Id)
        {
            
            using (Database_DB DB = new Database_DB())
            {
                Task<Categories_Tbl> Cat_Tbl_Tsk = new Task<Categories_Tbl>(() => { return (from Categories_Tbl in DB.Categories_Tbl join Users_Tbl in DB.Users_Tbl on Categories_Tbl.id equals Users_Tbl.category_id.id where Users_Tbl.id.Equals(User_Id) select Categories_Tbl).FirstOrDefault(); });
                Cat_Tbl_Tsk.Start();
                Categories_Tbl cat = Cat_Tbl_Tsk.Result;
                if (cat != null)
                {
                    return cat.title.ToString();
                }
            }
            return "";
        }
        //Get Category Title With Class End





        //Get Category Title With Class Start
        public static string Get_Satate_Name_From_Class_Value(int City_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Cites_tbl> Cites_Tbl_Tsk = new Task<Cites_tbl>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.enable_flag.Equals(true) && Cites_tbl.delete_flag.Equals(false) && Cites_tbl.id.Equals(City_Id) select Cites_tbl).FirstOrDefault(); });
                Cites_Tbl_Tsk.Start();
                Cites_tbl cat = Cites_Tbl_Tsk.Result;
                if (cat != null)
                {
                    return cat.city_name.ToString();
                }
            }
            return "";
        }
        //Get Category Title With Class End




        //Get Category Title With Class Start
        public static string[] Get_City_Name_From_Class_Value(int User_Id)
        {
            string []city_array = new string[2];
            using (Database_DB DB = new Database_DB())
            {
                Task<Cites_tbl> Cities_Tbl_Tsk = new Task<Cites_tbl>(()=> { return (from Cites_tbl in DB.Cites_tbl join Users_Tbl in DB.Users_Tbl on Cites_tbl.id equals Users_Tbl.city.id where Cites_tbl.enable_flag.Equals(true) && Cites_tbl.delete_flag.Equals(false) && Users_Tbl.id.Equals(User_Id) select Cites_tbl).FirstOrDefault();  });
                Cities_Tbl_Tsk.Start();
                Cites_tbl cat = Cities_Tbl_Tsk.Result;
                if (cat != null)
                {
                    city_array[0] = cat.city_name;
                    city_array[1] = cat.subcity;
                    return city_array;
                }
            }

            city_array[0] = "";
            city_array[1] = "";
            return city_array;
        }
        //Get Category Title With Class End




        //Get All Categoris Start
        public static List<Categories_Tbl> Get_All_Categories()
        {
            List<Categories_Tbl> All_Categories_List = new List<Categories_Tbl>();
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Categories_Tbl>> List_Categories_Tbl_Tsk = new Task<List<Categories_Tbl>>(()=> { return (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.enable_flag.Equals(true) && Categories_Tbl.delete_flag.Equals(false) select Categories_Tbl).ToList<Categories_Tbl>(); });
                List_Categories_Tbl_Tsk.Start();
                All_Categories_List = List_Categories_Tbl_Tsk.Result;
            }
            return All_Categories_List;
        }
        //Get All Categoris End




        //Get All State Start
        public static List<Cites_tbl> Get_All_States()
        {
            List<Cites_tbl> All_Cities_List = new List<Cites_tbl>();
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Cites_tbl>> Cities_Tbl_Tsk = new Task<List<Cites_tbl>>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.subcity.Equals(null) && Cites_tbl.enable_flag == true select Cites_tbl).ToList<Cites_tbl>(); });
                Cities_Tbl_Tsk.Start();
                All_Cities_List = Cities_Tbl_Tsk.Result;
            }
            return All_Cities_List;
        }
        //Get All State End




        //Get All Cities Start
        public static List<Cites_tbl> Get_All_Cities(int subid)
        {
            List<Cites_tbl> All_Cities_List = new List<Cites_tbl>();
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Cites_tbl>> Cities_Tbl_Tsk = new Task<List<Cites_tbl>>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.subcity.Equals(subid) select Cites_tbl).ToList<Cites_tbl>(); });
                Cities_Tbl_Tsk.Start();
                All_Cities_List = Cities_Tbl_Tsk.Result;
            }
            return All_Cities_List;
        }
        //Get All Cities End





        //Get All Cities Of State Start
        public static JArray Get_Cities_Of_State(string State_Id)
        {
            JArray json_array = new JArray();

            using (Database_DB DB = new Database_DB())
            {
                Task<List<Cites_tbl>> Cities_Tsk = new Task<List<Cites_tbl>>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.subcity == State_Id select Cites_tbl).ToList(); });
                Cities_Tsk.Start();
                List<Cites_tbl> citeis = Cities_Tsk.Result;

                for (int i = 0; i < citeis.Count; i++)
                {
                    Cites_tbl city = citeis[i];
                    JObject json = new JObject();
                    json.Add("id", city.id);
                    json.Add("name", city.city_name);
                    json_array.Add(json);
                }
            }
            
            return json_array;
        }
        //Get All Cities Of State End





        //Get Search Start
        public static JArray Get_Search(string Search)
        {
            JArray json_array = new JArray();
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> Users_Tsk = new Task<List<Users_Tbl>>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.name.Contains(Search) || Users_Tbl.family.Contains(Search) || Users_Tbl.phone.Contains(Search) || Users_Tbl.address.Contains(Search) select Users_Tbl).ToList(); });
                Users_Tsk.Start();
                List<Users_Tbl> Users = Users_Tsk.Result;
                for (int i = 0; i < Users.Count; i++)
                {
                    Users_Tbl User = Users[i];
                    JObject json = new JObject();
                    json.Add("id", User.id);
                    json.Add("img", User.profile_image_address);
                    json.Add("name", User.name);
                    json.Add("family", User.family);
                    json_array.Add(json);
                }
            }
            return json_array;
        }
        //Get Search End





        //Get All Categoris Start
        public static List<Categories_Tbl> Get_All_Categories_Data()
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Categories_Tbl>> Categories_Tsk = new Task<List<Categories_Tbl>>(()=> { return (from Categories_Tbl in DB.Categories_Tbl
                                                                                                                                                   where Categories_Tbl.delete_flag.Equals(false)
                                                                                                                                                   select Categories_Tbl).ToList<Categories_Tbl>();
                });

                Categories_Tsk.Start();
                return Categories_Tsk.Result;
            }
        }
        //Get All Categoris End



            
        //Get All Cities Start
        public static List<Cites_tbl> Get_All_Cities()
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Cites_tbl>> Cities_Tsk = new Task<List<Cites_tbl>>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.delete_flag == false select Cites_tbl).ToList<Cites_tbl>();  });
                Cities_Tsk.Start();
                return Cities_Tsk.Result;
            }
        }
        //Get All Cities End





        //Get City Data Start
        public static JObject Get_City_Data(int Id)
        {
            using(Database_DB DB=new Database_DB())
            {
                Task<Cites_tbl> City_Tsk = new Task<Cites_tbl>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.id.Equals(Id) select Cites_tbl).FirstOrDefault(); });
                City_Tsk.Start();
                Cites_tbl city = City_Tsk.Result;
                JObject json = new JObject();
                json.Add("id", city.id);
                json.Add("name", city.city_name);
                json.Add("status", city.enable_flag);
                return json;
            }
        }
        //Get City Data End





    }
}