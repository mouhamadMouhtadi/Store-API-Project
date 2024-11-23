using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Data.Entity.IdentityEntity;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mouhamad",
                    Email = "mahamd.mouh12345@gmail.com",
                    UserName = "MouhamadMouhtadi",
                    Address = new Address
                    {
                        FirstName = "Mouhamad",
                        LastName = "Mouhtadi",
                        City = "Houeich",
                        State = "Akkar",
                        Street = "5",
                        PostalCode = "112233",
                    }
                };
                await userManager.CreateAsync(user, "Password123#");
            }
        }
    }
}
