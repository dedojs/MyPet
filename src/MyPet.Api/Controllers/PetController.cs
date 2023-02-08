using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Utils;
using MyPet.Application.Dtos.PetDtos;
using MyPet.Services.TutorServices;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;
        
        public PetController(IPetService service)
        {
            _service = service;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetPets(int? page, int? row, string? orderBy)
        {
            var result = await _service.GetPets(page, row, orderBy);
            return Ok(result);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPet(int id)
        {
            var pet = await _service.GetPet(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            return Ok(pet);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            var response = await _service.CreatePet(request);

            if (response == null)
            {
                return NotFound("Tutor inválido");
            }

            return CreatedAtAction(nameof(GetPet), new { id = response.PetId }, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePet(int id, [FromBody] CreatePetDto createPetDto)
        {
            var petDto = await _service.UpdatePet(id, createPetDto);

            if (petDto == null)
            {
                return NotFound("Pet não localizado");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePet(int id)
        {
            var petDto = await _service.DeletePet(id);

            if (petDto == false)
            {
                return NotFound("Pet não localizado");
            }

            return NoContent();
        }

        [HttpGet("qrcode/{id}")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var pet = await _service.GetPetWithTutor(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            var qrCode = GenerateQrCodeForData.CreateQrCode(pet);

            return File(qrCode, "image/jpeg");
        }

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetInfo(int id)
        {
            var pet = await _service.GetPetWithTutor(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            return Ok(pet);
        }


    }
}