using Microsoft.AspNetCore.Identity;

namespace Project_UDW.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
