using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Services.TutorServices;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.TutorRepository;
using MyPet.Application.Dtos.TutorDtos;

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
                return NotFound("Tutor n�o localizado");
            }

            return Ok(tutor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTutores([FromBody] CreateTutorDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inv�lido");
            }
            var enderecoDto = await _service.ValidateCep(request.Cep);

            if (enderecoDto == null)
            {
                return NotFound("Cep Inv�lido");
            }

            var response = _repository.CreateTutor(request);

            var endereco = _enderecoRepository.GetEnderecosByCep(request.Cep);

            if (endereco == null)
            {
                _enderecoRepository.CreateEndereco(enderecoDto);
            }

            return CreatedAtAction(nameof(GetTutor), new { id = response.TutorId }, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] CreateTutorDto tutorDto)
        {
            var tutor = _repository.GetTutor(id);

            if (tutor == null)
            {
                return NotFound("Tutor n�o localizado");
            }

            var enderecoDto = await _service.ValidateCep(tutorDto.Cep);

            if (enderecoDto == null)
            {
                return NotFound("Cep Inv�lido");
            }

            var endereco = _enderecoRepository.GetEnderecosByCep(tutorDto.Cep);

            if (endereco == null)
            {
                _enderecoRepository.CreateEndereco(enderecoDto);
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
                return NotFound("Tutor n�o localizado");
            }
            _repository.DeleteTutor(id);

            return NoContent();
        }

    }
       
}