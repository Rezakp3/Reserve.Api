using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace Domain.Helpers
{
    public class Tokens
    {
        public string Accesstoken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }

    public static class TokenHelper
    {
        public static IConfiguration Config { get; }

        static TokenHelper() =>
            Config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();


        public static Tokens GenerateTokens(User user)
        {
            return new Tokens()
            {
                Accesstoken = GenerateAccessToken(user),
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDate = DateTime.Now.AddDays(30),
            };
        }


        public static string GenerateAccessToken(User user)
        {
            var mySecretKey = Encoding.UTF8.GetBytes(Config.GetSection("JwtSecret").ToString());

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(mySecretKey),
                SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = Config["Daya"],
                Audience = Config["Daya"],
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new[]
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
