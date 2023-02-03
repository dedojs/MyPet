using MyPet.Domain.Entidades;
using FluentAssertions;
using MyPet.Models.Dtos.TutorDtos;

namespace MyPet.Tests.Entidades.Test
{
    public class TestPet
    {
        [Fact]
        public void Test_Pet_Instance_Sucess()
        {
            var pet = new Pet()
            {
                PetId = 1,
                Nome = "Bob",
                TutorId = 1,
                Raca = "Beagle",
                Porte = "Pequeno"
            };

            Assert.Equal(1, pet.PetId);
            Assert.Equal("Bob", pet.Nome);
            pet.Should().BeOfType<Pet>();
        }
    }
}