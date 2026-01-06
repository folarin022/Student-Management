using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Repository.Interface;
using StudentManagement.Service.Interface;

namespace StudentManagement.Service
{
    public class StudentsService(IStudentRepository studentRepository, ApplicationDbContext dbContext) : IStudentsService
    {
        public async Task<BaseResponse<StudentResponseDto>> AddStudent(AddStudentDto request, CancellationToken cancellationToken)
        {
            var response= new BaseResponse<StudentResponseDto>();

            try
            {
                var course = await dbContext.Students
                    .FirstOrDefaultAsync(d => d.Id == request.CourseId, cancellationToken);

                if (course == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Course not found";
                }

                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    DateOfBirth = request.DateOfBirth,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    CourseId = request.CourseId,
                };

                dbContext.Students.Add(student);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;    
        }

        public async Task<BaseResponse<bool>> DeleteStudent(Guid Id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var isDeleted = await studentRepository.DeleteStudent(Id, cancellationToken);
                if (isDeleted)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Failed to delete student";
                }

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Student deleted successfully";
            }

            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = "Error deleting student";
            }
            return response; 
        }

        public async Task<BaseResponse<List<StudentResponseDto>>> GetAllStudent(CancellationToken cancellationToken)
        {
            var response= new BaseResponse<List<StudentResponseDto>>();

            try
            {
                var student = await dbContext.Students
                    .Include(e => e.Course)
                    .ToListAsync(cancellationToken);

                var list = student.Select(e => new StudentResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    Email = e.Email,
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    CourseId = e.CourseId,
                    Course = e.Course.CourseName,
                }).ToList();
            }

            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Error fetching student";
            }
            return response;
        }

        public Task<List<SelectListItem>> GetCourseForDropdown()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentById(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateStudent(Guid id, AddStudentDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
