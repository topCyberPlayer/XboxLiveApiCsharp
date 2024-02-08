using Microsoft.AspNetCore.Mvc;

namespace XboxLiveService.Controllers
{
    public class GameController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
