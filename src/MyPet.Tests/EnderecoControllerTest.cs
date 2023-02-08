using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Controllers;
using MyPet.Domain.Entidades;
using MyPet.Services.EnderecoServices;
using Newtonsoft.Json;
using System.Net;

namespace MyPet.Tests
{
    public class EnderecoControllerTest
    {
        [Fact(DisplayName = "Retorna a lista de todos os Endereços")]
        public async Task Retorna_ComSucesso_TodosOsEnderecosAsync()
        {
            var listEnderecos = new List<EnderecoDto>()
            {
                new EnderecoDto()
                {
                    EnderecoId = 1,
                    Cep = "45028-674",
                    Logradouro = "Rua Erico Gonçalves Aguiar",
                    Complemento = "",
                    Bairro = "Candeias",
                    Localidade = "Vitória da Conquista",
                    Uf = "BA"
                },
                new EnderecoDto()
                {
                    EnderecoId = 2,
                    Cep = "45028-125",
                    Logradouro = "Rua do teste",
                    Complemento = "",
                    Bairro = "Candeias",
                    Localidade = "Vitória da Conquista",
                    Uf = "BA"
                },
            };

            // Arrange
            var repository = new Mock<IEnderecoService>();

            repository.Setup(t => t.GetEnderecos(2, 1, "name"))
                .Returns(Task.FromResult(listEnderecos.AsEnumerable()));

            var controller = new EnderecoController(repository.Object);

            // Act
            var result = await controller.GetEnderecos(2, 1, "name");

            // Assert
            repository.Verify(p => p.GetEnderecos(2, 1, "name"), Times.Once);
            result.As<ObjectResult>().Value.As<List<EnderecoDto>>().Should().BeEquivalentTo(listEnderecos);
            result.As<ObjectResult>().Value.As<List<EnderecoDto>>().Should().HaveCount(2);
            result.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Theory(DisplayName = "Retorna o endereços equivalente ao cep informado")]
        [InlineData("45028-674")]
        public async Task Retorna_ComSucesso_OEnderecoDoCep(string cep)
        {
            var endereco = new EnderecoDto()
            {
                EnderecoId = 1,
                Cep = cep,
                Logradouro = "Rua Erico Gonçalves Aguiar",
                Complemento = "",
                Bairro = "Candeias",
                Localidade = "Vitória da Conquista",
                Uf = "BA"
            };

            var repository = new Mock<IEnderecoService>();

            repository.Setup(t => t.GetEnderecosByCep(cep))
                .Returns(Task.FromResult(endereco));

            var controller = new EnderecoController(repository.Object);

            var result = await controller.GetEnderecosByCep(cep);

            repository.Verify(p => p.GetEnderecosByCep(cep), Times.Once);
            result.As<ObjectResult>().Value.As<EnderecoDto>().Should().BeEquivalentTo(endereco);
            result.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory(DisplayName = "Cria um Endereço com sucesso")]
        [InlineData(1, "45028-674", "logradouro", "complemento", "bairro", "localidade", "estado")]
        public async Task Cria_Endereco_Sucesso(int EnderecoId, string cep, string logradouro, string complemento, string bairro, string localidade, string estado)
        {
            var createEnderecoDto = new CreateEnderecoDto()
            {
                Cep = cep,
                Logradouro = logradouro,
                Complemento = complemento,
                Bairro = bairro,
                Localidade = localidade,
                Uf = estado
            };

            var enderecoDto = new EnderecoDto()
            {
                EnderecoId = EnderecoId,
                Cep = cep,
                Logradouro = logradouro,
                Complemento = complemento,
                Bairro = bairro,
                Localidade = localidade,
                Uf = estado
            };

            var repository = new Mock<IEnderecoService>();

            repository.Setup(t => t.GetEnderecosByCep(cep))
                .Returns(Task.FromResult(enderecoDto));

            repository.Setup(t => t.CreateEndereco(createEnderecoDto))
                .Returns(Task.FromResult(enderecoDto));

            var controller = new EnderecoController(repository.Object);

            var result = await controller.CreateEndereco(createEnderecoDto);

            repository.Verify(p => p.CreateEndereco(createEnderecoDto), Times.Once);
            result.As<ObjectResult>().Value.As<EnderecoDto>().Should().BeEquivalentTo(enderecoDto);
            result.As<CreatedAtActionResult>().StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Theory(DisplayName = "Deleta um Endereço com sucesso")]
        [InlineData(1)]
        public async Task Deleta_Endereco_ComSucesso(int IdEntry)
        {
            var endereco = new EnderecoDto()
            {
                EnderecoId = 1,
                Cep = "45028674",
                Logradouro = "Rua Erico Gonçalves Aguiar",
                Complemento = "",
                Bairro = "Candeias",
                Localidade = "Vitória da Conquista",
                Uf = "BA"
            };

            var repository = new Mock<IEnderecoService>();

            repository.Setup(t => t.GetEnderecoById(IdEntry))
                .Returns(Task.FromResult(endereco));

            repository.Setup(p => p.DeleteEndereco(IdEntry)).Returns(Task.FromResult(true));

            var enderecoController = new EnderecoController(repository.Object);
            var enderecoResponse = await enderecoController.DeleteEndereco(IdEntry);

            repository.Verify(p => p.DeleteEndereco(IdEntry), Times.Once);
            enderecoResponse.As<NoContentResult>().StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Theory(DisplayName = "Retorna as informações do endereço baseado em sua latitude e longitude informados no Controller")]
        [InlineData("-22.544830", "-43.134367")]
        public async Task GetLocationByLatitudeLongitude_Succes(string latitude, string longitude)
        {
            var enderecoNominatim = new Nominatim()
            {
                place_id = 1111111,
                lat = latitude,
                lon = longitude,
                display_name = "teste",
                address = "teste de endereço",
            };

            var repository = new Mock<IEnderecoService>();

            repository.Setup(t => t.GetAdress(latitude, longitude))
                .Returns(Task.FromResult(new object()));

            var enderecoController = new EnderecoController(repository.Object);
            var enderecoResponse = await enderecoController.GetLocationByLatitudeLongitude(latitude, longitude);

            repository.Verify(p => p.GetAdress(latitude, longitude), Times.Once);
            enderecoResponse.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory(DisplayName = "Retorna as informações do endereço baseado em sua latitude e longitude")]
        [InlineData("-22.544830", "-43.134367")]
        public async Task Retorna_Informacoes_Do_Endereco(string latitude, string longitude)
        {
            var url = $"https://localhost:7101/endereco/location/{latitude}/{longitude}";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var jsonData = "{\r\n\t\"place_id\": 342345036,\r\n\t\"lat\": \"-22.5538559\",\r\n\t\"lon\": \"-43.0497497\",\r\n\t\"display_name\": \"Santo Aleixo, Magé, Região Geográfica Imediata do Rio de Janeiro, Região Metropolitana do Rio de Janeiro, Região Geográfica Intermediária do Rio de Janeiro, Rio de Janeiro, Região Sudeste, 25912-296, Brasil\",\r\n\t\"address\": {\r\n\t\t\"village\": \"Santo Aleixo\",\r\n\t\t\"city\": \"Magé\",\r\n\t\t\"municipality\": \"Região Geográfica Imediata do Rio de Janeiro\",\r\n\t\t\"county\": \"Região Metropolitana do Rio de Janeiro\",\r\n\t\t\"state_district\": \"Região Geográfica Intermediária do Rio de Janeiro\",\r\n\t\t\"state\": \"Rio de Janeiro\",\r\n\t\t\"ISO3166-2-lvl4\": \"BR-RJ\",\r\n\t\t\"region\": \"Região Sudeste\",\r\n\t\t\"postcode\": \"25912-296\",\r\n\t\t\"country\": \"Brasil\",\r\n\t\t\"country_code\": \"br\"\r\n\t},\r\n\t\"boundingbox\": [\r\n\t\t\"-22.6110895\",\r\n\t\t\"-22.4840381\",\r\n\t\t\"-43.1777958\",\r\n\t\t\"-43.013678\"\r\n\t]\r\n}";
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = content,
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var response = await httpClient.GetAsync(url);
            var data = JsonConvert.DeserializeObject<Nominatim>(await response.Content.ReadAsStringAsync());

            data.lat.Should().Contain(latitude.Substring(0,3));
            data.lon.Should().Contain(longitude.Substring(0, 3));
            data.display_name.Should().Contain("Brasil");
        }



    }
}