using Microsoft.AspNetCore.Mvc;

namespace Teachers.Api.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
