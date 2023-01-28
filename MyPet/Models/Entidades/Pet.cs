using System.ComponentModel.DataAnnotations;

namespace MyPet.Models.Entidades
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Nome { get; set; }
        public string Porte { get; set; }
        public string Raca { get; set; }
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int HashCode { get; set; }

        public Pet(string nome, string porte, string raca, int tutorId)
        {
            Nome= nome;
            Porte= porte;
            Raca = raca;
            TutorId = tutorId;
        }

    }
}
