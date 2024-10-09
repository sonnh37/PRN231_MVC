using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers
{
    public class AjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
