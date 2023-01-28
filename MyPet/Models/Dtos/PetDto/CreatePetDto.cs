﻿namespace MyPet.Models.Dtos.Pet
{
    public class CreatePetDto
    {
        public CreatePetDto(string nome, string porte, string raca, int tutorId)
        {
            Nome = nome;
            Porte = porte;
            Raca = raca;
            TutorId = tutorId;
        }

        
        public int PetId { get; set; }
        public string Nome { get; set; }
        public string Porte { get; set; }
        public string Raca { get; set; }
        public int TutorId { get; set; }
    }
}
