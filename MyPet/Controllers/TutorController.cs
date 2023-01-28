using Microsoft.AspNetCore.Mvc;
using MyPet.Models.Dtos.Pet;
using MyPet.Models.Dtos.Tutor;
using MyPet.Repository.Interfaces;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly IMyPetRepository _repository;
        public TutorController(IMyPetRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetTutores()
        {
            return Ok(_repository.GetTutores());
        }

        [HttpPost]
        public IActionResult CreateTutores([FromBody] CreateTutorDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            _repository.CreateTutor(request);
            return Ok(request);
        }
    }
       
}