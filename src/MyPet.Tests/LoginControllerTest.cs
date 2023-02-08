using AutoMapper;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Controllers;
using MyPet.Domain.Entidades;
using MyPet.Services.EnderecoServices;
using MyPet.Services.TutorServices;
using Newtonsoft.Json.Linq;
using System.Net;


namespace MyPet.Tests
{
    public class LoginControllerTest
    {
        [Fact]
        public async Task Login_Returns_OkResult()
        {
            var pet = new Pet()
            {
                PetId = 5,
                Nome = "Mini",
                Porte = "Pequeno",
                Raca = "Gato",
                TutorId = 1,
                DataNascimento = new DateTime(2012, 01, 31).Date
            };

            var tutor = new Tutor()
            {
                TutorId = 1,
                Nome = "Andre Luis",
                Cep = "45028674",
                Email = "tutor@example.com",
                Password = "password",
                Pets= new List<Pet>() { pet }
            };
            // Arrange
            var tutorLogin = new TutorLoginDto { Email = "tutor@example.com", Password = "password" };
            var repository = new Mock<ITutorService>();
            repository.Setup(x => x.ValidadeLoginTutor(tutorLogin)).ReturnsAsync(tutor);
            var controller = new LoginController(repository.Object);

            // Act
            var result = await controller.Login(tutorLogin);

            // Assert
            repository.Verify(p => p.ValidadeLoginTutor(tutorLogin), Times.Once);
            result.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Login_Returns_BadRequestResult()
        {
            // Arrange
            var tutorLogin = new TutorLoginDto { Email = "tutor@example.com", Password = "password" };
            var repository = new Mock<ITutorService>();
            repository.Setup(x => x.ValidadeLoginTutor(tutorLogin)).ReturnsAsync((Tutor)null);
            var controller = new LoginController(repository.Object);

            // Act
            var result = await controller.Login(tutorLogin);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var objectResult = (BadRequestObjectResult)result;
            Assert.Equal("Email ou Senha inválidos!", objectResult.Value);
        }

    }
}