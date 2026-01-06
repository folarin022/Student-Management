using StudentManagement.Data;
using StudentManagement.Dto.StudentModel;

namespace StudentManagement.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<bool> AddStudent(AddStudentDto dto, CancellationToken cancellationToken);
        Task<bool> DeleteStudent(Guid id, CancellationToken cancellationToken);
        Task<List<Student>> GetAllStudents(CancellationToken cancellationToken);
        Task<Student?> GetStudentById(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateStudent(AddStudentDto dto, CancellationToken cancellationToken);
    }
}