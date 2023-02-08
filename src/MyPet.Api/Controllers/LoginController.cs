using Microsoft.AspNetCore.Mvc;
using MyPet.Services.Token;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Services.TutorServices;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITutorService _service;
        public LoginController(ITutorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] TutorLoginDto tutorLogin)
        {
            var tutor = await _service.ValidadeLoginTutor(tutorLogin);

            if (tutor == null)
            {
                return BadRequest("Email ou Senha inválidos!");
            }

            var token = new TokenGenerator().Generate(tutor);

            return Ok(token);
        }


    }
       
}