using System;
using System.Collections.Generic;

namespace WebApp.ModelsDb;

public partial class TokenXstTable
{
    public string Xuid { get; set; } = null!;

    public DateTime IssueInstant { get; set; }

    public DateTime NotAfter { get; set; }

    public string Token { get; set; } = null!;
}
