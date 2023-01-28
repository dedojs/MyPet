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

        [HttpPost]
        public IActionResult CreatePet([FromBody] PetDto request)
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