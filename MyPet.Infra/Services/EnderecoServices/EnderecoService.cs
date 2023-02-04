using MyPet.Domain.Entidades;
using MyPet.Services.EnderecoServices;
using System.Net.Http.Json;

namespace MyPet.Services.TutorServices
{
    public class EnderecoService : IEnderecoService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://nominatim.openstreetmap.org/";

        public EnderecoService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("User-Agent", "MyPet");
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<object> GetAdress(string latitude, string longitude)
        {
            var url = $"reverse?lat={latitude}&lon={longitude}&format=json";

            var response = await _client.GetAsync(url);
            
            if (response == null || !response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadFromJsonAsync<Nominatim>();

            return content;

        }
                
    }
}
