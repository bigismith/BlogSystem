namespace BlogSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogSystem.Data.BlogSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BlogSystem.Data.BlogSystemDbContext";
        }

        protected override void Seed(BlogSystem.Data.BlogSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleAdmin = new IdentityRole
                {
                    Name = "Admin"
                };

                manager.Create(roleAdmin);
            }


            if (!context.Roles.Any(r => r.Name == "Blogger"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleBlogger = new IdentityRole
                {
                    Name = "Blogger"
                };

                manager.Create(roleBlogger);
            }

            if (!context.Users.Any(u => u.UserName == "bore@bore.bore"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "bore@bore.bore",
                    Email = "bore@bore.bore"
                };

                manager.Create(user, "Bore-33");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
