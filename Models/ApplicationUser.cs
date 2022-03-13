using Microsoft.AspNetCore.Identity;

namespace Vidly.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string DriveLicense { get; set; }
    }
}
