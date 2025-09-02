using Domain.Enums;

namespace Domain.Requests
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = null!;
        public string Gamertag { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
