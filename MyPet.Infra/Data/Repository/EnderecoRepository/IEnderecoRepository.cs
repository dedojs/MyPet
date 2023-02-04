using MyPet.Application.Dtos.EnderecoDtos;

namespace MyPet.Infra.Data.Repository.EnderecoRepository
{
    public interface IEnderecoRepository
    {
        // Endereco
        IEnumerable<EnderecoDto> GetEnderecos(int? page, int? row, string? orderBy);
        EnderecoDto CreateEndereco(CreateEnderecoDto endereco);
        public EnderecoDto GetEnderecosByCep(string Cep);
        public EnderecoDto GetEnderecoById(int id);
        public void DeleteEndereco(int id);


    }
}
