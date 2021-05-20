namespace WebPresentation.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebPresentation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(WebPresentation.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string defaultPassword = "P@ssw0rd";

            // creating roles
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "researcher" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "user" });

            context.SaveChanges(); 

            // adding the admin

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin
                };

                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                    context.SaveChanges();
                }
            }
        }
    }
}
