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
        public ActionResult Create([Bind(Include = "ID,Title,MemberCount,Semester,AccessCode,AdminInfo,ProfileImage,UniversityId")] Class @class)
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
        {
            // var id = User.Identity.Name;
            // Do stuf...


            var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
            ViewBag.UserID = ticketData["UserID"];

            //Note: ROleID for Students:2   &   ROleID for Proffesors:1
            ViewBag.UserRoleId =int.Parse(ticketData["UserRoleId"]);


            List<Class> Myclass = new List<Class>();

             //here we must show user the list of classes hw joined and for professor we must show him buttomn for create new class

            return View();
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
                try
                {
                    db.Classes.Add(@class);
                    db.SaveChanges();

                    return View("ClassSuccessful");
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





        public Class FindClassById(Guid? id)
        {
            if (id == null)           
                  return null;
            
            Class @class = db.Classes.Find(id);

            if (@class == null)          
                return null;
            

            return @class;

        }

        public PartialViewResult FindClassByAccessCode(string accessCode)
        {
            if (accessCode == null)
                return null;

            Class @class = db.Classes.Where(x => x.AccessCode == accessCode).FirstOrDefault();

            if (@class == null)
                return null;


            
            return PartialView(@class);

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
