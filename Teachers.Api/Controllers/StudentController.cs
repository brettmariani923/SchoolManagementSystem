using Microsoft.AspNetCore.Mvc;

namespace Teachers.Api.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
