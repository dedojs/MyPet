using MyPet.Application.Dtos.PetDtos;

namespace MyPet.Infra.Data.Repository.PetRepository;

public interface IPetRepository
{
    // C.R.U.D PET
    IEnumerable<PetDto> GetPets(int? page, int? row, string? orderBy);
    PetDto GetPet(int id);
    PetDtoWithTutor GetPetWithTutor(int id);
    PetDto CreatePet(CreatePetDto pet);
    void UpdatePet(int id, CreatePetDto pet);
    void DeletePet(int id);

}
