using Microsoft.AspNetCore.Mvc;

namespace XboxLiveService.Controllers
{
    public class ProfileController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
