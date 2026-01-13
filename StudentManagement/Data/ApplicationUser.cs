using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Data
{
    public class ApplicationUser :IdentityUser
    {
        public Guid Id { get; set; }

        public int UserName { get; set; }
        public string Password { get; set; }
    }
}
