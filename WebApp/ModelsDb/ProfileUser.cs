using System;
using System.Collections.Generic;

namespace WebApp.ModelsDb;

public partial class ProfileUserTable
{
    public long ProfileUserId { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public string Gamertag { get; set; } = null!;

    public int Gamerscore { get; set; }

    public string? Bio { get; set; }

    public DateTime DateTimeUpdate { get; set; }
}
