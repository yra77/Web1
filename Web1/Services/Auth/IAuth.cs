

using Web1.Models;


namespace Web1.Services.Auth
{
    public interface IAuth
    {
        Task<OkResponse> AuthAsync(string email, string password);
        Task<OkResponse> InsertAsync(RegisterModel profile);
    }
}
