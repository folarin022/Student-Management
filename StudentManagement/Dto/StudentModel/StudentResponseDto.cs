namespace StudentManagement.Dto.StudentModel
{
    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public string Course { get; set; } = string.Empty;
    }
}
