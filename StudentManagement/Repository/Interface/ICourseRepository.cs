using StudentManagement.Data;
using StudentManagement.Dto.CourseModel;

namespace StudentManagement.Repository.Interface
{
    public interface ICourseRepository
    {
        Task<bool> AddCourse(AddCourseDto request, CancellationToken cancellationToken);
        Task<List<Course>> GetAllCourse();
        Task<Course> GetCourseById(Guid Id, CancellationToken cancellationToken);
        Task<bool> UpdateCourse(Guid Id, AddCourseDto request, CancellationToken cancellationToken);
        Task<bool> DeleteCourse(Guid Id, CancellationToken cancellationToken);
    }
}
