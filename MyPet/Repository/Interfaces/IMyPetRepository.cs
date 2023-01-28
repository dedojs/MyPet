using MyPet.Models.Dtos.Pet;
using MyPet.Models.Dtos.Tutor;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface IMyPetRepository
    {
        // C.R.U.D TUTOR
        List<TutorDto> GetTutores();
        Tutor GetTutor(int id);
        TutorDto CreateTutor(CreateTutorDto tutorDto);
        void UpdateTutor(Tutor tutor);
        void DeleteTutor(int id);

        // C.R.U.D PET
        List<PetDto> GetPets();
        Pet GetPet(int id);
        CreatePetDto CreatePet(CreatePetDto pet);
        void UpdatePet(Pet pet);
        void DeletePet(int id);

    }
}
