using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XblApp.API.Test
{
    public static class JwtProfiles
    {
        public const string Default = "Default";
        public const string Warehouse = "Warehouse";
        public const string PvzApi = "PvzApi";
        public const string WrongKey = "WrongKey";
    }

    public class JwtTokenSettings
    {
        public required string Secret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpiryMinutes { get; set; }
    }

    public class JwtTokenFactory
    {
        private readonly Dictionary<string, JwtTokenSettings> _tokenProfiles;

        public JwtTokenFactory(IConfiguration configuration)
        {
            _tokenProfiles = new Dictionary<string, JwtTokenSettings>
            {
                ["Default"] = new JwtTokenSettings
                {
                    Secret = configuration["JwtSettings:Secret"],
                    Issuer = configuration["JwtSettings:Issuer"],
                    Audience = configuration["JwtSettings:Audience"],
                    ExpiryMinutes = int.Parse(configuration["JwtSettings:ExpiryMinutes"])
                },
                ["Warehouse"] = new JwtTokenSettings
                {
                    Secret = configuration["JwtSettings:WarehouseSecret"],
                    Issuer = configuration["JwtSettings:Issuer"],
                    Audience = configuration["JwtSettings:Audience"],
                    ExpiryMinutes = int.Parse(configuration["JwtSettings:WarehouseExpiryMinutes"])
                },
                ["PvzApi"] = new JwtTokenSettings
                {
                    Secret = configuration["PvzApiSettings:Secret"],
                    Issuer = configuration["PvzApiSettings:Issuer"],
                    Audience = configuration["JwtSettings:Audience"],
                    ExpiryMinutes = int.Parse(configuration["PvzApiSettings:ExpiryMinutes"])
                },
                ["WrongKey"] = new JwtTokenSettings
                {
                    Secret = "WRONG_SECRET_KEY_ABUBA_HEHEHE_SUKA",
                    Issuer = "wrong-issuer",
                    Audience = "wrong-audience",
                    ExpiryMinutes = 20
                }
            };
        }

        public string GenerateToken(string profile = "Default", string userId = "69")
        {
            if (!_tokenProfiles.TryGetValue(profile, out var settings))
                throw new ArgumentException($"Profile '{profile}' not found in JWT settings.");

            byte[]? key = Encoding.UTF8.GetBytes(settings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.NameIdentifier, "Lena Golovach"),
                    new Claim(ClaimTypes.Name, "user@example.com"),
                    new Claim("pvzId","123")
                ]),
                Expires = DateTime.UtcNow.AddMinutes(settings.ExpiryMinutes),
                Issuer = settings.Issuer,
                Audience = settings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string? jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
