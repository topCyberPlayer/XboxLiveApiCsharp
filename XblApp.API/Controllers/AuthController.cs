using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XblApp.Domain.DTO;
using XblApp.Infrastructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XblApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = new()
            {
                UserName = model.Gamertag,
                Email = model.Email,
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(new { Message = "Пользователь успешно зарегистрирован" });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Gamertag, model.Password, false, false);
            
            if (!result.Succeeded) return Unauthorized("Не удалось авторизоваться");

            return Ok(new { Message = "Авторизация успешна"});
        }
    }
}
