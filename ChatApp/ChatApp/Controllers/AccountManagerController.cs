using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ChatApp.Models;
using Model;
using FormsAuthenticationExtensions;
using System.Collections.Specialized;

namespace ChatApp.Controllers
{
    public class AccountManagerController : Controller
    {
        private ChatAppContext db = new ChatAppContext();

        // GET: AccountManager
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult Login(User user,string returnUrl)
        {
            //When user type some Url befor Login,and then directed to Loign Page,"returnUrl" will have some value...
            //but when The User himself press Login Butttomn,this that  will be null,so we redirect user to see his classes!!! 
          
            user.Password = Encryption.encrypt(user.Password);
            using ( ChatAppContext db = new ChatAppContext())
            {
                User u = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (u!=null)
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

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length>1 && returnUrl .StartsWith("/") && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    else
                    {
                        //ModelState.AddModelError("", "Invalid   URL!!!!!!!!!!");
                        return RedirectToAction("MyClasses", "Classes");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Not Correct!!!!!!");
                    return View();

                }
            }

           

        }




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
        public ActionResult SignUp([Bind(Include = "ID,Firstname,Lastname,Username,Password,ProfileImage,Status,EmailAddress,PhoneNumber,RoleId,Confirmation")] User user)
        {



            if (ModelState.IsValid)
            {
                user.ID = Guid.NewGuid();
                //Encrypting password:
                string EncryptedPassword = Encryption.encrypt(user.Password);
                user.Password = EncryptedPassword;
                //Encrypting ConfirmPassword:
                string EncryptedCOnfirmPassword = Encryption.encrypt(user.Confirmation);
                user.Confirmation = EncryptedCOnfirmPassword;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");

            }

            ViewBag.RoleId = new SelectList(db.Roles, "ID", "RoleName", user.RoleId);
            return View(user);


        }


        public ActionResult SignOut()
        {
            //ایا کاربر میتواند اکانت خودش را حذف کند؟؟؟؟؟؟
            return View();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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