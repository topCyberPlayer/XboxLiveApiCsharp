using System;
using System.Collections.Generic;

namespace WebApp.Data;

public partial class TokenOauth2Table
{
    public string UserId { get; set; } = null!;

    public string AspNetUserId { get; set; } = null!;

    public string? TokenType { get; set; }

    public int ExpiresIn { get; set; }

    public string? Scope { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? AuthenticationToken { get; set; }

    public DateTime? Expires { get; set; }

    public DateTime? Issued { get; set; }
}
