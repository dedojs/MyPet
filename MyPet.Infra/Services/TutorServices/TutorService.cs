using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.TutorRepository;
using System.Net.Http.Json;

namespace MyPet.Services.TutorServices
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private const string _baseUrl = "https://viacep.com.br/ws/";

        private readonly IEnderecoRepository _enderecoRepository;

        public TutorService(HttpClient client, ITutorRepository repository, IMapper mapper, IEnderecoRepository enderecoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
            _enderecoRepository = enderecoRepository;
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

        public async Task<IEnumerable<TutorDto>> GetTutores(int? page, int? row, string? orderBy)
        {
            var listTutores = await _repository.GetTutores(page, row, orderBy);

            var listTutoresDto = listTutores.Select(e => _mapper.Map<TutorDto>(e));

            return listTutoresDto;
        }

        public async Task<TutorDto> GetTutor(int id)
        {
            var tutor = await _repository.GetTutor(id);

            if (tutor == null)
            {
                return null;
            }

            var cep = tutor.Cep.Insert(5, "-");
            var endereco = await _enderecoRepository.GetEnderecosByCep(cep);

            var tutorDto = _mapper.Map<TutorDto>(tutor);

            tutorDto.Endereco = endereco;

            return tutorDto;
        }

        public async Task<TutorDto> CreateTutor(CreateTutorDto createTutorDto)
        {
            var tutor = _mapper.Map<Tutor>(createTutorDto);
            
            var tutorCreated = await _repository.CreateTutor(tutor);

            var tutorDto = _mapper.Map<TutorDto>(tutorCreated);

            return tutorDto;
        }

        public async Task UpdateTutor(CreateTutorDto tutorDto)
        {
            var tutor = _mapper.Map<Tutor>(tutorDto);

            await _repository.UpdateTutor(tutor);
        }

        public async Task DeleteTutor(TutorDto tutorDto)
        {
            var tutor = _mapper.Map<Tutor>(tutorDto);

            await _repository.DeleteTutor(tutor);
        }

        public async Task<Tutor> ValidadeLoginTutor(TutorLoginDto tutorLogin)
        {
            var tutor = _mapper.Map<Tutor>(tutorLogin);

            var tutorValidate = await _repository.ValidadeLoginTutor(tutor);

            if (tutorValidate == null)
                return null;

            return tutorValidate;
        }
    }
}
