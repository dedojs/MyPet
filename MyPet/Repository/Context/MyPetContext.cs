using Microsoft.EntityFrameworkCore;
using MyPet.Models.Entidades;

namespace MyPet.Repository.Context
{
    public class MyPetContext : DbContext, IMyPetContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public MyPetContext(DbContextOptions<MyPetContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=MyPet;User=sa;Password=Password12!;TrustServerCertificate=True")
                .UseLazyLoadingProxies();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutor>()
                .HasMany(t => t.Pets)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);

            modelBuilder.Entity<Endereco>()
                .HasData(
                new Endereco
                {
                    EnderecoId = 1,
                    Cep = "45028674",
                    Logradouro = "Rua Erico Gonçalves Aguiar",
                    Complemento = "",
                    Bairro = "Candeias",
                    Localidade = "Vitória da Conquista",
                    Uf = "BA"
                }
                );

            modelBuilder.Entity<Tutor>()
                .HasData(
                    new Tutor { TutorId = 1, Nome = "Andre", Email = "andre@gmail.com", Cep = "45028125", Password = "123456" },
                    new Tutor { TutorId = 2, Nome = "Luisa", Email = "luisa@gmail.com", Cep = "45028674", Password = "789654" },
                    new Tutor { TutorId = 3, Nome = "Lara", Email = "lara@gmail.com", Cep = "45028674", Password = "147852" },
                    new Tutor { TutorId = 4, Nome = "Livia", Email = "livia@gmail.com", Cep = "45028125", Password = "987654" }
                );

            modelBuilder.Entity<Pet>()
                .HasData(
                    new Pet
                    {
                        PetId = 1,
                        Nome = "July",
                        Porte = "Pequeno",
                        Raca = "Cão",
                        TutorId = 1,
                        DataNascimento = new DateTime(2020, 01, 10)
                    },
                    new Pet
                    {
                        PetId = 2,
                        Nome = "Bob",
                        Porte = "Pequeno",
                        Raca = "Cachorro",
                        TutorId = 2,
                        DataNascimento = new DateTime(2015, 01, 15)
                    },
                    new Pet
                    {
                        PetId = 3,
                        Nome = "Bisteca",
                        Porte = "´Médio",
                        Raca = "Cachorro",
                        TutorId = 3,
                        DataNascimento = new DateTime(2010, 01, 01)
                    },
                    new Pet
                    {
                        PetId = 4,
                        Nome = "Alecrim",
                        Porte = "Pequeno",
                        Raca = "Gato",
                        TutorId = 4,
                        DataNascimento = new DateTime(2021, 01, 20)
                    },
                    new Pet
                    {
                        PetId = 5,
                        Nome = "Mini",
                        Porte = "Pequeno",
                        Raca = "Gato",
                        TutorId = 4,
                        DataNascimento = new DateTime(2012, 01, 31)
                    }
                );

        }

    }
}
