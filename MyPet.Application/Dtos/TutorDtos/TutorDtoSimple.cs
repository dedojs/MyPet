using MyPet.Domain.Entidades;

namespace MyPet.Application.Dtos.TutorDtos
{
    public class TutorDtoSimple
    {
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public Endereco Endereco { get; set; }

    }
}
