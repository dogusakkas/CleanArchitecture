using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Login
{

    public class LoginDto
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
        public DateTime? RefreshTokenExpires { get; init; }
        public string Id { get; init; }
        
    }
    public sealed record LoginCommandResponse(LoginDto loginDto)
    {
    }
}
