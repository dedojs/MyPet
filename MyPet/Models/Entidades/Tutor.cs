using System.ComponentModel.DataAnnotations;

namespace MyPet.Models.Entidades
{
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Password { get; set; }
        public List<Pet> Pets { get; set; }

        public Tutor(string nome, string email, string cep, string password)
        {
            Nome = nome;
            Email = email;
            Cep = cep;
            Password = password;
            Pets = new List<Pet>();
        }


    }
}
