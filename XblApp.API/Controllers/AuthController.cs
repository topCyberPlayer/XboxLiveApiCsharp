using Application.InnerUseCases;
using Domain.DTO;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XblApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController() : ControllerBase
    {
        [HttpPost("register")]
        public async Task Register([FromBody] RegisterUserRequest model,
                                                  [FromServices] RegisterUserUseCase registerUserUseCase)
        {
            await registerUserUseCase.RegisterUser(model.Gamertag, model.Email, model.Password);
        }

        [HttpPost("login")]
        public Task Login([FromBody] LoginUserRequest model, [FromServices] LoginUserUseCase loginUserUseCase)
        {
            return loginUserUseCase.LoginUser(model.Gamertag, model.Password);
        }

        [HttpPost("refresh")]
        public Task<TokenDTO> RefreshToken(TokenDTO dto, [FromServices] LoginUserUseCase loginUserUseCase)
        {
            return loginUserUseCase.RefreshTokenAsync(dto);
        }
    }
}
