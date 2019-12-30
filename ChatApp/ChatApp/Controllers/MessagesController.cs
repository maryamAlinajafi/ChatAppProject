using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ChatApp.Models.CommonViewModels;
using FormsAuthenticationExtensions;
using Model;

namespace ChatApp.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private ChatAppContext db = new ChatAppContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Classes);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ID", "Title", message.ClassId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Text,SeenNumber,DateTime,UserId,ClassId")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ID", "Title", message.ClassId);
            return View(message);
        }

        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }

            db.Messages.Remove(message);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteByUserIdAndText(string Username,string Text,int ClassID)
        {
            if (Username == null || Text == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


            Guid UserId = db.Users.Where(u => u.Username == Username).FirstOrDefault().ID;

            Message message = db.Messages.Where(m=>m.UserId==UserId && m.Text==Text && m.ClassId==ClassID).FirstOrDefault();
            if (message == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }

            db.Messages.Remove(message);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

       

        //******************************************************************************
        //*****************************************************************************
        //*****************************************************************************


        public JsonResult LoadMsgs(int classId,int pageIndex)
        {


            var MsgList = (from msg in db.Messages
                           where msg.ClassId==classId
                           select new
                           {
                               msg.ID,
                              msg.Text,
                              msg.DateTime,
                              msg.UserId
                           })
                       .OrderByDescending(m => m.DateTime)
                       .Skip((pageIndex-1) * 10)
                       .Take(10)
                       .ToList();

            var MsgDisplayList = new List<MsgDisplay>();

            foreach (var item in MsgList)
            {
                var msg = new MsgDisplay()
                {
                    MsgID=item.ID,
                    Text = item.Text,
                    Time = item.DateTime.ToString(),
                    User_Username =FindUserNameById(item.UserId),
                    User_Profile=FindProfileImageById(item.UserId)

                };

                MsgDisplayList.Add(msg);
            }

           
            return Json(new { MsgList = MsgDisplayList }, JsonRequestBehavior.AllowGet);
        }
        public bool addMessage(string msgText,string classId)
        {
            //detect UserID
            var ticketData = ((FormsIdentity)HttpContext.User.Identity).Ticket.GetStructuredUserData();
            string userid = ticketData["UserID"];
            //Create Msg:
            var message = new Message();
            message.DateTime = DateTime.Now;
            message.ClassId =int.Parse(classId);
            message.UserId = Guid.Parse(userid);
            message.Text = msgText;
            try
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }


            
            
        }


        public string FindUserNameById(Guid id)
        {
            if (id == null)
                return null;

            User u = db.Users.Find(id);

            if (u == null)
                return null;


            return u.Username;

        }
        public string FindProfileImageById(Guid id)
        {
            if (id == null)
                return null;

            User u = db.Users.Find(id);

            if (u == null)
                return null;

            if (u.ProfileImage == null)
                return "/UserProfileImage/Profile.png";

            
            return u.ProfileImage.Remove(0, 1);

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
