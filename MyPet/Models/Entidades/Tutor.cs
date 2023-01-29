using System.ComponentModel.DataAnnotations;

namespace MyPet.Models.Entidades
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
        public string Cep { get; set; }
        [Required]
        [Range(6, 8, ErrorMessage = "A senha deve conter entre 6 e 8 caracteres")]
        public string Password { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }


    }
}
