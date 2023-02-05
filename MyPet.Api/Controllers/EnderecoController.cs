using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Services.EnderecoServices;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<IActionResult> GetEnderecos(int? page, int? row, string? orderBy)
        {
            var result = await _service.GetEnderecos(page, row, orderBy);
            return Ok(result);   
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetEnderecosByCep(string cep)
        {
            var endereco = await _service.GetEnderecosByCep(cep);
            if (endereco == null)
            {
                return NotFound("Endereço não localizado");
            }

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEndereco([FromBody] CreateEnderecoDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            var response = await _service.CreateEndereco(request);

            if (response == null)
            {
                return NotFound("Endereco inválido");
            }

            return CreatedAtAction(nameof(GetEnderecosByCep), new { cep = response.Cep }, response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _service.GetEnderecoById(id);

            if (endereco == null)
            {
                return NotFound("Endereco inválido");
            }

            await _service.DeleteEndereco(id);

            return NoContent();
        }

        [HttpGet("location/{latitude}/{longitude}")]
        public async Task<IActionResult> GetLocationByLatitudeLongitude(string latitude, string longitude)
        {
            var response = await _service.GetAdress(latitude, longitude);

            if (response == null)
            {
                return NotFound("Endereço não Localizado");
            }

            return Ok(response);

        }

    }
}