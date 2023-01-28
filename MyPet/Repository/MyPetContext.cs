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
                
                
        }
       
    }
}
