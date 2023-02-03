using MyPet.Domain.Entidades;

namespace MyPet.Services.EnderecoServices
{
    public interface IEnderecoService
    {
        Task<object> GetAdress(string latitude, string longitude);
    }
}
