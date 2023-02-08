using MyPet.Domain.Entidades;

namespace MyPet.Infra.Data.Repository.TutorRepository
{
    public interface ITutorRepository
    {
        // C.R.U.D TUTOR
        Task<IEnumerable<Tutor>> GetTutores(int? page, int? row, string? orderBy);
        Task<Tutor> GetTutor(int id);
        Task<Tutor> GetTutorByEmail(string email);
        Task<Tutor> CreateTutor(Tutor tutor);
        Task UpdateTutor(Tutor tutor);
        Task DeleteTutor(Tutor tutor);
        Task<Tutor> ValidadeLoginTutor(Tutor tutor);
    }
}
