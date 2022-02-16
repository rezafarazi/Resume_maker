using Newtonsoft.Json.Linq;
using Resume.maker.Models;
using Resume.maker.Models.Input_Models;
using Resume.maker.Models.Output_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Resume.maker.Functions.Statics
{
    public class Static
    {



        //Get Setup App Start
        public static void GetSetup_App()
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    var Count_Of_Categories = (from Categories_Tbl in DB.Categories_Tbl select Categories_Tbl);
                    if (Count_Of_Categories.Count() <= 0)
                    {
                        Categories_Tbl category = new Categories_Tbl();
                        category.title = "هیچکدام";
                        category.enable_flag = true;
                        category.delete_flag = false;
                        DB.Categories_Tbl.Add(category);
                    }

                    var Count_Of_cities = (from Cites_tbl in DB.Cites_tbl select Cites_tbl);
                    if (Count_Of_cities.Count() <= 0)
                    {
                        Cites_tbl city = new Cites_tbl();
                        city.city_name = "ندارد";
                        city.delete_flag = false;
                        city.enable_flag = true;
                        DB.Cites_tbl.Add(city);
                    }

                    DB.SaveChanges();
                }

            }).Start();
            
        }
        //Get Setup App End





        //Get Change Password Start
        public static void Get_Chnage_Password(User_Model User,string NewPassword)
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == User.id select Users_Tbl).FirstOrDefault();
                    user.password = Hasher_Lib.Hash.SHA_256(NewPassword);
                    DB.SaveChanges();
                }

            });
        }
        //Get Change Password End






        //Set Admin Frist User Start
        public static void SetAdmin_Frist_User()
        {
            new Task(()=> {
                using (Database_DB DB = new Database_DB())
                {
                    Users_Tbl user = (from Users_Tbl in DB.Users_Tbl select Users_Tbl).First();
                    user.user_Type = "ADMIN";
                    user.login_token = Hasher_Lib.Hash.SHA_256(user.username + user.password + PersianDateTime.Now.ToString());
                    DB.SaveChanges();
                }
            }).Start();
            
        }
        //Set Admin Frist User Start




        //Signup New User Function Start
        public static string Signup(Signup_Model User)
        {
            if (Get_Check_Exist_User(User.username,User.name,User.family))
            {
                using (Database_DB DB = new Database_DB())
                {
                    Task<string> User_Token = new Task<string>(() => {

                        Users_Tbl N_User = new Users_Tbl();

                        N_User.name = User.name;
                        N_User.family = User.family;
                        N_User.username = User.username;
                        N_User.password = Hasher_Lib.Hash.SHA_256(User.password);
                        N_User.user_Type = "USER";
                        N_User.enable_flag = true;
                        N_User.category_id = (from Categories_Tbl in DB.Categories_Tbl select Categories_Tbl).First();
                        N_User.city = (from Cites_tbl in DB.Cites_tbl select Cites_tbl).First();
                        N_User.date = PersianDateTime.Now.ToString();
                        N_User.last_edit_date = PersianDateTime.Now.ToString();
                        N_User.login_token = Hasher_Lib.Hash.SHA_256(User.username + User.password + PersianDateTime.Now.ToString());

                        DB.Users_Tbl.Add(N_User);
                        DB.SaveChanges();
                        return Hasher_Lib.Hash.SHA_256(User.username + User.password + PersianDateTime.Now.ToString());

                    });
                    User_Token.Start();
                    return User_Token.Result;
                    
                }
            }
            else
            {
                return "";
            }
        }
        //Signup New User Function End







        //Get Check Exist User Start
        public static bool Get_Check_Exist_User(string Username,string Name,string Family)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> User_Tsk = new Task<List<Users_Tbl>>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.username.Equals(Username) select Users_Tbl).ToList(); });
                User_Tsk.Start();
                var Users = User_Tsk.Result;
                if (Users.Count()>0)
                {
                    return false;
                }
            }
            return true;
        }
        //Get Check Exist User End






        //Get Check User Is Login With Token
        public static bool Get_Check_Login_User_With_Login_Token(string Token)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> User_Tsk = new Task<List<Users_Tbl>>(()=> {
                    return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.enable_flag.Equals(true) && Users_Tbl.login_token.Equals(Token) select Users_Tbl).ToList();
                });
                User_Tsk.Start();
                List<Users_Tbl> user = User_Tsk.Result;
                if (user.Count()>0)
                {
                    return true;
                }
                return false;
            }
        }
        //Get Check User Is Login With End




        //Get User Login Token From Username And Password Start
        public static async Task<string> Get_User_Login_Token(Login_Model User)
        {
            using (Database_DB DB = new Database_DB())
            {
                string Password = Hasher_Lib.Hash.SHA_256(User.password);

                Task<Users_Tbl> User_Tbl_Tsk = new Task<Users_Tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.enable_flag.Equals(true) && Users_Tbl.username.Equals(User.username) && Users_Tbl.password.Equals(Password) select Users_Tbl).FirstOrDefault();  });
                User_Tbl_Tsk.Start();
                Users_Tbl user_login = User_Tbl_Tsk.Result;

                if(user_login!=null)
                {
                    return user_login.login_token;
                }
            }
            return "";
        }
        //Get User Login Token From Username And Password End






        //Get User Data Start
        public static Users_Tbl Get_User_Data(string Login_Token)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Users_Tbl> User_Tsk = new Task<Users_Tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.enable_flag.Equals(true) && Users_Tbl.login_token.Equals(Login_Token) select Users_Tbl).FirstOrDefault(); });
                User_Tsk.Start();
                Users_Tbl User = User_Tsk.Result;
                return User;
            }
        }
        //Get User Data End







        //Get Edit User Resume Start
        public static void Get_Edit_User_Resume(User_Model User,Resume_Model Resume,string Profile_Image_Address)
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id.Equals(User.id) select Users_Tbl).FirstOrDefault();
                    int City_Id = (Resume.city_id != null) ? int.Parse(Resume.city_id) : 0;
                    int cat_id = int.Parse(Resume.category_id);
                    Categories_Tbl category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id == cat_id select Categories_Tbl).FirstOrDefault();
                    user.name = Resume.name;
                    user.family = Resume.family;
                    user.address = Resume.address;
                    user.brith_date = Resume.brith_date;
                    user.work_years = (Resume.work_years != null) ? int.Parse(Resume.work_years) : 0;
                    user.city = (from Cites_tbl in DB.Cites_tbl where Cites_tbl.id == City_Id select Cites_tbl).FirstOrDefault();
                    user.phone = Resume.phone;
                    user.resume_json = Resume.resume_json;
                    user.certificates_json = Resume.certificates_json;
                    user.last_edit_date = PersianDateTime.Now.ToString();
                    user.category_id = category;

                    if (!Profile_Image_Address.Equals(string.Empty))
                        user.profile_image_address = Profile_Image_Address;

                    DB.SaveChanges();
                }

            }).Start();
        }
        //Get Edit User Resume End







        //Get User From ID Start
        public static User_Model Get_User_From_Id(int Id, User_Model UserViewer)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Users_Tbl> User_Tsk = new Task<Users_Tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == Id select Users_Tbl).FirstOrDefault(); });
                User_Tsk.Start();
                Users_Tbl user = User_Tsk.Result;
                User_Model User = new User_Model();

                User.id = user.id;
                User.name = user.name;
                User.family = user.family;
                User.username = user.username;
                User.password = user.password;
                User.login_token = user.login_token;
                User.date = user.date;
                User.brith_date = user.brith_date;
                User.enable_flag = user.enable_flag;
                User.last_edit_date = user.last_edit_date;
                User.profile_image_address = user.profile_image_address;
                User.user_Type = user.user_Type;
                User.work_years = user.work_years;
                User.phone = user.phone;
                User.city = user.city;
                User.address = user.address;
                User.certificates_json = user.certificates_json;
                User.resume_json = user.resume_json;
                User.category_id = user.category_id;

                Viewers_tbl view = new Viewers_tbl();
                view.user_id = user;
                view.user_viewer =(from Users_Tbl in DB.Users_Tbl where Users_Tbl.id==UserViewer.id select Users_Tbl).FirstOrDefault();
                view.date_time = PersianDateTime.Now.ToString();

                DB.Viewers_tbl.Add(view);
                DB.SaveChanges();
                return User;
            }
        }
        //Get User From ID End








        //Get New Category Start
        public static void Add_New_Category(string New_Name)
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    Categories_Tbl Catgory = new Categories_Tbl();
                    Catgory.title = New_Name;
                    Catgory.delete_flag = false;
                    Catgory.enable_flag = true;
                    DB.Categories_Tbl.Add(Catgory);
                    DB.SaveChanges();
                }

            }).Start();
            
        }
        //Get New Category End
        



        //Get Delete Category Start
        public static void Get_Delete_Category(int Category_Id)
        {
            new Task(()=> {
                using (Database_DB DB = new Database_DB())
                {
                    Categories_Tbl Category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id.Equals(Category_Id) select Categories_Tbl).FirstOrDefault();
                    Category.delete_flag = true;
                    DB.SaveChanges();
                }
            }).Start();
        }
        //Get Delete Category End
        




        //Get Category Start
        public static JObject Get_Category(int Category_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Categories_Tbl> Category_Tsk = new Task<Categories_Tbl>(()=> { return (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id.Equals(Category_Id) select Categories_Tbl).FirstOrDefault(); });
                Category_Tsk.Start();
                Categories_Tbl Category = Category_Tsk.Result;
                JObject json = new JObject();
                json.Add("id",Category.id);
                json.Add("title", Category.title);
                json.Add("status", Category.enable_flag);
                return json;
            }
        }
        //Get Category End






        //Get Edit Category Start
        public static void Get_Edit_Category(int Id,string Title,string status)
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    Categories_Tbl Category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id.Equals(Id) select Categories_Tbl).FirstOrDefault();
                    Category.title = Title;
                    if (status.Equals("hide"))
                    {
                        Category.enable_flag = false;
                    }
                    else
                    {
                        Category.enable_flag = true;
                    }
                    DB.SaveChanges();
                }

            });
        }
        //Get Edit Category End






        //Add New City Start
        public static void Add_New_City(Resume.maker.Models.Input_Models.City_Model New_City)
        {
            new Task(()=> {

                using (Database_DB DB = new Database_DB())
                {
                    Cites_tbl City = new Cites_tbl();
                    City.city_name = New_City.name;
                    City.subcity = New_City.top_id;
                    City.delete_flag = false;
                    City.enable_flag = true;
                    DB.Cites_tbl.Add(City);
                    DB.SaveChanges();
                }

            }).Start();
        }
        //Add New City End







        //Get City Name From Id Start
        public static string Get_City_Name_From_Id(string Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                int City_Id = int.Parse(Id);
                Task<Cites_tbl> Cities_Tsk = new Task<Cites_tbl>(()=> { return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.id == City_Id select Cites_tbl).FirstOrDefault(); });
                Cities_Tsk.Start();
                Cites_tbl city = Cities_Tsk.Result;
                return city.city_name;
            }
        }
        //Get City Name From Id End







        //Get Delete City Start
        public static void Delete_City(int Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                new Task(()=> {

                    Cites_tbl city = (from Cites_tbl in DB.Cites_tbl where Cites_tbl.id == Id select Cites_tbl).FirstOrDefault();
                    city.delete_flag = true;
                    DB.SaveChanges();

                }).Start();
            }
        }
        //Get Delete City End







        //Get Edit City Start
        public static void Get_Edit_City(Resume.maker.Models.Input_Models.Edit_City_Model City)
        {
            using (Database_DB DB = new Database_DB())
            {
                int Id = int.Parse(City.id);
                Task<Cites_tbl> Cities_Tsk = new Task<Cites_tbl>(()=> {return (from Cites_tbl in DB.Cites_tbl where Cites_tbl.id == Id select Cites_tbl).FirstOrDefault(); });
                Cities_Tsk.Start();
                Cites_tbl city = Cities_Tsk.Result;
                city.city_name = City.name;

                if(City.status=="hide")
                {
                    city.enable_flag = false;
                }
                else
                {
                    city.enable_flag = true;
                }

                DB.SaveChanges();
            }
        }
        //Get Edit City End








        //Get All Admins Start
        public static List<Users_Tbl> Get_All_Admins()
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> Users_Tsk = new Task<List<Users_Tbl>>(() => { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.user_Type.Equals("ADMIN") select Users_Tbl).ToList<Users_Tbl>(); });
                Users_Tsk.Start();
                return Users_Tsk.Result;
            }
        }
        //Get All Admins End




        //Get All Users Start
        public static List<Users_Tbl> Get_All_Users()
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> Users_Tsk = new Task<List<Users_Tbl>>(()=> { return (from Users_Tbl in DB.Users_Tbl select Users_Tbl).ToList<Users_Tbl>(); });
                Users_Tsk.Start();
                return Users_Tsk.Result;
            }
        }
        //Get All Users End




        //Get All Users Start
        public static List<Users_Tbl> Get_Search_Users(string Search)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<List<Users_Tbl>> Users_Tsk = new Task<List<Users_Tbl>>(()=> {
                    return (from Users_Tbl in DB.Users_Tbl
                            where Users_Tbl.name.Contains(Search) || Users_Tbl.family.Contains(Search) || Users_Tbl.phone.Contains(Search) || Users_Tbl.username.Contains(Search)
                            select Users_Tbl).ToList<Users_Tbl>();
                });

                Users_Tsk.Start();
                return Users_Tsk.Result;
            }
        }
        //Get All Users End






        //Get User Category Start
        public static string Category_Name(int User_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Categories_Tbl> Category_Tsk = new Task<Categories_Tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl join Categories_Tbl in DB.Categories_Tbl on Users_Tbl.category_id.id equals Categories_Tbl.id where Users_Tbl.id == User_Id select Categories_Tbl).FirstOrDefault(); });
                Category_Tsk.Start();
                Categories_Tbl User_Data = Category_Tsk.Result;
                return User_Data.title;
            }
            return "";
        }
        //Get User Category End






        //Get City Name Start
        public static string City_Name(int User_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Cites_tbl> City_Tbl_Tsk = new Task<Cites_tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl join Cites_tbl in DB.Cites_tbl on Users_Tbl.city.id equals Cites_tbl.id where Users_Tbl.id == User_Id select Cites_tbl).FirstOrDefault(); });
                City_Tbl_Tsk.Start();
                Cites_tbl User_Data = City_Tbl_Tsk.Result;
                return User_Data.city_name;
            }
            return "";
        }
        //Get City Name End



        //Get User Data Json Start
        public static JObject Get_User_Data_Json(int UserId)
        {
            using (Database_DB DB = new Database_DB())
            {
                JObject json = new JObject();
                Task<Users_Tbl> User_Tbl_Tsk = new Task<Users_Tbl>(() => { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id.Equals(UserId) select Users_Tbl).FirstOrDefault(); });
                User_Tbl_Tsk.Start();
                Users_Tbl User = User_Tbl_Tsk.Result;
                json.Add("id",User.id);
                json.Add("type", User.user_Type);
                json.Add("status", User.enable_flag);
                return json;
            }
        }
        //Get User Data Json End



        //Get Edit User Start
        public static void Get_Edit_User(int Id,string UserType,string Status)
        {
            using (Database_DB DB = new Database_DB())
            {
                Task<Users_Tbl> User_Tbl_Tsk = new Task<Users_Tbl>(()=> { return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == Id select Users_Tbl).FirstOrDefault(); });
                User_Tbl_Tsk.Start();
                Users_Tbl user = User_Tbl_Tsk.Result;
                

                if (UserType.Equals("admin"))
                {
                    user.user_Type = "ADMIN";
                }
                else if(UserType.Equals("user"))
                {
                    user.user_Type = "USER";
                }

                if(Status.Equals("active"))
                {
                    user.enable_flag = true;
                }
                else if (Status.Equals("diactive"))
                {
                    user.enable_flag = false;
                }

                DB.SaveChanges();
            }
        }
        //Get Edit User End



    }
}