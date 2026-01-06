using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Service.Interface;

namespace StudentManagement.Controllers
{
    public class CourseController(ICourseService courseService) :Controller
    {
        [HttpGet]
        public async Task<IActionResult> FrontPage ()
        {
            var response = await courseService.GetAllCourses();

            if (!response.IsSuccess || response.Data == null)
            {
                return View(new List<CourseDto>());
            }

            var course = response.Data.Select(d => new CourseDto
            {
                Id = d.Id,
                CourseName = d.CourseName,
            });

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Create ()
        {
            return View(new AddCourseDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create (AddCourseDto dto,CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            var result = await courseService.AddCourse(dto, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(dto);
            }

            return RedirectToAction("FrontPage");
                
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellationToken)
        {
            var response = await courseService.GetCourseById(Id, cancellationToken);

            if(!response.IsSuccess || response.Data == null)
            {
                return NotFound(); 
            }
             var model = new EditCourseDto
             {
                    Id = response.Data.Id,
                    CourseName = response.Data.CourseName
             };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, EditCourseDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await courseService.UpdateCourse(Id, dto, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(dto);
            }

            return RedirectToAction("FrontPage");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return NotFound();

            var response = await courseService.GetCourseById(id, cancellationToken);

            if (!response.IsSuccess || response.Data == null)
                return NotFound();

            var dto = new CourseDto
            {
                Id = response.Data.Id,
                CourseName = response.Data.CourseName,
            };

            return View(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = await courseService.DeleteCourse(id, cancellationToken);
            return RedirectToAction("FrontPage");
        }


    }
}
