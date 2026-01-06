using StudentManagement.Dto;
using StudentManagement.Dto.CourseModel;

namespace StudentManagement.Service.Interface
{
    public interface ICourseService
    {
        Task<BaseResponse<CourseResponseDto>> AddCourse(AddCourseDto dto, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteCourse(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<List<CourseResponseDto>>> GetAllCourses(CancellationToken cancellationToken);
        Task<BaseResponse<CourseResponseDto>> GetCourseById(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateCourse(Guid id, EditCourseDto request, CancellationToken cancellationToken);
    }
}