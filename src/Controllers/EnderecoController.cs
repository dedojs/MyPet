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
        private readonly IEnderecoRepository _repository;
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoRepository repository, IEnderecoService service)
        {
            _repository = repository;
            _service = service;

        }

        [HttpGet]
        public IActionResult GetEnderecos(int? page, int? row, string? orderBy)
        {
            return Ok(_repository.GetEnderecos(page, row, orderBy));   
        }

        [HttpGet("{cep}")]
        public IActionResult GetEnderecosByCep(string cep)
        {
            var endereco = _repository.GetEnderecosByCep(cep);
            if (endereco == null)
            {
                return NotFound("Endereço não localizado");
            }

            return Ok(endereco);
        }

        [HttpPost]
        public IActionResult CreateEndereco([FromBody] CreateEnderecoDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            var response = _repository.CreateEndereco(request);

            if (response == null)
            {
                return NotFound("Endereco inválido");
            }

            return CreatedAtAction(nameof(GetEnderecosByCep), new { cep = response.Cep }, response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteEndereco(int id)
        {
            var endereco = _repository.GetEnderecoById(id);

            if (endereco == null)
            {
                return NotFound("Endereco inválido");
            }

            _repository.DeleteEndereco(id);

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