using MyPet.Models.Dtos.PetDto;

namespace MyPet.Repository.Interfaces
{
    public interface IPetRepository
    {
        // C.R.U.D PET
        List<PetDto> GetPets();
        PetDto GetPet(int id);
        PetDto CreatePet(CreatePetDto pet);
        void UpdatePet(int id, CreatePetDto pet);
        void DeletePet(int id);

    }
}
