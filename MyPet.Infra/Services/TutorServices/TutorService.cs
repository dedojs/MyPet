using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;
using System.Net.Http.Json;

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

        public async Task<CreateEnderecoDto> ValidateCep(string cep)
        {
            var url = $"{cep}/json/";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<CreateEnderecoDto>();

            return result;
        }
    }
}
