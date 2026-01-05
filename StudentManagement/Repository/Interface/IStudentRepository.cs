using StudentManagement.Data;
using StudentManagement.Dto.StudentModel;

namespace StudentManagement.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<bool> AddStudent (AddStudentDto dto);
        Task<Student> GetStudentsById(Guid Id, CancellationToken cancellationToken);
        Task<List<Student>> GetAllStudent(CancellationToken cancellationToken);
        Task<bool> UpdateStudents(Student dto, CancellationToken cancellationToken);
        Task<bool> DeleteStudents(Guid Id, CancellationToken cancellationToken);
    }
}
