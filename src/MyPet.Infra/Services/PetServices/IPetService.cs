using MyPet.Application.Dtos.PetDtos;

namespace MyPet.Services.TutorServices
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetPets(int? page, int? row, string? orderBy);
        Task<PetDto> GetPet(int id);
        Task<PetDtoWithTutor> GetPetWithTutor(int id);
        Task<PetDto> CreatePet(CreatePetDto createPetDto);
        Task<bool> UpdatePet(int id, CreatePetDto createPetDto);
        Task<bool> DeletePet(int id);
    }
}
