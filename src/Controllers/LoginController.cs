using Microsoft.AspNetCore.Mvc;
using MyPet.Services.Token;
using MyPet.Infra.Data.Repository.TutorRepository;
using MyPet.Application.Dtos.TutorDtos;

namespace MyPet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITutorRepository _repository;
        public LoginController(ITutorRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] TutorLoginDto tutorLogin)
        {
            var tutor = _repository.ValidadeLoginTutor(tutorLogin);

            if (tutor == null)
            {
                return BadRequest("Email ou Senha inválidos!");
            }

            var token = new TokenGenerator().Generate(tutor);

            return Ok(token);
        }


    }
       
}