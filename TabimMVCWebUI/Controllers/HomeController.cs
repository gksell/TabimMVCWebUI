using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TabimMVCWebUI.Entity;
using Microsoft.AspNet.Identity;

namespace TabimMVCWebUI.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        
        // GET: Home
        public ActionResult Index()
        {
            // if null check yapılacak !
            // view düzenle olumlu olumsuz ekleme yapılacak ! 
            var userId = User.Identity.GetUserId();
            return View(db.UserOperation.Where(i=>i.UserId==userId).ToList());
        }

        
        
        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameSurname,Description,Documantation,IsApproved,UploadTime,ManagerDescription,ManagerUploadTime")] UserOperation userOperation)
        {
            if (ModelState.IsValid)
            {
                userOperation.UploadTime = DateTime.Now;
                userOperation.UserId = User.Identity.GetUserId();
                db.UserOperation.Add(userOperation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userOperation);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserOperation userOperation = db.UserOperation.Find(id);
            if (userOperation == null)
            {
                return HttpNotFound();
            }
            return View(userOperation);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserOperation userOperation = db.UserOperation.Find(id);
            db.UserOperation.Remove(userOperation);
            db.SaveChanges();
            return RedirectToAction("Index");
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
