using System.ComponentModel.DataAnnotations;

namespace XblApp.API.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Gamertag,
        [Required] string Password);
}
