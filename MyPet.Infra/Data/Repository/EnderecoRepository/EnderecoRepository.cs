using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Context;


namespace MyPet.Infra.Data.Repository.EnderecoRepository
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

        public async Task<Endereco> CreateEndereco(Endereco createEndereco)
        {
            _context.Enderecos.Add(createEndereco);
            await _context.SaveChangesAsync();

            return createEndereco;
        }

        public async Task<IEnumerable<Endereco>> GetEnderecos(int? page, int? row, string? orderBy)
        {
            if (page == null)
                page = 1;
            if (row == null)
                row = 20;
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "id";

            var listDataEnderecos = _context.Enderecos.OrderBy(t => t.EnderecoId);

            if (orderBy == "name")
                listDataEnderecos = _context.Enderecos.OrderBy(t => t.Localidade);

            var listEnderecosFilter = listDataEnderecos.Skip((page.Value - 1) * row.Value).Take(row.Value);

            return await listEnderecosFilter.ToListAsync();
        }

        public async Task<Endereco> GetEnderecosByCep(string cep)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Cep == cep);

            return endereco;
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.EnderecoId == id);

            if (endereco == null)
            {
                return null;
            }

            return endereco;
        }

        public async Task DeleteEndereco(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }
}
