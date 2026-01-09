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
                return View(dto);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (dto.Id == Guid.Empty)
            {
                return BadRequest("Invalid course ID");
            }

            var result = await _courseService.UpdateCourse(dto.Id, dto, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            return RedirectToAction(nameof(FrontPage));
        }



        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            var response = await _courseService.GetCourseById(id, cancellationToken);

            var dto = new CourseDto
            {
                Id = response.Data.Id,
                CourseName = response.Data.CourseName
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