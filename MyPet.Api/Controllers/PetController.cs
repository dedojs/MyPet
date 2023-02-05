using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPet.Utils;
using MyPet.Infra.Data.Repository.PetRepository;
using MyPet.Application.Dtos.PetDtos;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _repository;
        
        public PetController(IPetRepository repository)
        {
            _repository = repository;
            
        }

        [HttpGet]
        public IActionResult GetPets(int? page, int? row, string? orderBy)
        {
            return Ok(_repository.GetPets(page, row, orderBy));   
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
        [Authorize]
        public IActionResult CreatePet([FromBody] CreatePetDto request)
        {
            if (request == null)
            {
                return BadRequest("Elemento Inválido");
            }

            var response = _repository.CreatePet(request);

            if (response == null)
            {
                return NotFound("Tutor inválido");
            }

            return CreatedAtAction(nameof(GetPet), new { id = response.PetId }, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdatePet(int id, [FromBody] CreatePetDto petDto)
        {
            var pet = _repository.GetPet(id);

            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            _repository.UpdatePet(id, petDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeletePet(int id)
        {
            var pet = _repository.GetPet(id);

            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }
            _repository.DeletePet(id);

            return NoContent();
        }

        [HttpGet("qrcode/{id}")]
        public IActionResult GenerateQrCode(int id)
        {
            var pet = _repository.GetPetWithTutor(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            var qrCode = GenerateQrCodeForData.CreateQrCode(pet);

            return File(qrCode, "image/jpeg");
        }

        [HttpGet("info/{id}")]
        public IActionResult GetInfo(int id)
        {
            var pet = _repository.GetPetWithTutor(id);
            if (pet == null)
            {
                return NotFound("Pet não localizado");
            }

            return Ok(pet);
        }


    }
}