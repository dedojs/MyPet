using MyPet.Models.Entidades;

namespace MyPet.Services.EnderecoServices
{
    public interface IEnderecoService
    {
        Task<Object> GetAdress(double latitude, double longitude);
    }
}
