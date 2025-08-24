namespace XblApp.Domain.Responses
{
    public class LoginUserResult
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
