using System.ComponentModel.DataAnnotations;

namespace MyPet.Models
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set;}
        public string Password { get; set; }
        public List<Pet> Pets { get; set; }


    }
}
