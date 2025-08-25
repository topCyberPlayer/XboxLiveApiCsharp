namespace Domain.Requests
{
    public class LoginUserRequest
    {
        public string Gamertag { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
