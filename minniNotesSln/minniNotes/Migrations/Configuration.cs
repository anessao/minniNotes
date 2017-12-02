namespace minniNotes.Migrations
{
    using minniNotes.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<minniNotes.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(minniNotes.Models.ApplicationDbContext context)
        {


            var adminRole = new IdentityRole("Admin");
            context.Roles.AddOrUpdate(r => r.Name, adminRole);

            context.SaveChanges();

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser
            {
                UserName = "anessa",
                Email = "anessa.ortner@gmail.com",
            };

            userManager.CreateAsync(user, "password").Wait();

            var addedUser = context.Users.First(x => x.UserName == user.UserName);

            userManager.AddToRoleAsync(addedUser.Id, "Admin").Wait();
        }
    }
}
