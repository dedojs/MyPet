using Moq;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Services.TutorServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyPet.Tests
{
    public class ValidateCepTest
    {
        [Fact]
        public async Task ValidateCep_WithValidCep_ShouldReturnCreateEnderecoDto()
        {
            // Arrange
            var expected = new CreateEnderecoDto { Cep = "12345678" };
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync("12345678/json/")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            });
            var sut = new TutorService(mockHttpClient.Object);

            // Act
            var result = await sut.ValidateCep("12345678");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ValidateCep_WithInvalidCep_ShouldReturnNull()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(x => x.GetAsync("12345678/json/")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });
            var sut = new TutorService(mockHttpClient.Object);

            // Act
            var result = await sut.ValidateCep("12345678");

            // Assert
            Assert.Null(result);
        }

    }
}
