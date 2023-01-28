using Microsoft.EntityFrameworkCore;
using MyPet.Models.Entidades;
using MyPet.Repository.Interfaces;

namespace MyPet.Repository
{
    public class MyPetContext : DbContext, IMyPetContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Tutor> Tutores { get; set; }

        public MyPetContext(DbContextOptions<MyPetContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=MyPet;User=sa;Password=Password12!;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutor>()
                .HasMany(t => t.Pets)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);

            modelBuilder.Entity<Tutor>()
                .HasData(
                    new Tutor(1, "Andre", "andre@gmail.com", "45028-125", "123456"),
                    new Tutor(2, "Luisa", "luisa@gmail.com", "45028-674", "789654"),
                    new Tutor(3, "Lara", "lara@gmail.com", "41250-330", "147852"),
                    new Tutor(4, "Livia", "livia@gmail.com", "45028618", "987654")
                );

            modelBuilder.Entity<Pet>()
                .HasData(
                    new Pet(1, "July", "Pequeno", "Cão", 1),
                    new Pet(2, "Bob", "Pequeno", "Cachorro", 2),
                    new Pet(3, "Bisteca", "´Médio", "Cachorro", 3),
                    new Pet(4, "Alecrim", "Pequeno", "Gato", 4),
                    new Pet(5, "Mini", "Pequeno", "Gato", 4)
                );
            
        }
       
    }
}
