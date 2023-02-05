using System.ComponentModel.DataAnnotations;

namespace MyPet.Domain.Entidades
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        
    }
}
