using Microsoft.AspNetCore.Mvc;
using T05_ASP.NETCoreIntroduction.Contracts;
using T05_ASP.NETCoreIntroduction.Models;

namespace T05_ASP.NETCoreIntroduction.Controllers
{
    [Route("/intro")]
    public class IntroController : Controller
    {
        private readonly IStudentService studentService;

        public IntroController(IStudentService _studentService)
        {
            studentService = _studentService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetNumber(int number)
        {
            ViewBag.Title = "GetNumber";

            return View("Number", number);
        }

        [Route("data")]
        public IActionResult GetStudentData(int id)
        {
            ViewBag.Title = "Get Student Data";

            var model = studentService.GetStudent(id);

            return View("StudentData", model);
        }

        [Route("edit")]
        public IActionResult EditStudent(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View("StudentData", model);
            }

            if (studentService.UpdateStudent(model))
            {
                return RedirectToAction(nameof(GetStudentData), new { id = model.Id });
            }

            return RedirectToAction(nameof(Index));

        }
    }
}

