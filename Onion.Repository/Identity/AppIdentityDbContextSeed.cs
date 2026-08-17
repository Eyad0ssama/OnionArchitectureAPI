using Microsoft.AspNetCore.Identity;
using Onion.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsynk(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Habiba Omar",
                    Email = "eyadossama598@gmail.com",
                    UserName = "EyadOsama.BB",
                    PhoneNumber = "01010217956"
                };
                await userManager.CreateAsync(User, "Pa$$word");
            }

        }
    }
}
