using MyPet.Domain.Entidades;
using FluentAssertions;
using MyPet.Models.Dtos.TutorDtos;

namespace MyPet.Tests.Entidades.Test
{
    public class TestTutor
    {
        [Fact]
        public void Test_Tutor_Instance_Sucess()
        {
            var tutor = new Tutor()
            {
                TutorId = 1,
                Nome = "Andre Luis",
                Email = "andre@teste.com",
                Cep = "45028674",
                Password = "123456"
            };

            Assert.Equal(1, tutor.TutorId);
            Assert.Equal("Andre Luis", tutor.Nome);
            tutor.Should().BeOfType<Tutor>();
        }
    }
}