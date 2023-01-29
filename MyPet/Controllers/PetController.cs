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
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _repository;
        
        public PetController(IPetRepository repository)
        {
            _repository = repository;
            
        }

        [HttpGet]
        public IActionResult GetPets()
        {
            return Ok(_repository.GetPets());   
        }

        [HttpGet("{id}")]
        public IActionResult GetPet(int id)
        {
            var pet = _repository.GetPet(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult CreatePet([FromBody] CreatePetDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            var response = _repository.CreatePet(request);

            if (response == null)
            {
                return NotFound("Tutor inválido");
            }

            return CreatedAtAction(nameof(GetPet), new { id = response.PetId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePet(int id, [FromBody] CreatePetDto petDto)
        {
            var pet = _repository.GetPet(id);

            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            _repository.UpdatePet(id, petDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id)
        {
            var pet = _repository.GetPet(id);

            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }
            _repository.DeletePet(id);

            return NoContent();
        }
    }
}