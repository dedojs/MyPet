using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Application.Dtos.EnderecoDtos;
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
                return NotFound("Endere�o n�o localizado");
            }

            return Ok(endereco);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEndereco([FromBody] CreateEnderecoDto request)
        {
            EnderecoDto response = new();

            if (request == null)
            {
                return BadRequest("Elemento Inv�lido");
            }

            var cep = request.Cep.Split("-");
            var cep1 = $"{cep[0]}{cep[1]}";

            var endereco = await _service.GetEnderecosByCep(cep1);

            if (endereco == null)
                response = await _service.CreateEndereco(request);

            if (endereco != null)
                return BadRequest("Endere�o j� cadastrado!");
             

            if (response == null)
            {
                return NotFound("Endereco inv�lido");
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
                return NotFound("Endereco inv�lido");
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
                return NotFound("Endere�o n�o Localizado");
            }

            return Ok(response);

        }

    }
}