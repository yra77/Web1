

using System.IdentityModel.Tokens.Jwt;


namespace Web1_Server.Services.JwtService
{
    public interface IJwtToken
    {
        JwtSecurityToken CreateJwtToken(string data);
    }
}
