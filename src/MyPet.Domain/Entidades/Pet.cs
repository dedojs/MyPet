using MyPet.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyPet.Domain.Entidades
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Porte { get; set; }
        
        [Required]
        public string Raca { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        public int TutorId { get; set; }
        
        [JsonIgnore]
        public virtual Tutor Tutor { get; set; }
        public int HashCode { get; set; }
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                return idade;
            }
        }
        
    }
}
