namespace Domain.DTO
{
    public class RegisterUserResult
    {
        public RegisterUserResult(bool success, string? error, string? userId)
        {
            this.Success = success;
            this.Error = error;
            this.UserId = userId;
        }

        public string? UserId { get; private set; }
        public bool Success { get; private init; }
        public string? Error { get; private init; }
    }
}
