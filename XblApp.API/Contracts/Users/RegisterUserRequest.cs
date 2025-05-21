using System.ComponentModel.DataAnnotations;

namespace XblApp.API.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string Gamertag,
        [Required] string Email,
        [Required] string Password);
}
