using System.ComponentModel.DataAnnotations;

namespace MyPet.Application.Dtos.TutorDtos
{
    public class TutorLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 8 caracteres")]
        public string Password { get; set; }
    }
}
