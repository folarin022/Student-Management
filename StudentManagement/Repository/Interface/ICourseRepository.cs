using StudentManagement.Data;
using StudentManagement.Dto.CourseModel;

namespace StudentManagement.Repository.Interface
{
    public interface ICourseRepository
    {
        Task<bool> AddCourse(Course course, CancellationToken cancellationToken); 
        Task<bool> DeleteCourse(Guid id, CancellationToken cancellationToken);
        Task<List<Course>> GetAllCourses(); 
        Task<Course?> GetCourseById(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateCourse(Course course, CancellationToken cancellationToken);
    }
}
