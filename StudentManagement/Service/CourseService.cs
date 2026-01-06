using StudentManagement.Data;
using StudentManagement.Dto;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Models;
using StudentManagement.Repository.Interface;
using StudentManagement.Service.Interface;

namespace StudentManagement.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<BaseResponse<CourseResponseDto>> AddCourse(AddCourseDto dto, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CourseResponseDto>();

            try
            {
                // Check if course code already exists
                var existingCourse = await _courseRepository.GetCourseByCodeAsync(dto.CourseCode, cancellationToken);
                if (existingCourse != null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Course with code '{dto.CourseCode}' already exists.";
                    return response;
                }

                var course = new Course
                {
                    Id = Guid.NewGuid(),
                    CourseName = dto.CourseName,
                    CourseCode = dto.CourseCode,
                    Description = dto.Description,
                    Credits = dto.Credits,
                    CreatedAt = DateTime.UtcNow
                };

                var isAdded = await _courseRepository.AddCourse(course, cancellationToken);
                if (!isAdded)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to add course to database.";
                    return response;
                }

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

        public async Task<BaseResponse<List<CourseResponseDto>>> GetAllCourses(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<CourseResponseDto>>();

            try
            {
                var courses = await _courseRepository.GetAllCourses(cancellationToken);
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
                var course = await _courseRepository.GetCourseById(id, cancellationToken);
                if (course == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Course not found";
                    return response;
                }

           
                course.CourseName = request.CourseName;

                var isUpdated = await _courseRepository.UpdateCourse(Id,course, cancellationToken);
                if (!isUpdated)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Failed to update course";
                    return response;
                }

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