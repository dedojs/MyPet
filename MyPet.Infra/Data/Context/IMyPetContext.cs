using Microsoft.EntityFrameworkCore;
using MyPet.Domain.Entidades;

namespace MyPet.Infra.Data.Context
{
    public interface IMyPetContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public Task<int> SaveChangesAsync();

    }
}
