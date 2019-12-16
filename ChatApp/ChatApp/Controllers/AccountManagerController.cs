using FormsAuthenticationExtensions;
using Model;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatApp.Controllers
{
    public class AccountManagerController : Controller
    {
        private ChatAppContext db = new ChatAppContext();

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyClasses", "Classes");
            }
            else
            {
                return View();

            }

        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            //When user type some Url befor Login,and then directed to Loign Page,"returnUrl" will have some value...
            //but when The User himself press Login Butttomn,this that  will be null,so we redirect user to see his classes!!! 

            user.Password = Encryption.encrypt(user.Password);
            using (ChatAppContext db = new ChatAppContext())
            {
                User u = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    // FormsAuthentication.SetAuthCookie(user.Username,false);

                    //NOTE: I create a new FormsAuthenticationCookie intead of useing "AuthCookie" To Store additional data 
                    //in order to have username & userRoleId in all over the Project!

                    var ticketData = new NameValueCollection
                       {
                             { "UserID", u.ID.ToString() },
                             { "UserRoleId", u.RoleId.ToString() }
                       };
                    new FormsAuthentication().SetAuthCookie(u.Username, true, ticketData);

                    //declare a cookie for admin inorder to make difference in layout for him:
                    if (u.RoleId==1010)
                    {
                        Response.Cookies["adminCookie"].Value = "admin";

                    }


                    //Status field show that the whether the user is online or offline !
                    u.Status = true;
                    //save profile url in coocki:
                    
                    if (u.ProfileImage != null)
                    {
                        string cookieTitle = u.Username;
                        Response.Cookies[cookieTitle].Value = u.ProfileImage;

                    }



                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    else
                    {
                        return RedirectToAction("MyClasses", "Classes");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "!نام کاربری یا رمز عبور اشتباه است");
                    ModelState.Clear();
                    return View();

                }
            }



        }



        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public ActionResult SignUp()
        {
            //return RedirectToAction("Create", "Users");
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName");
            return View();

        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(AddEditUserViewmodel model)
        {

            if (ModelState.IsValid)
            {
                
               
                    //recive uploaded file as the profile image:
                    if (model.ImageFile!=null)
                    {
                        try
                        {
                            string filename = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                            string extention = Path.GetExtension(model.ImageFile.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                            model.ImagePath = "~/UserProfileImage/" + filename;
                            filename = Path.Combine(Server.MapPath("~/UserProfileImage/"), filename);
                            model.ImageFile.SaveAs(filename);

                            model.UserViewModel.ProfileImage = model.ImagePath;
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "آپلود عکس با مشکل مواجه شده است.لطفا مجددا تلاش کنید");
                            return View(model);
                        }
                        
                    }
                model.UserViewModel.ID = Guid.NewGuid();
                //Encrypting password:
                string EncryptedPassword = Encryption.encrypt(model.UserViewModel.Password);
                model.UserViewModel.Password = EncryptedPassword;
                //Encrypting ConfirmPassword:
                string EncryptedCOnfirmPassword = Encryption.encrypt(model.UserViewModel.Confirmation);
                model.UserViewModel.Confirmation = EncryptedCOnfirmPassword;


                try
                {
                    db.Users.Add(model.UserViewModel);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "ثبت نام شما در سامانه با خطا مواجه شده.لطفا مجددا اقدام کنید");
                }
                

            }

            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName", model.UserViewModel.RoleId);
            return View(model);

        }


        public ActionResult SignOut()
        {
            //ایا کاربر میتواند اکانت خودش را حذف کند؟؟؟؟؟؟
            return View();
        }


        public ActionResult LogOut()
        {
            // set user status :offline! 
            //{
            var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
            string userid = ticketData["UserID"];
            ClassesController c = new ClassesController();
            User u = c.FindUserById(Guid.Parse(userid));
            u.Status = false;
            //}
            //destroy Admin Cookie:
            if (Request.Cookies["adminCookie"] != null)
            {
                Response.Cookies["adminCookie"].Expires = DateTime.Now.AddDays(-1);
            }

            //destroy Profile Cookie:
            if (Request.Cookies["ProfileImage"] != null)
            {
                Response.Cookies["ProfileImage"].Expires = DateTime.Now.AddDays(-1);
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        public ActionResult NiceLoginPage()
        {
            return View();
        }


       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }








    }
}