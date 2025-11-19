using DastakWebApi.ConfigModels;
using DastakWebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DastakWebApi.HelperMethods
{
    public class JwtHandler
    {
        private readonly JwtConfigurations _jwtConfig;
        public JwtHandler(IOptions<JwtConfigurations> configuration)
        {
            _jwtConfig = configuration.Value;
        }

        public string GetToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);

            //1st way to create web token
            var tokenOptions = GetTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            ////2nd way to create web token
            //var tokenOptions = GetSecurityTokenDiscriptor(signingCredentials, claims);
            //var handler = new JwtSecurityTokenHandler();
            //return handler.WriteToken(handler.CreateToken(tokenOptions));

        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(User user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Email.ToString())
            };
        }
        private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfig.ExpirationTimeInMinutes)),
                claims: claims,
                signingCredentials: signingCredentials
                );
        }
    }
}
