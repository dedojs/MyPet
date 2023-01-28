namespace MyPet.Models.Dtos.Tutor
{
    public class CreateTutorDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Password { get; set; }

        public CreateTutorDto(string nome, string email, string cep, string password)
        {
            Nome = nome;
            Email = email;
            Cep = cep;
            Password = password;
        }
    }
}
