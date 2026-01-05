using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Repository.Interface;
using System.Threading;

namespace StudentManagement.Repository
{
    public class StudentRepository(ApplicationDbContext dbContext) : IStudentRepository
    {
        public async Task<bool> AddStudent(AddStudentDto dto)
        {
            var students = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };

            await dbContext.Students.AddAsync(students);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudents(Guid Id, CancellationToken cancellationToken)
        {
            var students = await dbContext.Student.FindAsync(new object[] { Id }, cancellationToken);

            if (students == null)
                return false;

            dbContext.Students.Remove(students);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<List<Student>> GetAllStudent(CancellationToken cancellationToken)
        {
            return await dbContext.Students.ToListAsync(cancellationToken);
        }

        public async Task<Student> GetStudentsById(Guid Id, CancellationToken cancellationToken)
        {
            return await dbContext.Students.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
        }

        public async Task<bool> UpdateStudents(Student dto, CancellationToken cancellationToken)
        {
            dbContext.Students.Update(dto);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
