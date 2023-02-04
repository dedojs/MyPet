using System.ComponentModel.DataAnnotations;


namespace MyPet.Domain.Entidades
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Informe apenas os 8 dígitos numéricos do CEP")]
        public string Cep { get; set; }
        [Required]
        [Range(6, 8, ErrorMessage = "A senha deve conter entre 6 e 8 caracteres")]
        public string Password { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        
    }
}
