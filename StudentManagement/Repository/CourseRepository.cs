using Microsoft.EntityFrameworkCore;
using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Repository.Interface;

namespace StudentManagement.Repository
{
    public class CourseRepository(ApplicationDbContext _dbContext) : ICourseRepository
    {
        public async Task<bool> AddCourse(AddCourseDto request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                CourseName = request.CourseName,
            };

            await _dbContext.Courses.AddAsync(course, cancellationToken);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourse(Guid Id, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.FindAsync(new object[] { Id }, cancellationToken);

            if (course == null)
                return false;

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<List<Course>> GetAllCourse(CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.ToListAsync(cancellationToken);
        }

        public async Task<Course> GetCourseById(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
        }

        public async Task<bool> UpdateCourse(Guid Id, AddCourseDto request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.FindAsync(new object[] { Id }, cancellationToken);
            if (course == null)
                return false;

            course.CourseName = request.CourseName;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
