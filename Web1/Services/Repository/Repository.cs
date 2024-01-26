

using Web1.Models;

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace Web1.Services.Repository
{
    internal class Repository : IRepository
    {


        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;


        public Repository()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<bool> Ping()
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync(new Uri(Constants.ServerPaths.PING_URL));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ping http - " + e.Message);
                throw new Exception("Server Connsction Error");
            }

            return false;
        }

        public async Task<bool> DeleteAsync<T>(int id) where T : class, new()
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(GetPath<T>() + $"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error delete http - " + e.Message);
                throw new Exception("Server Connsction Error");
            }

            return false;
        }

        public async Task<List<T>> GetDataAsync<T>() where T : class, new()
        {
            var Items = new List<T>();

            Uri uri = new Uri(uriString: string.Format(GetPath<T>(), string.Empty));

            try
            {

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<List<T>> GetDataAsync<T>(int id) where T : class, new()
        {
            var Items = new List<T>();

            Uri uri = new Uri(uriString: string.Format(GetPath<T>() + $"/{id}", string.Empty));
            try
            {

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<JsonObject> GetDataAsync<T>(T data) where T : class, new()
        {
            try
            {
                StringContent content = new(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(GetPath<T>(), content);
                string cont = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var obj = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(cont, _serializerOptions);
                    return obj;
                }
                else
                {
                    var obj = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(cont, _serializerOptions);
                    throw new Exception((obj["title"]).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<JsonObject> InsertAsync<T>(T item) where T : class, new()
        {
            try
            {
                StringContent content = new(Newtonsoft.Json.JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
               
               // _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
               //                                                   Auth.Auth.Token);
                HttpResponseMessage response = await _client.PostAsync(GetPath<T>(), content);
                string cont = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var obj = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(cont, _serializerOptions);
                    return obj;
                }
                else
                {
                    var obj = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(cont, _serializerOptions);
                    throw new Exception((obj["title"]).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateDataAsync<T>(int id, T item) where T : class, new()
        {
            try
            {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item),
                                                             Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(GetPath<T>() + $"/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error update http - " + e.Message);
                throw new Exception("Server Connection Error");
            }

            return false;
        }

        private string GetPath<T>() where T : class, new()
        {
            string path = null;

            if (typeof(T) == typeof(LoginModel))
            {
                path = Constants.ServerPaths.Auth_URL;
            }
            else if (typeof(T) == typeof(RegisterModel))
            {
                path = Constants.ServerPaths.Register_URL;
            }

            return path;
        }
    }
}
