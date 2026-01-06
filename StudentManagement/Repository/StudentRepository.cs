using Microsoft.EntityFrameworkCore;
using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Repository.Interface;

namespace StudentManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddStudent(AddStudentDto dto, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };

            await _dbContext.Students.AddAsync(student, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteStudent(Guid id, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (student == null)
                return false;

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Student>> GetAllStudents(CancellationToken cancellationToken)
        {
            return await _dbContext.Students.ToListAsync(cancellationToken);


        }

        public async Task<Student?> GetStudentById(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateStudent(AddStudentDto dto, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == dto.Id, cancellationToken);

            if (student == null)
                return false;

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.Address = dto.Address;
            student.PhoneNumber = dto.PhoneNumber;

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}