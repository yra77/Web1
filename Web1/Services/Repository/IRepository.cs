﻿

using Web1.Models;


namespace Web1.Services.Repository
{
    internal interface IRepository
    {
        Task<List<T>> GetDataAsync<T>() where T : class, new();
        Task<List<T>> GetDataAsync<T>(int id) where T : class, new();
        Task<T> GetDataAsync<T>(LoginModel data) where T : class, new();
        Task<bool> DeleteAsync<T>(int id) where T : class, new();
        Task<bool> InsertAsync<T>(T item) where T : class, new();
        Task<bool> UpdateDataAsync<T>(int id, T item) where T : class, new();
    }
}
