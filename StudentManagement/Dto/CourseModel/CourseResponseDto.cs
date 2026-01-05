using StudentManagement.Dto.StudentModel;

namespace StudentManagement.Dto.CourseModel
{
    public class CourseResponseDto
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public List<StudentResponseDto> Students { get; set; } = new List<StudentResponseDto>();
    }
}
