namespace StudentManagement.Data
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; }
    }
}
