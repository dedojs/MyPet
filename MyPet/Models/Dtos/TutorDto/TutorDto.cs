using MyPet.Models.Dtos.Pet;
using MyPet.Models.Entidades;


namespace MyPet.Models.Dtos.Tutor
{
    public class TutorDto
    {
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }

        public TutorDto(string nome, string email, string cep)
        {
            Nome = nome;
            Email = email;
            Cep = cep;
        }

        public TutorDto(int id, string nome, string email, string cep)
        {
            TutorId = id;   
            Nome = nome;
            Email = email;
            Cep = cep;
        }
    }
}
