

using System.Text.Json.Nodes;


namespace Web1.Services.Repository
{
    internal interface IRepository
    {
        Task<List<T>> GetDataAsync<T>() where T : class, new();
        Task<List<T>> GetDataAsync<T>(int id) where T : class, new();
        Task<JsonObject> GetDataAsync<T>(T data) where T : class, new();
        Task<bool> DeleteAsync<T>(int id) where T : class, new();
        Task<JsonObject> InsertAsync<T>(T item) where T : class, new();
        Task<bool> UpdateDataAsync<T>(int id, T item) where T : class, new();
    }
}
