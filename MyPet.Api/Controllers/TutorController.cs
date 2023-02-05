using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Services.TutorServices;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.TutorRepository;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Services.EnderecoServices;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorRepository _repository;
        private readonly IEnderecoService _enderecoService;
        private readonly ITutorService _service;
        public TutorController(ITutorRepository repository, ITutorService tutorService, IEnderecoService enderecoService)
        {
            _service = tutorService;
            _enderecoService = enderecoService;
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
            var enderecoDto = await _service.ValidateCep(request.Cep);

            if (enderecoDto == null || enderecoDto.Localidade == null)
            {
                return NotFound("Cep Inválido");
            }

            var response = _repository.CreateTutor(request);

            var endereco = _enderecoService.GetEnderecosByCep(request.Cep);

            if (endereco == null)
            {
                try
                {
                    _enderecoService.CreateEndereco(enderecoDto);
                }
                catch(ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                
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
                return NotFound("Tutor não localizado");
            }

            var enderecoDto = await _service.ValidateCep(tutorDto.Cep);

            if (enderecoDto == null)
            {
                return NotFound("Cep Inválido");
            }

            var endereco = _enderecoService.GetEnderecosByCep(tutorDto.Cep);

            if (endereco == null)
            {
                _enderecoService.CreateEndereco(enderecoDto);
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