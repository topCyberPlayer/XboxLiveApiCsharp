namespace XblApp.Domain.Requests
{
    public class LoginRequest
    {
        public string Gamertag { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
