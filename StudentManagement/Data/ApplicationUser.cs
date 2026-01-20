using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Data
{
    public class ApplicationUser :IdentityUser
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Gender { get; internal set; }
        public string Password { get; internal set; }
    }
}

