using StudentManagement.Data;

namespace StudentManagement.Dto.CourseModel
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
