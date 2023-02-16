using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FullName = "Super Admin",
                    Email = "superadmin@bookstore.com",
                    UserName = "superadmin@bookstore.com",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
