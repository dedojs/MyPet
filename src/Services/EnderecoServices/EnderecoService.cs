using MyPet.Domain.Entidades;
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
            _client.DefaultRequestHeaders.Add("User-Agent", "MyPet");
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<object> GetAdress(string latitude, string longitude)
        {
            var url = $"reverse?lat={latitude}&lon={longitude}&format=json";
            //var url2 = $"https://nominatim.openstreetmap.org/reverse?lat={latitude}&lon={longitude}&format=json";
            //Console.WriteLine($"{_baseUrl}{url}");
            Console.WriteLine(url);

            var response = await _client.GetAsync(url);
            Console.WriteLine($"Response: {response}");
            if (response == null || !response.IsSuccessStatusCode)
            {
                Console.WriteLine("A chamada na Nominatim falhou aqui no service");
                return null;
            }

            Console.WriteLine("Esta aqui no service");

            var content = await response.Content.ReadFromJsonAsync<Nominatim>();
            //Console.WriteLine(content);
            //var result = JsonConvert.DeserializeObject<dynamic>(content);
            //var result = await response.Content.ReadFromJsonAsync<Endereco>();
            //var adress = result.display_name;

            return content;

        }
                
    }
}
