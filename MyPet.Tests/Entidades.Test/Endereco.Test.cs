using MyPet.Domain.Entidades;
using FluentAssertions;
using MyPet.Models.Dtos.TutorDtos;

namespace MyPet.Tests.Entidades.Test
{
    public class TestEndereco
    {
        [Fact]
        public void Test_Endereco_Instance_Sucess()
        {
            var endereco = new Endereco()
            {
                EnderecoId = 1,
                Cep = "45028674",
                Logradouro = "Candeias",
                Complemento = "perto dali",
                Bairro = "Candeias",
                Localidade = "Vitória da Conquista",
                Uf = "BA"
        };

            Assert.Equal(1, endereco.EnderecoId);
            Assert.Equal("45028674", endereco.Cep);
            endereco.Should().BeOfType<Endereco>();
        }
    }
}