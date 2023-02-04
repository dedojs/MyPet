using MyPet.Application.Dtos.TutorDtos;


namespace MyPet.Application.Dtos.PetDtos
{
    public class PetDtoWithTutor
    {
        public int PetId { get; set; }
        public string Nome { get; set; }
        public string Porte { get; set; }
        public string Raca { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public int TutorId { get; set; }
        public TutorDtoSimple? Tutor { get; set; }
    }
}
