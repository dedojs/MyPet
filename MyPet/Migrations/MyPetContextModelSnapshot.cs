﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPet.Repository;

#nullable disable

namespace MyPet.Migrations
{
    [DbContext(typeof(MyPetContext))]
    partial class MyPetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyPet.Models.Entidades.Pet", b =>
                {
                    b.Property<int>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PetId"), 1L, 1);

                    b.Property<int>("HashCode")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Porte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Raca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.HasKey("PetId");

                    b.HasIndex("TutorId");

                    b.ToTable("Pets");

                    b.HasData(
                        new
                        {
                            PetId = 1,
                            HashCode = 0,
                            Nome = "July",
                            Porte = "Pequeno",
                            Raca = "Cão",
                            TutorId = 1
                        },
                        new
                        {
                            PetId = 2,
                            HashCode = 0,
                            Nome = "Bob",
                            Porte = "Pequeno",
                            Raca = "Cachorro",
                            TutorId = 2
                        },
                        new
                        {
                            PetId = 3,
                            HashCode = 0,
                            Nome = "Bisteca",
                            Porte = "´Médio",
                            Raca = "Cachorro",
                            TutorId = 3
                        },
                        new
                        {
                            PetId = 4,
                            HashCode = 0,
                            Nome = "Alecrim",
                            Porte = "Pequeno",
                            Raca = "Gato",
                            TutorId = 4
                        },
                        new
                        {
                            PetId = 5,
                            HashCode = 0,
                            Nome = "Mini",
                            Porte = "Pequeno",
                            Raca = "Gato",
                            TutorId = 4
                        });
                });

            modelBuilder.Entity("MyPet.Models.Entidades.Tutor", b =>
                {
                    b.Property<int>("TutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TutorId"), 1L, 1);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TutorId");

                    b.ToTable("Tutores");

                    b.HasData(
                        new
                        {
                            TutorId = 1,
                            Cep = "45028-125",
                            Email = "andre@gmail.com",
                            Nome = "Andre",
                            Password = "123456"
                        },
                        new
                        {
                            TutorId = 2,
                            Cep = "45028-674",
                            Email = "luisa@gmail.com",
                            Nome = "Luisa",
                            Password = "789654"
                        },
                        new
                        {
                            TutorId = 3,
                            Cep = "41250-330",
                            Email = "lara@gmail.com",
                            Nome = "Lara",
                            Password = "147852"
                        },
                        new
                        {
                            TutorId = 4,
                            Cep = "45028618",
                            Email = "livia@gmail.com",
                            Nome = "Livia",
                            Password = "987654"
                        });
                });

            modelBuilder.Entity("MyPet.Models.Entidades.Pet", b =>
                {
                    b.HasOne("MyPet.Models.Entidades.Tutor", "Tutor")
                        .WithMany("Pets")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("MyPet.Models.Entidades.Tutor", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
