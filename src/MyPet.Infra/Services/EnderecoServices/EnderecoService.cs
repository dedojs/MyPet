using AutoMapper;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Services.EnderecoServices;
using System.Net.Http.Json;

namespace MyPet.Services.TutorServices
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private const string _baseUrl = "https://nominatim.openstreetmap.org/";

        public EnderecoService(HttpClient client, IEnderecoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<IEnumerable<EnderecoDto>> GetEnderecos(int? page, int? row, string? orderBy)
        {
            var listEnderecos = await _repository.GetEnderecos(page, row, orderBy);

            var listEnderecosDto = listEnderecos.Select(e => _mapper.Map<EnderecoDto>(e));

            return listEnderecosDto;
        }

        public async Task<EnderecoDto> CreateEndereco(CreateEnderecoDto createEnderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(createEnderecoDto);

            var enderecoCriado = await _repository.CreateEndereco(endereco);

            var enderecoDto = _mapper.Map<EnderecoDto>(enderecoCriado);

            return enderecoDto;
        }

        public async Task<EnderecoDto> GetEnderecosByCep(string Cep)
        {
            var cep = Cep.Insert(5, "-");
            var endereco = await _repository.GetEnderecosByCep(cep);

            var enderecoDto = _mapper.Map<EnderecoDto>(endereco);

            return enderecoDto;
        }

        public async Task<EnderecoDto> GetEnderecoById(int id)
        {
            var endereco = await _repository.GetEnderecoById(id);

            if (endereco == null)
            {
                return null;
            }

            var enderecoDto = _mapper.Map<EnderecoDto>(endereco);

            return enderecoDto;
        }

        public async Task DeleteEndereco(int id)
        {
            var endereco = await _repository.GetEnderecoById(id);
            await _repository.DeleteEndereco(endereco);
        }
    }
}
