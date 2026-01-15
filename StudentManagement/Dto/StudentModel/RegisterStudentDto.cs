
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Dto.StudentModel
{
    public class RegisterStudentDto
    {
        [Required]
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Gender{ get; set; }
        public string EmailAddress{ get; set; }
        public string PhoneNumber{ get; set; }
        public string Password{ get; set; }

    }
}
