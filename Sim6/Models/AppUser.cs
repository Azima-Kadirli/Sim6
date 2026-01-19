using Microsoft.AspNetCore.Identity;

namespace Sim6.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
