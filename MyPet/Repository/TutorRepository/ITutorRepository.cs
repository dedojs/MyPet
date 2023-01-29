using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface ITutorRepository
    {
        // C.R.U.D TUTOR
        List<TutorDto> GetTutores();
        TutorDto GetTutor(int id);
        TutorDto CreateTutor(CreateTutorDto tutorDto);
        void UpdateTutor(int id, CreateTutorDto tutorDto);
        void DeleteTutor(int id);

    }
}
