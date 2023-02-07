using MyPet.Application.Dtos.PetDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetPets(int? page, int? row, string? orderBy);
        Task<PetDto> GetPet(int id);
        Task<PetDtoWithTutor> GetPetWithTutor(int id);
        Task<PetDto> CreatePet(CreatePetDto createPetDto);
        Task UpdatePet(CreatePetDto createPetDto);
        Task DeletePet(PetDto petDto);
    }
}
