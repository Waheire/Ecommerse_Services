using Microsoft.AspNetCore.Identity;

namespace TheJitu_Commerce_Auth.Model
{
    public class ApplicationUser : IdentityUser
    {
        //add your columns
        public string Name { get; set; } = string.Empty;
    }
}
