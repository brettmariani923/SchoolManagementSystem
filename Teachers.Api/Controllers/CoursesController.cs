using Microsoft.AspNetCore.Mvc;
using Teachers.Domain.Interfaces;
using Teachers.Domain.Implementation;


namespace Teachers.Api.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IDataAccess _data;
        public IActionResult Index()
        {
            return View();
        }
    }
}
