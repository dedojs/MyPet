using System.ComponentModel.DataAnnotations;

namespace MyPet.Application.Dtos.EnderecoDtos
{
    public class CreateEnderecoDto
    {
        [Required]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Cep deve obedecer esse formato: 45253-55")]
        public string Cep { get; set; }
        [Required]
        public string Logradouro { get; set; }
        
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Localidade { get; set; }
        [Required]
        public string Uf { get; set; }
    }
}
