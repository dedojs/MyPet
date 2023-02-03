using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Models.Dtos.PetDtos;
using MyPet.Models.Dtos.TutorDtos;
using MyPet.Domain.Entidades;
using MyPet.Services.TutorServices;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.TutorRepository;

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
        public IActionResult GetTutores(int? page, int? row, string? orderBy)
        {
            return Ok(_repository.GetTutores(page, row, orderBy));
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
        [Authorize]
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
        [Authorize]
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