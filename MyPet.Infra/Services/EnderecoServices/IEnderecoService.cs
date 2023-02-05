using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Services.EnderecoServices
{
    public interface IEnderecoService
    {
        Task<object> GetAdress(string latitude, string longitude);

        Task<IEnumerable<EnderecoDto>> GetEnderecos(int? page, int? row, string? orderBy);
        Task<EnderecoDto> CreateEndereco(CreateEnderecoDto createEnderecoDto);
        Task<EnderecoDto> GetEnderecosByCep(string Cep);
        Task<EnderecoDto> GetEnderecoById(int id);
        Task DeleteEndereco(int id);
    }
}
