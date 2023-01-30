using Microsoft.AspNetCore.Mvc;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;
using MyPet.Repository.Interfaces;
using MyPet.Services.TutorServices;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorRepository _repository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ITutorService _service;
        public TutorController(ITutorRepository repository, ITutorService tutorService, IEnderecoRepository enderecoRepository)
        {
            _service = tutorService;
            _enderecoRepository = enderecoRepository;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetTutores()
        {
            return Ok(_repository.GetTutores());
        }

        [HttpGet("{id}")]
        public IActionResult GetTutor(int id)
        {
            var tutor = _repository.GetTutor(id);
            if (tutor == null)
            {
                return NotFound("Tutor não localizado");
            }

            return Ok(tutor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTutores([FromBody] CreateTutorDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }
            var cep = await _service.ValidateCep(request.Cep);
            if (cep == null)
            {
                return NotFound("Cep Inválido");
            }

            var response = _repository.CreateTutor(request);

            var endereco = _enderecoRepository.GetEnderecosByCep(request.Cep);

            if (endereco == null)
            {
                _enderecoRepository.CreateEndereco(cep);
            }

            return CreatedAtAction(nameof(GetTutor), new { id = response.TutorId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTutor(int id, [FromBody] CreateTutorDto tutorDto)
        {
            var tutor = _repository.GetTutor(id);

            if (tutor == null)
            {
                return NotFound("Tutor não localizado");
            }

            _repository.UpdateTutor(id, tutorDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTutor(int id)
        {
            var tutor = _repository.GetTutor(id);

            if (tutor == null)
            {
                return NotFound("Tutor não localizado");
            }
            _repository.DeleteTutor(id);

            return NoContent();
        }

    }
       
}