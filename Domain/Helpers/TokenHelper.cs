using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace Domain.Helpers
{

    public static class TokenHelper
    {
        public static IConfiguration _configuration { get; }

        static TokenHelper() =>
            _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        public static string GenerateAccessToken(User user)
        {
            var mySecretKey = Encoding.UTF8.GetBytes(_configuration["AuthenticationOptions:SecretKey"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(mySecretKey),
                SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["AuthenticationOptions:Issuer"],
                Audience = _configuration["AuthenticationOptions:Audience"],
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("IsActive",user.IsActive.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                })
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }

        public static string GenerateRefreshToken()
        {
            var jwtToken = Guid.NewGuid();
            return jwtToken.ToString();
        }
    }
}
