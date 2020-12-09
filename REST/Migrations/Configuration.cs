namespace REST.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using REST.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<REST.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "REST.Models.ApplicationDbContext";
        }

        protected override void Seed(REST.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "superadmin@pickup.com",
                Email = "superadmin@pickup.com",
                FirstName = "Group 4",
                LastName = "dmaj0919"
            };

            manager.Create(user, "Pwd123!");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Employee" });
                roleManager.Create(new IdentityRole { Name = "Customer" });
            }

            var adminUser = manager.FindByEmail("superadmin@pickup.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Employee", "Customer" });
            base.Seed(context);
        }
    }
}
