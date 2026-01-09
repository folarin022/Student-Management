using Microsoft.EntityFrameworkCore;
using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Repository.Interface;

namespace StudentManagement.Repository
{
    public class CourseRepository(ApplicationDbContext _dbContext) : ICourseRepository
    {
        public async Task<bool> AddCourse(Course course, CancellationToken cancellationToken)
        {
            await _dbContext.Courses.AddAsync(course, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
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

        public async Task<List<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
        }

        public async Task<bool> UpdateCourse(Course course, CancellationToken cancellationToken)
        {
            var existingCourse = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == course.Id, cancellationToken);

            if (existingCourse == null)
                return false;

            existingCourse.CourseName = course.CourseName;

            _dbContext.Courses.Update(existingCourse);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

    }
}
