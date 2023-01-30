using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface IEnderecoRepository
    {
        // Endereco
        List<Endereco> GetEnderecos();
        Endereco CreateEndereco(Endereco endereco);
        public Endereco GetEnderecosByCep(string Cep);
        public Endereco GetEnderecoById(int id);
        public void DeleteEndereco(int id);


    }
}
