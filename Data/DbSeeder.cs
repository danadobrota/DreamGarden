using Microsoft.AspNetCore.Identity;
using DreamGarden.Constants;

namespace DreamGarden.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            // creation of method can be used to seed default data into the database.
            var userMgr= service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            //adding some role to the database
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
           

            //create a default admin record
            var admin = new IdentityUser
            {
                UserName = "admin@dreamgarden.com",
                Email = "admin@dreamgarden.com",
                EmailConfirmed = true,

            };

            
        }
    }
}

