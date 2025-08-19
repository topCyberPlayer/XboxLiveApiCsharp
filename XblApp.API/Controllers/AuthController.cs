using Microsoft.AspNetCore.Mvc;
using XblApp.Application.InnerUseCases;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest model, 
                                               [FromServices] LoginUserUseCase loginUserUseCase,
                                               [FromServices] IJwtTokenGenerator tokenGenerator)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LoginUserResult result = await loginUserUseCase.Handle(model.Gamertag, model.Password);

            if (!result.Success) return Unauthorized(result.Error);

            string token = tokenGenerator.GenerateToken(result.UserId, result.Email, result.Roles);

            return Ok(new { Token = token});
        }
    }
}
