using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Application.Dtos.PetDtos;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Controllers;
using MyPet.Domain.Entidades;
using MyPet.Services.EnderecoServices;
using MyPet.Services.TutorServices;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using static QRCoder.PayloadGenerator;

namespace MyPet.Tests
{
    public class TutorControllerTest
    {
        [Fact(DisplayName = "Retorna a lista todos dos Tutores")]
        public async Task Retorna_ComSucesso_TodosOsTutoresAsync()
        {
            var repository = new Mock<ITutorService>();
            var enderecoRepository = new Mock<IEnderecoService>();
            var listTutores = new List<TutorDtoSimple>()
            {
                new TutorDtoSimple { TutorId = 1, Nome = "Andre", Email = "andre@gmail.com", Cep = "45028125"},
                new TutorDtoSimple { TutorId = 2, Nome = "Luisa", Email = "luisa@gmail.com", Cep = "45028674"},
                new TutorDtoSimple { TutorId = 3, Nome = "Lara", Email = "lara@gmail.com", Cep = "45028674"},
                new TutorDtoSimple { TutorId = 4, Nome = "Livia", Email = "livia@gmail.com", Cep = "45028125"}
            };

            repository.Setup(t => t.GetTutores(4, 1, "name"))
                .Returns(Task.FromResult(listTutores.AsEnumerable()));

            var tutorController = new TutorController(repository.Object, enderecoRepository.Object);

            var allTutores = await tutorController.GetTutores(4, 1, "name");

            repository.Verify(p => p.GetTutores(4, 1, "name"), Times.Once);
            allTutores.As<ObjectResult>().Value.As<List<TutorDtoSimple>>().Should().BeEquivalentTo(listTutores);
            allTutores.As<ObjectResult>().Value.As<List<TutorDtoSimple>>().Should().HaveCount(4);
            allTutores.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Theory(DisplayName = "Retorna o Tutor informado através de seu ID")]
        [InlineData(1)]
        public async Task Retorna_ComSucesso_OPetInformadoAsync(int idEntry)
        {
            var pet = new Pet()
            {
                PetId = 5,
                Nome = "Mini",
                Porte = "Pequeno",
                Raca = "Gato",
                TutorId = idEntry,
                DataNascimento = new DateTime(2012, 01, 31).Date
            };

            var endereco = new Endereco()
            {
                EnderecoId = idEntry,
                Cep = "45028125",
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var tutorDto = new TutorDto()
            {
                TutorId = idEntry,
                Nome = "Andre",
                Email = "teste@gmail.com",
                Cep = "45028125",
                Pets= new List<Pet>()
                {
                    pet,
                },
                Endereco = endereco
            };
            

            var repository = new Mock<ITutorService>();
            var enderecoRepository = new Mock<IEnderecoService>();

            repository.Setup(p => p.GetTutor(idEntry)).Returns(Task.FromResult(tutorDto));

            var tutorController = new TutorController(repository.Object, enderecoRepository.Object);
            var tutorResponse = await tutorController.GetTutor(idEntry);

            repository.Verify(p => p.GetTutor(idEntry), Times.Once);
            tutorResponse.As<ObjectResult>().Value.As<TutorDto>().Should().BeEquivalentTo(tutorDto);
            tutorResponse.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Nome completo", "emailexistente@email.com", "41830595", "123456")]
        [InlineData("", "invalido@email.com", "41830595", "123456")]
        [InlineData("Nome completo", "emailValido@email.com", "000000", "123456")]
        public async Task CriarTutor_Retornando_BadRequest_ComDadosInvalidos(string name, string email, string cep, string password)
        {
            var endereco = new Endereco()
            {
                EnderecoId = 1,
                Cep = "45028125",
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var tutorDto = new TutorDto()
            {
                TutorId = 1,
                Nome = "Andre",
                Email = "teste@gmail.com",
                Cep = "45028125",
                Endereco = endereco
            };

            // Arrange
            var request = new CreateTutorDto { Nome = name, Email = email, Cep = cep, Password = password };
            var mockService = new Mock<ITutorService>();
            var mockEnderecoService = new Mock<IEnderecoService>();

            mockService.Setup(x => x.GetTutorByEmail(email))
                .Returns(Task.FromResult(tutorDto));

            mockService.Setup(y => y.ValidateCep(cep))
                .Returns(Task.FromResult(new CreateEnderecoDto()));

            var controller = new TutorController(mockService.Object, mockEnderecoService.Object);

            // Act
            var result = await controller.CreateTutor(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("Full Name", "valid@email.com", "45028125", "123456")]
        public async Task CriarTutor_Retornando_CreatedAtActionResult_ComDadosValiddos(string name, string email, string cep, string password)
        {
            var endereco = new EnderecoDto()
            {
                EnderecoId = 1,
                Cep = "45028125",
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var createEndereco = new CreateEnderecoDto()
            {
                Cep = "45028125",
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var tutorDto = new TutorDto()
            {
                TutorId = 1,
                Nome = "Andre",
                Email = "teste@gmail.com",
                Cep = "45028125",
            };

            // Arrange
            var request = new CreateTutorDto { Nome = name, Email = email, Cep = cep, Password = password };
            var mockService = new Mock<ITutorService>();
            var mockEnderecoService = new Mock<IEnderecoService>();

            mockService.Setup(x => x.GetTutorByEmail(email))
                .ReturnsAsync(email == "existing@email.com" ? new TutorDto() : null);

            mockService.Setup(y => y.ValidateCep(cep))
                .Returns(Task.FromResult(createEndereco));

            mockEnderecoService.Setup(z => z.CreateEndereco(new CreateEnderecoDto()))
                .Returns(Task.FromResult(endereco));

            mockService.Setup(x => x.CreateTutor(It.IsAny<CreateTutorDto>()))
                .ReturnsAsync(tutorDto);

            var controller = new TutorController(mockService.Object, mockEnderecoService.Object);

            // Act
            var result = await controller.CreateTutor(request);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Theory(DisplayName = "Atualiza um Tutor com sucesso")]
        [InlineData(1, "teste@gmail.com", "45028125")]
        public async Task AtualizaUmTutorComSucesso(int IdEntry, string email, string cep)
        {
            var endereco = new EnderecoDto()
            {
                EnderecoId = 1,
                Cep = cep,
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var createEndereco = new CreateEnderecoDto()
            {
                Cep = cep,
                Bairro = "teste",
                Complemento = "teste",
                Logradouro = "teste",
                Localidade = "teste",
                Uf = "Teste"
            };

            var tutorUpdateDto = new CreateTutorDto()
            {
                Nome = "Andre",
                Email = email,
                Cep = cep,
                Password = "123456"
            };

            // Arrange
            var repository = new Mock<ITutorService>();
            var enderecoService = new Mock<IEnderecoService>();

            repository.Setup(y => y.ValidateCep(cep))
                .Returns(Task.FromResult(createEndereco));

            enderecoService.Setup(z => z.CreateEndereco(new CreateEnderecoDto()))
                .Returns(Task.FromResult(endereco));

            repository.Setup(t => t.UpdateTutor(IdEntry, tutorUpdateDto)).Returns(Task.FromResult(true));

            var tutorController = new TutorController(repository.Object, enderecoService.Object);

            // Act
            var tutorResponse = await tutorController.UpdateTutor(IdEntry, tutorUpdateDto);

            // Assert
            repository.Verify(p => p.UpdateTutor(IdEntry, tutorUpdateDto), Times.Once);
            tutorResponse.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Theory(DisplayName = "Deleta um Tutor com sucesso")]
        [InlineData(1)]
        public async Task Deleta_Tutor_ComSucesso(int IdEntry)
        {
            var repository = new Mock<ITutorService>();
            var enderecoService = new Mock<IEnderecoService>();

            repository.Setup(p => p.DeleteTutor(IdEntry)).Returns(Task.FromResult(true));

            var tutorController = new TutorController(repository.Object, enderecoService.Object);
            var tutorResponse = await tutorController.DeleteTutor(IdEntry);

            repository.Verify(p => p.DeleteTutor(IdEntry), Times.Once);
            tutorResponse.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }



    }
}