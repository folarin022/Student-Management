using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Service.Interface;

namespace StudentManagement.Controllers
{
    public class StudentController(IStudentsService studentsService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> HomePage(CancellationToken cancellationToken)
        {
            var response = await studentsService.GetAllStudent(cancellationToken);

            if (!response.IsSuccess || response.Data == null)
            {
                return View(new List<StudentResponseDto>());
            }


            var studentDtos = response.Data.Select(d => new StudentResponseDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Gender = d.Gender,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                Address = d.Address,
                Course = d.Course,
            }).ToList();

            return View(studentDtos);
        }

        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            var student = new AddStudentDto
            {
                Courses = await studentsService.GetCourseForDropdown()
            };

            return View(student);
        }





        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto student, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                student.Courses = await studentsService.GetCourseForDropdown();
                return View(student);
            }

            await studentsService.AddStudent(student, cancellationToken);
            return RedirectToAction("HomePage");
        }


        [HttpGet]
        public async Task<IActionResult> EditStudent(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var student = await studentsService.GetStudentById(id, cancellationToken);

            if (student == null)
            {
                return NotFound();
            }

            var request = new EditStudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                CourseId = student.CourseId,
                Course = await studentsService.GetCourseForDropdown(),
            };

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditStudentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                request.Course = await studentsService.GetCourseForDropdown();
                return View(request);
            }

            var edit = await studentsService.UpdateStudent(request.Id, request, cancellationToken);
            if (!edit.IsSuccess)
            {
                ModelState.AddModelError("", edit.Message);
                request.Course = await studentsService.GetCourseForDropdown();
                return View(request);
            }

            return RedirectToAction("HomePage");
        }

        [HttpGet]
        public async Task<IActionResult> StudentDetails(Guid Id, CancellationToken cancellationToken)
        {
            if (Id == Guid.Empty)
                return NotFound();


            var student = await studentsService.GetStudentById(Id, cancellationToken);
            if (student == null)
            {
                return NotFound();
            }

            var dto = new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                Course = student.Course.CourseName
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Guid Id, CancellationToken cancellationToken)
        {
            await studentsService.DeleteStudent(Id, cancellationToken);
            return RedirectToAction("HomePage");
        }
    }
}
