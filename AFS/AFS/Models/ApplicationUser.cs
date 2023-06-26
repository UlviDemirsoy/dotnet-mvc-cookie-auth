using Microsoft.AspNetCore.Identity;

namespace AFS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name{ get; set; }
    }
}
