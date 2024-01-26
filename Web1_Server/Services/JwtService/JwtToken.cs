

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Web1_Server.Services.JwtService
{
    public class JwtToken : IJwtToken
    {

        public JwtSecurityToken CreateJwtToken(string data)
        {
            List<Claim> claims = new() { new Claim(ClaimTypes.Email, data) };
            // создаем JWT-токен
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: Constants.JwtConstant.ISSUER,
                    audience: Constants.JwtConstant.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                    signingCredentials: new SigningCredentials(Constants.JwtConstant.GetSymmetricSecurityKey(),
                                                               SecurityAlgorithms.HmacSha256));

            return jwt;
        }
    }
}
