using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Helpers
{
    public class JwtServices
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly int expirationMinutes;

        public JwtServices()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            this.issuer = configuration["JwtSettings:Issuer"];
            this.audience = configuration["JwtSettings:Audience"];
            this.secretKey = configuration["JwtSettings:SecretKey"];
            this.expirationMinutes = Int32.Parse(configuration["JwtSettings:ExpirationMinutes"]);
        }

        public string GenerateToken(string userId, string userEmail, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim(ClaimTypes.Role, role),
                // Add additional claims as needed
            }),
                Expires = DateTime.UtcNow.AddMinutes(this.expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = this.issuer,
                Audience = this.audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
