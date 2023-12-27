using BusinessObjects.DTO.AuthDTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Services.Helpers
{
    public class JwtServices
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly int expirationMinutes;
        private readonly ILogger<JwtServices> _logger;

        public JwtServices(ILogger<JwtServices> logger)
        {
            this._logger = logger;

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            this.issuer = configuration["JwtSettings:Issuer"];
            this.audience = configuration["JwtSettings:Audience"];
            this.secretKey = configuration["JwtSettings:SecretKey"];
            this.expirationMinutes = Int32.Parse(configuration["JwtSettings:ExpirationMinutes"]);
        }

        public string GenerateToken(string accountId, string email, string role)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("accountId, email, and role cannot be null or empty");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("accountId", accountId),
                new Claim("email", email),
                new Claim("role", role),
            }),
                Expires = DateTime.UtcNow.AddMinutes(this.expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = this.issuer,
                Audience = this.audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ResultValidateTokenDto VerifyToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = this.issuer,
                ValidAudience = this.audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                var jwtSecurityToken = validatedToken as JwtSecurityToken;
                var payload = jwtSecurityToken?.Payload.SerializeToJson();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var securityToken = tokenHandler.ReadToken(authToken) as JwtSecurityToken;

            // Mapping data to response payload
            ResultValidateTokenDto resultDto = new ResultValidateTokenDto
            {
                AccountId = securityToken.Claims.First(claim => claim.Type == "accountId").Value,
                Email = securityToken.Claims.First(claim => claim.Type == "email").Value,
                Role = securityToken.Claims.First(claim => claim.Type == "role").Value,
            };
            return resultDto;
        }
    }
}
