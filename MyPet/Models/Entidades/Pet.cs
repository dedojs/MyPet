using System.ComponentModel.DataAnnotations;

namespace MyPet.Models.Entidades
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Nome { get; set; }
        public string Porte { get; set; }
        public string Raça { get; set; }
        public int TutorId { get; set; }
        public int HashCode { get; set; }

    }
}
