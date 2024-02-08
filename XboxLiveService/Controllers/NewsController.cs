using Microsoft.AspNetCore.Mvc;

namespace XboxLiveService.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
