
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Enums;
using XandaApp.Data.Models;
using XandaApp.Data.Constants;

namespace XandaApp.Data.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.App.ToString()));

            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = UserConstants.default_username, Email = UserConstants.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, UserConstants.default_password);
                await userManager.AddToRoleAsync(defaultUser, UserConstants.default_role.ToString());
            }
        }
    }
}
