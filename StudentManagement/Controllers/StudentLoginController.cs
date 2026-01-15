using Microsoft.AspNetCore.Mvc;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Service.Interface;

namespace StudentManagement.Controllers
{
    public class StudentLoginController(IStudentLoginService studentLoginService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Login(StudentLoginDto request, CancellationToken cancellationToken)
        {
            await studentLoginService.Login(request, cancellationToken);
            return RedirectToAction("FrontPage");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentDto request, CancellationToken cancellationToken)
        {
            await studentLoginService.RegisterStudent(request, cancellationToken);
            return RedirectToAction("FrontPage");
        }

    }
}
