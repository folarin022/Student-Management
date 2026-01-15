using StudentManagement.Dto;
using StudentManagement.Dto.StudentModel;

namespace StudentManagement.Service.Interface
{
    public interface IStudentLoginService
    {
        Task<BaseResponse<bool>> RegisterStudent (RegisterStudentDto request,CancellationToken cancellationToken );
        Task<BaseResponse<bool>> Login (StudentLoginDto request,CancellationToken cancellationToken );
    }
}
