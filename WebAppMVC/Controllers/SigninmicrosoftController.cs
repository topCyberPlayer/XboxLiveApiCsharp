using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SigninmicrosoftController : Controller
    {
        public IActionResult Index()
        {
            return View("/Home/Index");
        }
    }
}
