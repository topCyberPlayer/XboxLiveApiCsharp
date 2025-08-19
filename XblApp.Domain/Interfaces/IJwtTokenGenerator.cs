﻿
namespace XblApp.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string? userId, string? email, IList<string>? roles);
    }
}