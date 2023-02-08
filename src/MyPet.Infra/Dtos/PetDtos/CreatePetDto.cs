using System.ComponentModel.DataAnnotations;

namespace MyPet.Application.Dtos.PetDtos
{
    public class CreatePetDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O tamanho do nome não pode excedder 50 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O tamanho do nome não pode excedder 20 caracteres")]
        public string Porte { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O tamanho do nome não pode excedder 20 caracteres")]
        public string Raca { get; set; }

        [Required]
        public int TutorId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
