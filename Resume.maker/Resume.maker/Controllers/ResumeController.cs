using Newtonsoft.Json.Linq;
using Resume.maker.Models;
using Resume.maker.Models.Input_Models;
using Resume.maker.Models.Output_Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Resume.maker.Controllers
{
    public class ResumeController : Controller
    {








        /***********************************Login And Signup**************************************/
        //Get Website Root Start
        [Route("")]
        public async Task Index()
        {
            Login_Check();
            Response.Redirect("/Dashboard");
        }
        //Get Website Root End


        //Get Login Start
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            return View("Context/Login");
        }

        [HttpPost]
        [Route("Login")]
        public void Login(Login_Model User)
        {
            string Token=Functions.Statics.Static.Get_User_Login_Token(User).Result;
            if(Token.Trim()!="")
            {
                HttpCookie Login_Cookie = new HttpCookie("LT");
                Login_Cookie.Value = Token;
                Response.Cookies.Add(Login_Cookie);
            }
            Response.Redirect("~/");
        }
        //Get Login End



        //Get Signup Start
        [Route("Signup")]
        public async Task<ActionResult> Signup()
        {
            return View("Context/Signup");
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult> Signup(Signup_Model signup)
        {
            string Login_Token=Functions.Statics.Static.Signup(signup);
            if (Login_Token != "")
            {
                HttpCookie Login_Cookie = new HttpCookie("LT");
                Login_Cookie.Value = Login_Token;
                Response.Cookies.Add(Login_Cookie);
                Response.Redirect("~/");
            }
            else
            {
                ViewBag.Message = "نام کاربری از قبل وجود دارد";
            }
            return View("Context/Signup");
        }
        //Get Signup End



        //Get Password Start
        [HttpPost]
        [Route("ChangePassword")]
        public void Change_Password(string newpassword)
        {
            User_Model User_Data = Login_Check().Result;
            Resume.maker.Functions.Statics.Static.Get_Chnage_Password(User_Data,newpassword);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        //Get Password End



        //Get Setup Start
        [Route("SetupA")]
        public void SetupA()
        {
            Functions.Statics.Static.GetSetup_App();
        }

        [Route("SetupB")]
        public void SetupB()
        {
            Functions.Statics.Static.GetSetup_App();
        }
        //Get Setup End
        /***********************************Login And Signup**************************************/














        /***********************************Dashboard**************************************/
        //Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            User_Model User_Data = Login_Check().Result;
            return View("Context/Dashboard",User_Data);
        }
        //Dashboard End


        //ViewResume Start
        [Route("ViewResume")]
        public async Task<ActionResult> ViewResume()
        {
            User_Model User_Data = Login_Check().Result;
            return View("Context/ViewResume", User_Data);
        }
        //ViewResume End

        //Get Edit Resume Start
        [Route("EditResume")]
        public async Task<ActionResult> EditResume()
        {
            User_Model User_Data = Login_Check().Result;
            return View("Context/EditResume", User_Data);
        }

        [HttpPost]
        [Route("EditResume")]
        public void EditResume(maker.Models.Input_Models.Resume_Model resume)
        {
            User_Model User_Data = Login_Check().Result;
            string profile_image_address = "";
            
            if (resume.profile_image != null)
            {
                string name=Get_New_Name_For_File();
                string file_type = Path.GetExtension(resume.profile_image.FileName);
                resume.profile_image.SaveAs(Server.MapPath("~/Uploads/"+name+ file_type));
                profile_image_address = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host + ":" + Request.Url.Port + "/Uploads/" + name + file_type;
            }

            Functions.Statics.Static.Get_Edit_User_Resume(User_Data,resume, profile_image_address);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        //Get Edit Resume End


        //Search Start
        [Route("Search")]
        public async Task<ActionResult> Search()
        {
            User_Model User_Data = Login_Check().Result;
            return View("Context/Search", User_Data);
        }
        //Search End





        //Search Result Start
        [HttpGet]
        [Route("Search_Result/{User_Id}")]
        public async Task<ActionResult> Search_Result(string User_Id)
        {
            User_Model User_Data = Login_Check().Result;
            User_Model User = Functions.Statics.Static.Get_User_From_Id(int.Parse(User_Id),User_Data);
            return View("Context/Search_Result",User);
        }
        //Search Result End








        //Categoris Start
        [Route("Categoris")]
        public async Task<ActionResult> Categoris()
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            return View("Context/Categories", User_Data);
        }

        [HttpPost]
        [Route("Categoris")]
        public async Task<ActionResult> Categoris(string Category_Name)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Functions.Statics.Static.Add_New_Category(Category_Name);
            return View("Context/Categories", User_Data);
        }


        [HttpGet]
        [Route("Delete_Category/{Category_Id}")]
        public void Delete_Category(string Category_Id)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Functions.Statics.Static.Get_Delete_Category(int.Parse(Category_Id));
            Response.Redirect(Request.UrlReferrer.ToString());
        }


        [Route("Get_Category/{Category_Id}")]
        public async Task<ActionResult> Get_Category(string Category_Id)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            JObject json=Functions.Statics.Static.Get_Category(int.Parse(Category_Id));
            return Content(json.ToString(), "text/json");
        }

        [HttpPost]
        [Route("Get_Edit_Category")]
        public void Edit_Category(string category_id,string category_title,string category_status)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Functions.Statics.Static.Get_Edit_Category(int.Parse(category_id),category_title,category_status);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        //Categoris End








        //Cities Start
        [Route("Cities")]
        public async Task<ActionResult> Cities()
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            return View("Context/Cities", User_Data);
        }

        [HttpPost]
        [Route("Cities")]
        public async Task<ActionResult> Cities(Resume.maker.Models.Input_Models.City_Model New_City)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Functions.Statics.Static.Add_New_City(New_City);
            return View("Context/Cities", User_Data);
        }

        [HttpGet]
        [Route("Delete_City/{City_Id}")]
        public void Delete_City(string City_Id)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Resume.maker.Functions.Statics.Static.Delete_City(int.Parse(City_Id));
            Response.Redirect(Request.UrlReferrer.ToString());
        }


        [HttpGet]
        [Route("Get_City/{City_Id}")]
        public async Task<ActionResult> Get_City(string City_Id)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            JObject json=Resume.maker.Functions.Statics_Front.Statics_Front.Get_City_Data(int.Parse(City_Id));
            return Content(json.ToString(),"text/json");
        }


        [HttpPost]
        [Route("Edit_City")]
        public void Edit_City(Resume.maker.Models.Input_Models.Edit_City_Model City)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Resume.maker.Functions.Statics.Static.Get_Edit_City(City);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        //Cities End







        //Get All Users Start
        [Route("AllUsers")]
        public async Task<ActionResult> AllUsers()
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            ViewData.Clear();
            return View("Context/All_Users", User_Data);
        }

        [HttpPost]
        [Route("AllUsers")]
        public async Task<ActionResult> AllUsers(string Search_Value)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            if (Search_Value != string.Empty)
            {
                ViewData["Search_User"] = Search_Value;
            }
            else
            {
                ViewData.Clear();
            }
            return View("Context/All_Users", User_Data);
        }

        [HttpGet]
        [Route("AllUsers/GetUser/{UserId}")]
        public async Task<ActionResult> GetUser(string UserId)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            JObject json = Functions.Statics.Static.Get_User_Data_Json(int.Parse(UserId));
            return Content(json.ToString(),"text/json");
        }

        [HttpGet]
        [Route("AllUsers/EditUser/{UserId}/{UserStatus}/{UserType}")]
        public void GetUser(string UserId,string UserStatus,string UserType)
        {
            User_Model User_Data = Login_Check().Result;
            if (!User_Data.user_Type.Equals("ADMIN"))
            {
                Response.Redirect("~/Dashboard");
            }
            Functions.Statics.Static.Get_Edit_User(int.Parse(UserId),UserType,UserStatus);
        }
        //Get All Users End







        //Exit Start
        [Route("Exit")]
        public void Exit()
        {
            Response.Cookies["LT"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("~/");
        }
        //Exit Start


        /***********************************Dashboard**************************************/

















        //Get Api Start
        [HttpGet]
        [Route("Get_Cities_Of_State/{State_Name}")]
        public async Task<ActionResult> Get_Cities_Of_State(string State_Name)
        {
            JArray json_array = Functions.Statics_Front.Statics_Front.Get_Cities_Of_State(State_Name);
            return Content(json_array.ToString(),"text/json");
        }


        [HttpGet]
        [Route("Search/{Search_Value}")]
        public async Task<ActionResult> Get_Search(string Search_Value)
        {
            System.Diagnostics.Debug.WriteLine(Search_Value);
            User_Model User_Data =  Login_Check().Result;
            JArray json_array = Functions.Statics_Front.Statics_Front.Get_Search(Search_Value);
            return Content(json_array.ToString(), "text/json");
        }
        //Get Api End




















        /***********************************My Function**************************************/
        //My Function Start
        public async Task<Resume.maker.Models.Output_Models.User_Model> Login_Check()
        {
            
            if (Request.Cookies["LT"]!=null)
            {
                Task<bool> User_Auth = new Task<bool>(() => { return Functions.Statics.Static.Get_Check_Login_User_With_Login_Token(Request.Cookies["LT"].Value.ToString()); });
                User_Auth.Start();
                if (User_Auth.Result)
                {
                    Task<Users_Tbl> User_Tbl_Tsk = new Task<Users_Tbl>(()=> { return Functions.Statics.Static.Get_User_Data(Request.Cookies["LT"].Value.ToString()); });
                    User_Tbl_Tsk.Start();
                    Users_Tbl user = User_Tbl_Tsk.Result;
                    Resume.maker.Models.Output_Models.User_Model User = new Models.Output_Models.User_Model();
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

                    return User;
                }
            }
            Response.Redirect("~/Login");
            return null;
        }


        //Get New Image Address Start
        public string Get_New_Name_For_File()
        {
            string file_name;
            while (true)
            {
                if (System.IO.Directory.Exists(Server.MapPath("~/Uploads")))
                {
                    Random random_name = new Random();
                    string name=random_name.Next(1,int.MaxValue).ToString();
                    if (!System.IO.File.Exists(Server.MapPath("~/Uploads/"+name+".jpg")) && !System.IO.File.Exists(Server.MapPath("~/Uploads/" + name + ".png")))
                    {
                        file_name = name;
                        break;
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads"));
                }
            }
            return file_name;
        }
        //Get New Image Address End

        //My Function End
        /***********************************My Function**************************************/






    }
}