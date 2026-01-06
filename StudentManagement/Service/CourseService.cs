using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Repository.Interface;
using StudentManagement.Service.Interface;

namespace StudentManagement.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _dbContext = _dbContext;
        }

        public async Task<BaseResponse<CourseResponseDto>> AddCourse(AddCourseDto dto, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CourseResponseDto>();

            try
            {

                var course = new Course
                {
                    Id = Guid.NewGuid(),
                    CourseName = dto.CourseName,
                };

                _dbContext.Courses.Add(course);

                response.IsSuccess = true;
                response.Data = MapToResponseDto(course);
                response.Message = "Course added successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding course: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteCourse(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var isDeleted = await _courseRepository.DeleteCourse(id, cancellationToken);
                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Course not found";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Course deleted successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error deleting course: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<List<CourseResponseDto>>> GetAllCourses()
        {
            var response = new BaseResponse<List<CourseResponseDto>>();

            try
            {
                var courses = await _courseRepository.GetAllCourse();
                var courseDtos = courses.Select(MapToResponseDto).ToList();

                response.IsSuccess = true;
                response.Data = courseDtos;
                response.Message = "Courses retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error fetching courses: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<CourseResponseDto>> GetCourseById(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CourseResponseDto>();

            try
            {
                var course = await _courseRepository.GetCourseById(id, cancellationToken);
                if (course == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Course not found";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = MapToResponseDto(course);
                response.Message = "Course retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error retrieving course: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<bool>> UpdateCourse(Guid Id, EditCourseDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var course = await _courseRepository.GetCourseById(Id, cancellationToken);
                if (course == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Course not found";
                    return response;
                }


                course.CourseName = request.CourseName;

                await _dbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Course updated successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error updating course: {ex.Message}";
            }

            return response;
        }

        private CourseResponseDto MapToResponseDto(Course course)
        {
            return new CourseResponseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
            };
        }
    }
}