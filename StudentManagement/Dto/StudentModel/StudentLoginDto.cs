using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Dto.StudentModel
{
    public class StudentLoginDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool RememberMe { get; set; }
    }

}
