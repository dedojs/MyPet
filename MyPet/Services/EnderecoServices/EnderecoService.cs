using MyPet.Models.Entidades;
using MyPet.Services.EnderecoServices;
using Newtonsoft.Json;

namespace MyPet.Services.TutorServices
{
    public class EnderecoService : IEnderecoService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://nominatim.openstreetmap.org/";

        public EnderecoService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<object> GetAdress(double latitude, double longitude)
        {
            var url = $"reverse?format=json&lat={latitude}&lon={longitude}";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            var adress = result.display_name;

            return adress;

        }
    }
}
