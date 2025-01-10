using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WBAPI.API.Infraestructure.OptionsSettings;
using WBAPI.API.Models;

namespace WBAPI.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Constructor that initializes the AuthService with JWT settings.
        /// </summary>
        /// <param name="jwtSettings">The JWT settings configured in app settings.</param>
        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Validates the login credentials and returns a JWT token if successful.
        /// </summary>
        /// <param name="username">The username of the user trying to log in.</param>
        /// <param name="password">The password of the user trying to log in.</param>
        /// <returns>A JWT token if login is successful.</returns>
        public AuthToken Login(string username, string password)
        {
            // Login logic ommited for brevity, assume validations succedded 
            return GenerateJwtToken(username);
        }

        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="username">The username for which the JWT is generated.</param>
        /// <returns>A JWT token as a string.</returns>
        private AuthToken GenerateJwtToken(string username)
        {
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryDurationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new AuthToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }
    }
}
