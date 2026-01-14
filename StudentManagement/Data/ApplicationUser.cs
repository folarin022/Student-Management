using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Data
{
    public class ApplicationUser :IdentityUser
    {
        public string Email { get; set; }
    }
}
