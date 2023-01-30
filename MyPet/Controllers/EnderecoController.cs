using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;
using MyPet.Repository.Interfaces;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _repository;
        
        public EnderecoController(IEnderecoRepository repository)
        {
            _repository = repository;
            
        }

        [HttpGet]
        public IActionResult GetEnderecos()
        {
            return Ok(_repository.GetEnderecos());   
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
        public IActionResult CreateEndereco([FromBody] Endereco request)
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


    }
}