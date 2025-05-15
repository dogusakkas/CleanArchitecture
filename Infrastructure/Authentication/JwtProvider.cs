using Application.Abstractions;
using Application.Features.AuthFeatures.Commands.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;

        public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<LoginCommandResponse> CreateTokenAsync(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim("NameLastName", user.NameLastName)
            };

            DateTime expires = DateTime.Now.AddHours(1);

            JwtSecurityToken jwtSecurityToken = new
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore : DateTime.UtcNow,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = expires.AddMinutes(15);
            await _userManager.UpdateAsync(user);

            var loginDto = new LoginDto
            {
                Token = token,
                RefreshToken = refreshToken,
                RefreshTokenExpires = user.RefreshTokenExpires,
                Id = user.Id,
            };

            LoginCommandResponse response = new(loginDto);

            return response;
        }
    }
}
