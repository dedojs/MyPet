using MyPet.Models.Entidades;

namespace MyPet.Services.TutorServices
{
    public class TutorService : ITutorService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://viacep.com.br/ws/";

        public TutorService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<object> ValidateCep(string cep)
        {
            var url = $"{cep}/json/";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return default(object);
            }

            var result = await response.Content.ReadFromJsonAsync<object>();
            
            return result;
        }
    }
}
