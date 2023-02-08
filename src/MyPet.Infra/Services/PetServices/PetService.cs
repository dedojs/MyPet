using AutoMapper;
using MyPet.Application.Dtos.PetDtos;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.PetRepository;
using MyPet.Infra.Data.Repository.TutorRepository;

namespace MyPet.Services.TutorServices
{
    public class PetService : IPetService
    {
        private readonly IEnderecoRepository _enderecoRepository;  
        private readonly ITutorRepository _tutorRepository;
        private readonly IPetRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private const string _baseUrl = "https://viacep.com.br/ws/";

        public PetService(HttpClient client, IPetRepository repository, IMapper mapper, ITutorRepository tutorRepository, IEnderecoRepository enderecoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
            _tutorRepository = tutorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<IEnumerable<PetDto>> GetPets(int? page, int? row, string? orderBy)
        {
            var listPets = await _repository.GetPets(page, row, orderBy);
            var listPetsDto = listPets.Select(e => _mapper.Map<PetDto>(e));
            return listPetsDto;
        }

        public async Task<PetDto> GetPet(int id)
        {
            var pet = await _repository.GetPet(id);

            if (pet == null)
            {
                return null;
            }

            var petDto = _mapper.Map<PetDto>(pet);

            return petDto;
        }

        public async Task<PetDtoWithTutor> GetPetWithTutor(int id)
        {
            var pet = await _repository.GetPet(id);

            if (pet == null)
            {
                return null;
            }

            var petDtoWithTutor = _mapper.Map<PetDtoWithTutor>(pet);

            var cep = pet.Tutor.Cep.Insert(5, "-");
            
            var endereco = await _enderecoRepository.GetEnderecosByCep(cep);

            petDtoWithTutor.Tutor.Endereco = endereco;

            return petDtoWithTutor;
        }

        public async Task<PetDto> CreatePet(CreatePetDto createPetDto)
        {
            var tutor = await _tutorRepository.GetTutor(createPetDto.TutorId);

            if (tutor == null)
            {
                return null;
            }

            var pet = _mapper.Map<Pet>(createPetDto);

            var petCreated = await _repository.CreatePet(pet);

            var petDto = _mapper.Map<PetDto>(petCreated);

            return petDto;
        }

        public async Task<bool> UpdatePet(int id, CreatePetDto createPetDto)
        {
            var findPet = await _repository.GetPet(id);

            if (findPet == null)
                return false;

            var petUpdate = _mapper.Map(createPetDto, findPet);

            await _repository.UpdatePet(petUpdate);

            return true;
        }

        public async Task<bool> DeletePet(int id)
        {
            var pet = await _repository.GetPet(id);

            if (pet == null)
                return false;

            await _repository.DeletePet(pet);

            return true;
        }
    }
}
