using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<CreateEnderecoDto> ValidateCep(string cep);

        Task<IEnumerable<TutorDto>> GetTutores(int? page, int? row, string? orderBy);
        Task<TutorDto> GetTutor(int id);
        Task<TutorDto> CreateTutor(CreateTutorDto tutorDto);
        Task UpdateTutor(CreateTutorDto tutorDto);
        Task DeleteTutor(TutorDto tutorDto);
        Task<Tutor> ValidadeLoginTutor(TutorLoginDto tutorLogin);
    }
}
