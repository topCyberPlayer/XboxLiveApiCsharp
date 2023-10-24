using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Profile;

public partial class ProfileUserModelDb
{
    [Key]
    public long ProfileUserId { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public string Gamertag { get; set; } = null!;

    public int Gamerscore { get; set; }

    public string? Bio { get; set; }

    public DateTime DateTimeUpdate { get; set; }
}
