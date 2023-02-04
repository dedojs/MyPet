using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<CreateEnderecoDto> ValidateCep(string cep);
    }
}
