

using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Web1_Server.Constants
{
    public class JwtConstant
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
