using MyPet.Models.Dtos.Pet;
using MyPet.Models.Dtos.Tutor;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface IMyPetRepository
    {
        // C.R.U.D TUTOR
        List<Tutor> GetTutores();
        Tutor GetTutor(int id);
        TutorDto CreateTutor(CreateTutorDto tutorDto);
        void UpdateTutor(Tutor tutor);
        void DeleteTutor(int id);

        // C.R.U.D PET
        List<Pet> GetPets();
        Pet GetPet(int id);
        PetDto CreatePet(PetDto pet);
        void UpdatePet(Pet pet);
        void DeletePet(int id);

    }
}
