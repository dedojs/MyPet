using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface IMyPetRepository
    {
        // C.R.U.D TUTOR
        Tutor GetTutorores();
        Tutor GetTutor(int id);
        Tutor CreateTutor(Tutor tutor);
        void UpdateTutor(Tutor tutor);
        void DeleteTutor(int id);

        // C.R.U.D PET
        Pet GetPets();
        Pet GetPet(int id);
        Pet CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        void DeletePet(int id);

    }
}
