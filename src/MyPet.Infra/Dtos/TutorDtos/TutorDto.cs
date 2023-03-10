using MyPet.Domain.Entidades;

namespace MyPet.Application.Dtos.TutorDtos
{
    public class TutorDto
    {
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public virtual ICollection<Pet>? Pets { get; set; }
        public Endereco? Endereco { get; set; }

    }
}
