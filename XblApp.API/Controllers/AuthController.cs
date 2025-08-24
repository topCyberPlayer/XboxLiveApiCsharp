using Application.InnerUseCases;
using Microsoft.AspNetCore.Mvc;
using XblApp.Domain.Interfaces;
using XblApp.Domain.Requests;
using XblApp.Domain.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XblApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController() : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest model,
                                                  [FromServices] RegisterUserUseCase registerUserUseCase)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            RegisterUserResult result = await registerUserUseCase.RegisterUser(model.Gamertag, model.Email, model.Password);

            if (!result.Success) return BadRequest(result);

            return Ok(new { Message = "Пользователь успешно зарегистрирован" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest model,
                                               [FromServices] LoginUserUseCase loginUserUseCase)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LoginUserResult result = await loginUserUseCase.LoginUser(model.Gamertag, model.Password);

            return Ok(result);
        }
    }
}
