using Microsoft.EntityFrameworkCore;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Interfaces
{
    public interface IMyPetContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public int SaveChanges();

    }
}
