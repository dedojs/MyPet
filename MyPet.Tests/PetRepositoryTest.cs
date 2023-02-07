using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyPet.Application.Dtos.PetDtos;
using MyPet.Controllers;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Repository.PetRepository;
using MyPet.Services.TutorServices;
using System.Net;

namespace MyPet.Tests
{
    public class PetRepositoryTest
    {
        [Fact(DisplayName = "Retorna a lista todos os Pets")]
        public async Task RetornaComSucessoTodosOsPetsAsync()
        {
            var repository = new Mock<IPetService>();
            var listPets = new List<PetDto>()
            {
                new PetDto
                    {
                        PetId = 1,
                        Nome = "July",
                        Porte = "Pequeno",
                        Raca = "C�o",
                        TutorId = 1,
                        DataNascimento = new DateTime(2020, 01, 10).Date
                    },
                    new PetDto
                    {
                        PetId = 2,
                        Nome = "Bob",
                        Porte = "Pequeno",
                        Raca = "Cachorro",
                        TutorId = 2,
                        DataNascimento = new DateTime(2015, 01, 15).Date
                    },
                    new PetDto
                    {
                        PetId = 3,
                        Nome = "Bisteca",
                        Porte = "�M�dio",
                        Raca = "Cachorro",
                        TutorId = 3,
                        DataNascimento = new DateTime(2010, 01, 01).Date
                    },
                    new PetDto
                    {
                        PetId = 4,
                        Nome = "Alecrim",
                        Porte = "Pequeno",
                        Raca = "Gato",
                        TutorId = 4,
                        DataNascimento = new DateTime(2021, 01, 20).Date
                    },
                    new PetDto
                    {
                        PetId = 5,
                        Nome = "Mini",
                        Porte = "Pequeno",
                        Raca = "Gato",
                        TutorId = 4,
                        DataNascimento = new DateTime(2012, 01, 31).Date
                    }
            };

            repository.Setup(p => p.GetPets(5, 1, "name"))
                .Returns(Task.FromResult(listPets.AsEnumerable()));

            var petController = new PetController(repository.Object);

            var allPets = await petController.GetPets(5, 1, "name");

            repository.Verify(p => p.GetPets(5, 1, "name"), Times.Once);
            allPets.As<ObjectResult>().Value.As<List<PetDto>>().Should().BeEquivalentTo(listPets);
            allPets.As<ObjectResult>().Value.As<List<PetDto>>().Should().HaveCount(5);
            allPets.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Theory(DisplayName = "Retorna o pet informado atrav�s de seu ID")]
        [InlineData(5)]
        public async Task RetornaComSucessoOPetInformadoAsync(int idEntry)
        {
            var petDto = new PetDto()
            {
                PetId = 5,
                Nome = "Mini",
                Porte = "Pequeno",
                Raca = "Gato",
                TutorId = 4,
                DataNascimento = new DateTime(2012, 01, 31).Date
            };

            var repository = new Mock<IPetService>();

            repository.Setup(p => p.GetPet(idEntry)).Returns(Task.FromResult(petDto));

            var petController = new PetController(repository.Object);
            var petResponse = await petController.GetPet(idEntry);

            repository.Verify(p => p.GetPet(idEntry), Times.Once);
            petResponse.As<ObjectResult>().Value.As<PetDto>().Should().BeEquivalentTo(petDto);
            petResponse.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory(DisplayName = "Cria um Pet com sucesso")] 
        [InlineData(1, "Nome do Pet", "Porte do pet", "Ra�a do Pet", 1, "2022-03-02")]
        public async Task CriaUmPetComSucesso(int petIdEntry, string namePetEntry, string portePetEntry, string racaPEtEntry, int idTutorEntry, DateTime dateEntry)
        {
            var petCreateDto = new CreatePetDto()
            {
                Nome = namePetEntry,
                Porte = portePetEntry,
                Raca = racaPEtEntry,
                TutorId = idTutorEntry,
                DataNascimento = dateEntry
            };

            var petDto = new PetDto()
            {
                PetId = petIdEntry,
                Nome = namePetEntry,
                Porte = portePetEntry,
                Raca = racaPEtEntry,
                TutorId = idTutorEntry,
                DataNascimento = dateEntry
            };

            var repository = new Mock<IPetService>();

            repository.Setup(p => p.CreatePet(petCreateDto)).Returns(Task.FromResult(petDto));

            var petController = new PetController(repository.Object);
            var petResponse = await petController.CreatePet(petCreateDto);

            repository.Verify(p => p.CreatePet(petCreateDto), Times.Once);
            petResponse.As<ObjectResult>().Value.As<PetDto>().Should().BeEquivalentTo(petDto);
            petResponse.As<CreatedAtActionResult>().StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Theory(DisplayName = "Atualiza um Pet com sucesso")]
        [InlineData(1)]
        public async Task AtualizaUmPetComSucesso(int petIdEntry)
        {
            var petUpdateDto = new CreatePetDto()
            {
                Nome = "Astrolopitecus",
                Porte = "pequeno",
                Raca = "pit bull",
                TutorId = 2,
                DataNascimento = new DateTime(2012, 01, 31).Date
            };

            var repository = new Mock<IPetService>();

            repository.Setup(p => p.UpdatePet(petIdEntry, petUpdateDto)).Returns(Task.FromResult(true));

            var petController = new PetController(repository.Object);
            var petResponse = await petController.UpdatePet(petIdEntry, petUpdateDto);

            repository.Verify(p => p.UpdatePet(petIdEntry, petUpdateDto), Times.Once);
            petResponse.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Theory(DisplayName = "Deleta um Pet com sucesso")]
        [InlineData(1)]
        public async Task DeletaUmPetComSucesso(int petIdEntry)
        {
            var repository = new Mock<IPetService>();

            repository.Setup(p => p.DeletePet(petIdEntry)).Returns(Task.FromResult(true));

            var petController = new PetController(repository.Object);
            var petResponse = await petController.DeletePet(petIdEntry);

            repository.Verify(p => p.DeletePet(petIdEntry), Times.Once);
            petResponse.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

    }
}