﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Models;

public partial class TokenOAuthModelDb
{
    [Key]
    [Required]
    public string? AspNetUserId { get; set; }
    public string? UserId { get; set; }
    public string? TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string? Scope { get; set; }    
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? AuthenticationToken { get; set; }
}

public partial class TokenXauModelDb
{
    [Key]
    [Required]
    public string? AspNetUserId { get; set; }
    public DateTime IssueInstant { get; set; }
    public DateTime NotAfter { get; set; }
    public string Token { get; set; }

    public string Uhs { get; set; }
}


public partial class TokenXstsModelDb
{
    [Key]
    [Required]
    public string? AspNetUserId { get; set; }
    public DateTime IssueInstant { get; set; }
    public DateTime NotAfter { get; set; }
    public string? Token { get; set; }

    public string? Xuid { get; set; }
    public string? Userhash { get; set; }
    public string? Gamertag { get; set; }
    public string? AgeGroup { get; set; }
    public string? Privileges { get; set; }
    public string? UserPrivileges { get; set; }
}
