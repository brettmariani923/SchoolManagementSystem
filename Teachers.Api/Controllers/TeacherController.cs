using Microsoft.AspNetCore.Mvc;

namespace Teachers.Api.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
