using Microsoft.AspNetCore.Mvc;
using StudentManagement.Dto.CourseModel;
using StudentManagement.Service;
using StudentManagement.Service.Interface;

namespace StudentManagement.Controllers
{
    public class CourseController :Controller
    {

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> FrontPage()
        {
            var response = await _courseService.GetAllCourses();

            if (!response.IsSuccess || response.Data == null)
            {
                return View(new List<CourseDto>());
            }


            var courseDtos = response.Data.Select(d => new CourseDto
            {
                Id = d.Id,
                CourseName = d.CourseName,
            }).ToList();

            return View(courseDtos); 
        }

        [HttpGet]
        public IActionResult Create ()
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

            var result = await _courseService.AddCourse(dto, cancellationToken);

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
            var response = await _courseService.GetCourseById(Id, cancellationToken);

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

            var result = await _courseService.UpdateCourse(Id, dto, cancellationToken);

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

            var response = await _courseService.GetCourseById(id, cancellationToken);

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

            var result = await _courseService.DeleteCourse(id, cancellationToken);
            return RedirectToAction("FrontPage");
        }


    }
}