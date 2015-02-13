using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WM.AspNetMvc.Models;
using Constants = WM.AspNetMvc.Models.Constants;

namespace WM.AspNetMvc.Migrations
{
    internal class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "Admin@watermeter.com";
            const string password = "Default@123";

            var role = roleManager.Roles.FirstOrDefault(x => x.Name == Constants.AdminRoleName);
            if (role == null)
            {
                role = new ApplicationRole(Constants.AdminRoleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.FirstOrDefault() ?? string.Empty);

                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }

            base.Seed(context);
        }
    }
}
