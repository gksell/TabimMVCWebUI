using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TabimMVCWebUI.Entity;

namespace TabimMVCWebUI.Controllers
{
    
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin
        [Authorize(Roles ="admin")]
        public ActionResult ListAdmin()
        {
            var liste = db.UserOperation
                .Where(i => i.IsApproved == null);
            liste = liste.OrderByDescending(i => i.UploadTime);

            return View(liste.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.UserOperation.ToList());
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameSurname,Description,Documantation,IsApproved,ManagerDescription,ManagerUploadTime")] UserOperation userOperation)
        {
            if (ModelState.IsValid)
            {
                var ent = db.UserOperation.Find(userOperation.Id);
                if (ent != null)
                {
                    ent.NameSurname = userOperation.NameSurname;
                    userOperation.ManagerUploadTime = DateTime.Now;
                    ent.IsApproved = userOperation.IsApproved;
                    ent.ManagerDescription = userOperation.ManagerDescription;
                    ent.ManagerUploadTime = userOperation.ManagerUploadTime;
                    
                    db.SaveChanges();
                    SendMail(userOperation.NameSurname,userOperation.Description,userOperation.ManagerDescription,userOperation.IsApproved);
                    return RedirectToAction("Index");
                }
            }
            return View(userOperation);
        }
       
        public ActionResult PrintAllDemand()
        {
            var report = new Rotativa.ActionAsPdf("Index");
            return report;
        }

        public void SendMail(string name,string description,string mDescription,bool? durum)
        {
            string durum2 = null;
            if (durum == true)
            {
                durum2 = "olumlu";
            }
            if (durum==false)
            {
                durum2 = "olumsuz";
            }
                
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("sender@atanur.net", " Göksel Yıldızak"); //E-Posta'nin kimden gönderileceği bilgisini tutar.
            ePosta.To.Add("gokselyildizak@gmail.com");
           
            //ePosta.Attachments.Add(new Attachment(@"C:\deneme.txt")); //Eklenecek dosya konumunu tutar.
            ePosta.Subject = "Talep Detayı Hakkında"; // Konu bilgisi tutar
            ePosta.IsBodyHtml = true;
            ePosta.Body = "Sayın " + name + " ; "+ description + "  talebiniz "+ mDescription +" nedeni ile " + durum2 + " sonuçlandırılmıştır. "; // İçerik bilgisini tutar

            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new System.Net.NetworkCredential("sender@atanur.net", "*%_ntE-rcnNH"); //Gönderen adresin bilgileri
            smtp.Port = 587;
            smtp.Host = "45.158.12.111";
            smtp.EnableSsl = false;
            object userState = ePosta;
            try
            {
                smtp.Send(ePosta);// E-Posta'yı asenkron olarak gönderir. Yani e-posta gönderilene kadar çalısan thread kapanmaz, gönderme işlemi tamamlandıktan sonra kapatılır.
                                                       //smtp.Send(ePosta); //Send ya da SendAsync
                                                       // MessageBox.Show("Mail Gönderme Başarılı");
            }
            catch (SmtpException ex)
            {

                Console.WriteLine("MW");
            }

        }
    }
}