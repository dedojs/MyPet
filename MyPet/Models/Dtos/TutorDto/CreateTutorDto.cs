using System.ComponentModel.DataAnnotations;

namespace MyPet.Models.Dtos.TutorDto
{
    public class CreateTutorDto
    {
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "O Nome deve ter no mínimo 10 e no máximo 50 caracteres")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Informe apenas os 8 dígitos numéricos do CEP")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Cep deve conter apenas números")]
        public string Cep { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 8 caracteres")]
        public string Password { get; set; }
        
    }
}
