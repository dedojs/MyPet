using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<CreateEnderecoDto> ValidateCep(string cep);

        Task<IEnumerable<TutorDtoSimple>> GetTutores(int? page, int? row, string? orderBy);
        Task<TutorDto> GetTutor(int id);
        Task<TutorDto> GetTutorByEmail(string email);
        Task<TutorDto> CreateTutor(CreateTutorDto tutorDto);
        Task<bool> UpdateTutor(int id, CreateTutorDto crateTutorDto);
        Task<bool> DeleteTutor(int id);
        Task<Tutor> ValidadeLoginTutor(TutorLoginDto tutorLogin);
        Task<TutorDto> GetSimpleTutor(int id);
    }
}
