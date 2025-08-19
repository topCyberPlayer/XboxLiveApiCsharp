using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Domain.Responses
{
    public class LoginUserResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public string? UserId { get; init; }
        public string? Email { get; init; }
        public IList<string>? Roles { get; init; }
    }
}
