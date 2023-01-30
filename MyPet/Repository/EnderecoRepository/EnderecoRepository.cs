using AutoMapper;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;
using MyPet.Repository.Context;
using MyPet.Repository.Interfaces;
using System.Runtime.ConstrainedExecution;

namespace MyPet.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IMyPetContext _context;
        private readonly IMapper _mapper;

        public EnderecoRepository(IMyPetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Endereco CreateEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            
            return endereco;
        }

        public List<Endereco> GetEnderecos()
        {
            return _context.Enderecos.ToList();
        }

        public Endereco GetEnderecosByCep(string Cep)
        {
            var cep = Cep.Insert(5, "-");
            var endereco = _context.Enderecos.FirstOrDefault(e => e.Cep == cep);
            
            return endereco;
        }

        public Endereco GetEnderecoById(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.EnderecoId == id);

            if (endereco == null)
            {
                return null;
            }

            return endereco;
        }

        public void DeleteEndereco(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(c => c.EnderecoId == id);
           
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
        }
    }
}
