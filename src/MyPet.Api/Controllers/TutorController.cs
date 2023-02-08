using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Services.TutorServices;
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
        public async Task<IActionResult> CreateTutor([FromBody] CreateTutorDto request)
        {
            var findTutor = await _service.GetTutorByEmail(request.Email);

            if (findTutor != null)
                return BadRequest("Esse usuário já esta cadastrado");

            if (request == null)
                return BadRequest("Elemento Inválido");

            var enderecoDto = await _service.ValidateCep(request.Cep);

            if (enderecoDto == null || enderecoDto.Localidade == null)
                return NotFound("Cep Inválido");

            var endereco = await _enderecoService.CreateEndereco(enderecoDto);

            var response = await _service.CreateTutor(request);

            return CreatedAtAction(nameof(GetTutor), new { id = response.TutorId }, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] CreateTutorDto createTutorDto)
        {
            var tutor = await _service.UpdateTutor(id, createTutorDto);

            if (tutor == false)
                return NotFound("Tutor não localizado");

            var enderecoDto = await _service.ValidateCep(createTutorDto.Cep);

            if (enderecoDto == null || enderecoDto.Localidade == null)
            {
                return NotFound("Cep Inválido");
            }
            await _enderecoService.CreateEndereco(enderecoDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutorDto = await _service.DeleteTutor(id);

            if (tutorDto == false)
            {
                return NotFound("Tutor não localizado");
            }

            return NoContent();
        }

    }
       
}