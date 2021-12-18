using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TabimMVCWebUI.Identity
{
    public class IdentityInitializer:DropCreateDatabaseIfModelChanges<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            // Roller 
            //Admin rolü
            if(!context.Roles.Any(i=>i.Name== "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "admin", Description = "AdminRolü" };
                manager.Create(role);
            }
            //user rolü
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "UserRolü" };
                manager.Create(role);
            }
            // Test verileri oluşturma (Admin Rolünde)

            if (!context.Users.Any(i => i.Name == "gokselyildizak@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {
                    Name="Göksel",
                    Surname="Yıldızak",
                    UserName="gokselyildizak@gmail.com",
                    PhoneNumber = "05379945816",
                    UserType = "admin"
                };

                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "admin");
            }
            if (!context.Users.Any(i=>i.Name == "deneme@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {
                    Name = "Deneme",
                    Surname = "Deneme",
                    UserName= "deneme@gmail.com",
                    PhoneNumber = "05344589191",
                    UserType="user"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "user");
            }
            base.Seed(context);
        }
    }
}