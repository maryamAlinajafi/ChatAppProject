using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using FormsAuthenticationExtensions;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace ChatApp.Controllers
    {
    [Authorize]
    public class ClassesController : Controller
    {
        private ChatAppContext db = new ChatAppContext();


        // GET: Classes
        public ActionResult Index()
        {
            var classes = db.Classes.Include(u => u.University);
            return View(classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,MemberCount,Semester,AccessCode,AdminInfo,ProfileImage,UniversityId")] Model.Class @class)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Classes.Add(@class);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                   /////?????????????????????????????????????????????????/
                    throw;
                }
               
            }

            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle", @class.UniversityId);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle", @class.UniversityId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,MemberCount,Semester,AccessCode,AdminInfo,ProfileImage,UniversityId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle", @class.UniversityId);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        //*****************************************************
        //*****************************************************






        public ActionResult MyClasses()
        {   //Goal:show ether "CreationClassPage " or "JoinClassPage " To the User,Depend on What is his Role ?!
            var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
                  //  ViewBag.UserID = ticketData["UserID"];          
            //Note: ROleID for Students:2   &   ROleID for Proffesors:1
            ViewBag.UserRoleId =int.Parse(ticketData["UserRoleId"]);


            string userid = ticketData["UserID"];
            User u = FindUserById(Guid.Parse(userid));
            var UserClassList = u.Classes;
            

            return View(UserClassList);
        }
        public ActionResult OpenClass(int id)
        {
            return View();

        }

        public JsonResult LeftClass()
        {
            int id = Int32.Parse(Request["id"]);
           
            var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
            string userid = ticketData["UserID"];
            User u = FindUserById(Guid.Parse(userid));
            Class c = FindClassById(id);

            //if the User is admin of class(=Professor) ,then Class must DESTROY at all 

            if (c.AdminInfo==u.Username)
            {
                db.Classes.Remove(c);
                db.SaveChanges();
            }
            else
            {
                //he is just a student:
                u.Classes.Remove(c);
                db.SaveChanges();
            }



            
           
          
            return Json(true, JsonRequestBehavior.AllowGet);
         //   Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult FindUniversity()
        {
            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle");
            return View();

        }
        public ActionResult CreateClass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClass([Bind(Include = "ID,Title,MemberCount,Semester,AccessCode,AdminInfo,ProfileImage,UniversityId")] Class @class)
        {


            if (ModelState.IsValid)
            {
                var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
                string userid = ticketData["UserID"];
                User u = FindUserById(Guid.Parse(userid));

               
                    @class.AdminInfo = u.Username;
                    @class.MemberCount = 0;
                    db.Classes.Add(@class);
                    
                    //add the creator(=professor) as one member :
                    @class.Users.Add(u);
                    @class.MemberCount++;
                //complete AdminInfo Column whit creator(=professor) Username :
                try
                {
                    db.SaveChanges();
                    return View("ClassSuccesfullyCreated");

                }


                catch (DbUpdateException e)
                {
                    var sqlException = e.GetBaseException() as SqlException;
                    //2601 is error number of unique index violation
                    if (sqlException != null && sqlException.Number == 2601)
                    {
                        //Unique index was violated. Show corresponding error message to user:
                        ModelState.AddModelError("AccessCode", "کدورود انتخاب شده تکراری میباشد...");
                    }
                }

            }

            ViewBag.UniversityId = new SelectList(db.Universities, "ID", "UniversityTitle", @class.UniversityId);
            return View(@class);
        }





        public Class FindClassById(int? id)
        {
            if (id == null)           
                  return null;
            
            Class @class = db.Classes.Find(id);

            if (@class == null)          
                return null;
            

            return @class;

        }

        public User FindUserById(Guid id)
        {
            if (id == null)
                return null;

            User u= db.Users.Find(id);

            if (u== null)
                return null;


            return u;

        }


        [HttpPost]
        public PartialViewResult FindClassByAccessCode(string accessCode)
        {

            Class c = new Class();


            if (accessCode == null)           
                return PartialView(c);
            

            Class @class = db.Classes.Where(x => x.AccessCode == accessCode).FirstOrDefault();

            if (@class == null)
                return PartialView(c);

            return PartialView(@class);

        }

        public ActionResult JoinClass(int id)
        {


            try
            {
               var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
               string userid = ticketData["UserID"];
               User u = FindUserById(Guid.Parse(userid));
               Class c = FindClassById(id);
                ViewBag.StudentName = u.Firstname + " " + u.Lastname;
                ViewBag.classname = "" + c.Title;
                //check current user hane NOT enrolled in class yet:
                if (u.Classes.Contains(c))
                {
                    return View("EnrollmentError");
                }
                db.Users.Attach(u);
               u.Classes.Add(c);               
               db.SaveChanges();
                c.MemberCount++;
                db.SaveChanges();

                //Creating some viewbag to show User some Info about new class that he has joind:
               
                ViewBag.nth = "" + (c.Users.Count-1) ;
                return View("UserSuccesfullyJoined");
            }
            catch (Exception)
            {
                return Content("Error from controller!");
            }







            
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
