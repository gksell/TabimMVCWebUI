using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TabimMVCWebUI.Entity;

namespace TabimMVCWebUI.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin
        public ActionResult ListAdmin()
        {
            var liste = db.UserOperation
                .Where(i => i.IsApproved == null);
            liste = liste.OrderByDescending(i => i.UploadTime);
            return View(liste.ToList());
        }
        public ActionResult Index()
        {
            return View(db.UserOperation.ToList());
        }

        // GET: Admin/Details/Id
        public ActionResult Details(int? id)
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
        // GET: Admin/Edit/Id
        public ActionResult Edit(int? id)
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

        // POST: Admin/Edit/ıd
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameSurname,Description,Documantation,IsApproved,ManagerDescription,ManagerUploadTime")] UserOperation userOperation)
        {
            if (ModelState.IsValid)
            {
                var ent = db.UserOperation.Find(userOperation.Id);
                if (ent != null)
                {
                    userOperation.ManagerUploadTime = DateTime.Now;
                    ent.IsApproved = userOperation.IsApproved;
                    ent.ManagerDescription = userOperation.ManagerDescription;
                    ent.ManagerUploadTime = userOperation.ManagerUploadTime;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(userOperation);
        }
    }
}