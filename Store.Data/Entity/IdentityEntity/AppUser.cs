
using Microsoft.AspNetCore.Identity;

namespace Store.Data.Entity.IdentityEntity
{
    public class AppUser : IdentityUser
    {
        public string  DisplayName { get; set; }
        public Address   Address{ get; set; }
    }
}
