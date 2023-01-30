using MyPet.Models.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<Endereco> ValidateCep(string cep);
    }
}
