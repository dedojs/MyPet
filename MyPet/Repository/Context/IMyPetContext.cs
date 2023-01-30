using Microsoft.EntityFrameworkCore;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Context
{
    public interface IMyPetContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public int SaveChanges();

    }
}
