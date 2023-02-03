using MyPet.Domain.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<Endereco> ValidateCep(string cep);
    }
}
