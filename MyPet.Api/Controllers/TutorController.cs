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
        private readonly IEnderecoService _enderecoService;
        private readonly ITutorService _service;
        public TutorController(ITutorService tutorService, IEnderecoService enderecoService)
        {
            _service = tutorService;
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTutores(int? page, int? row, string? orderBy)
        {
            var result = await _service.GetTutores(page, row, orderBy);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutor(int id)
        {
            var tutor = await _service.GetTutor(id);
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

            var response = await _service.CreateTutor(request);

            var endereco = await _enderecoService.GetEnderecosByCep(request.Cep);

            if (endereco == null)
            {
                try
                {
                   await _enderecoService.CreateEndereco(enderecoDto);
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
            var tutor = await _service.GetTutor(id);

            if (tutor == null)
            {
                return NotFound("Tutor não localizado");
            }

            var enderecoDto = await _service.ValidateCep(tutorDto.Cep);

            if (enderecoDto == null || enderecoDto.Localidade == null)
            {
                return NotFound("Cep Inválido");
            }

            var endereco = _enderecoService.GetEnderecosByCep(tutorDto.Cep);

            if (endereco == null)
            {
                try
                {
                    await _enderecoService.CreateEndereco(enderecoDto);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            await _service.UpdateTutor(tutorDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutorDto = await _service.GetTutor(id);

            if (tutorDto == null)
            {
                return NotFound("Tutor não localizado");
            }
            await _service.DeleteTutor(tutorDto);

            return NoContent();
        }

    }
       
}