using MyPet.Domain.Entidades;

namespace MyPet.Infra.Data.Repository.EnderecoRepository
{
    public interface IEnderecoRepository
    {
        // Endereco
        Task<IEnumerable<Endereco>> GetEnderecos(int? page, int? row, string? orderBy);
        Task<Endereco> CreateEndereco(Endereco createEndereco);
        Task<Endereco> GetEnderecosByCep(string Cep);
        Task<Endereco> GetEnderecoById(int id);
        Task DeleteEndereco(Endereco endereco);


    }
}
