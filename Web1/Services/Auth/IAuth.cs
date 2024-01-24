

using Web1.Models;


namespace Web1.Services.Auth
{
    public interface IAuth
    {
        Task<(bool, (string, LoginModel))> AuthAsync(string email, string password);
    }
}
