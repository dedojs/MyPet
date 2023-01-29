using Microsoft.AspNetCore.Mvc;
using MyPet.Models.Dtos.Pet;
using MyPet.Models.Dtos.Tutor;
using MyPet.Repository.Interfaces;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IMyPetRepository _repository;
        public PetController(IMyPetRepository repository)
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

            _repository.CreatePet(request);
            return Ok(request);
        }
    }
}