using XblApp.API.Contracts.Users;
using XblApp.Application.InnerUseCases;

namespace XblApp.API.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(RegisterUserRequest request, UserUseCase useCase)
        {
            await useCase.RegisterUser(request.Gamertag, request.Email, request.Email);
            return Results.Ok();
        }

        private static async Task<IResult> Login(LoginUserRequest request, UserUseCase useCase)
        {
            await useCase.Login(request.Gamertag, request.Password);
            return Results.Ok();
        }
    }
}
