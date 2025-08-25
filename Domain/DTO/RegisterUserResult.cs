namespace Domain.DTO
{
    public class RegisterUserResult
    {
        public string? UserId { get; set; }
        public bool Success { get; init; }
        public string? Error { get; init; }
    }
}
