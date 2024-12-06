using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorApp00.Models;

namespace BlazorApp00.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConsumoApi");
        }

        // Autenticar usuario y obtener token JWT
        public async Task<string?> Authenticate(string username, string password)
        {
            var request = new AuthenticateRequest { Username = username, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await
            _httpClient.PostAsync("api/Users/authenticate", content);

            if (!response.IsSuccessStatusCode) 
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthenticateResponse>(responseContent, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return authResponse?.Token;
        }

        // Obtener usuarios protegidos
        public async Task<IEnumerable<User>> GetAllUsers(string token)
        {
            // Agregar encabezado de autorización dinámicamente
            _httpClient.DefaultRequestHeaders.Authorization = new
            AuthenticationHeaderValue("Bearer", token);

            var response = await
            _httpClient.GetAsync("api/Users");

            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo obtener la lista de usuarios.");

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<User>>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<User>();
        }
    }
}
