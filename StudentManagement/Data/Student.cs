namespace StudentManagement.Data
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
    public class StudentViewModel
    {
        public Guid Id { set; get; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Course { get; set; }
    }
}
