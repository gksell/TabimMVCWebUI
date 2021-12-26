using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabimMVCWebUI.Entity;
using TabimMVCWebUI.Identity;
using TabimMVCWebUI.Models;


namespace TabimMVCWebUI.Controllers
{
    public class AccountController : Controller
    {

        public UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var UserStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(UserStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                // Register işlemleleri eğer validation şartları sağlanıyorsa
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;
                user.UserType = "user";
                
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    // kullanıcı başarı ile oluştuğunda rol ataması yap
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturulamadı, hata var !");
                }
            }
            
            return View(model);
        }
        //GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl, ApplicationRole rols)
        {
            if (ModelState.IsValid)
            {
                // Login yapılacak
                var user = UserManager.Find(model.UserName,model.Password);

                if (user != null)
                {
                    // var olan kullanıcı sisteme dahil olsun 
                    // ApplicationCookie oluşturma

                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityClaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();

                    authManager.SignIn(authProperties, identityClaims);
                    //if (!String.IsNullOrEmpty(ReturnUrl))
                    //{
                    //    return Redirect(ReturnUrl);
                    //}

                    var userType = user.UserType;
                    if (userType.Equals("admin"))
                    {
                        return RedirectToAction("ListAdmin", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Create", "Home");
                    } 
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok! ");
                } 
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UnAuthorized()
        {
            return View();
           
        }
    }
}