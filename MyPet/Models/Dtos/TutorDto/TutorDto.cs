

using MyPet.Models.Entidades;

namespace MyPet.Models.Dtos.TutorDto
{
    public class TutorDto
    {
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public ICollection<Pet>? Pets { get; set; }
        
    }
}
