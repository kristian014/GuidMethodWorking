using GuidMethodWorking.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuidMethodWorking.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuidMethodWorking.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuidMethodWorking.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //this working perfectly!!!!!!
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "kristian234@mvc.com"))
            {
                var user = new ApplicationUser { UserName = "kristian234@mvc.com", Email = "kristian234@mvc.com" };
                userManager.Create(user, "Magnus123@");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "SuperAdminPlus" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "SuperAdminPlus");
            }


        }
    }
}
