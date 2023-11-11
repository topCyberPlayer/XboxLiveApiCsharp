using System.ComponentModel.DataAnnotations;

namespace WebApp.Services.Authentication;

public partial class TokenOauth2ModelDb
{
    [Key]
    public string UserId { get; set; } = null!;

    public string? AspNetUserId { get; set; }

    public string? TokenType { get; set; }

    public int ExpiresIn { get; set; }

    public string? Scope { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? AuthenticationToken { get; set; }

    public DateTime? Expires { get; set; }

    public DateTime? Issued { get; set; }
}

public partial class TokenXstsModelDb
{
    [Key]
    public string Xuid { get; set; } = null!;

    public string? AspNetUserId { get; set; }

    public DateTime IssueInstant { get; set; }

    public DateTime NotAfter { get; set; }

    public string? Token { get; set; }
}
