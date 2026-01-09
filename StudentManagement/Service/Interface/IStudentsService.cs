using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Data;
using StudentManagement.Dto;
using StudentManagement.Dto.StudentModel;

namespace StudentManagement.Service.Interface
{
    public interface IStudentsService
    {
        Task<BaseResponse<StudentResponseDto>> AddStudent(AddStudentDto request, CancellationToken cancellationToken);
        Task<BaseResponse<List<StudentResponseDto>>> GetAllStudent(CancellationToken cancellationToken);
        Task<Student> GetStudentById(Guid Id, CancellationToken cancellationToken);
        Task<List<CourseDropdownDto>> GetCourseForDropdown();
        Task<BaseResponse<bool>> DeleteStudent(Guid Id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateStudent(Guid id, EditStudentDto request, CancellationToken cancellationToken);
    }
}
