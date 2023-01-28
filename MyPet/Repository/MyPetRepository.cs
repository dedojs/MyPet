using MyPet.Models.Dtos.Pet;
using MyPet.Models.Dtos.Tutor;
using MyPet.Models.Entidades;
using MyPet.Repository.Interfaces;

namespace MyPet.Repository
{
    public class MyPetRepository : IMyPetRepository
    {
        private readonly IMyPetContext _context;

        public MyPetRepository(IMyPetContext context)
        {
            _context = context;
        }

        public PetDto CreatePet(PetDto petDto)
        {
            var pet = new Pet(petDto.Nome, petDto.Porte, petDto.Raca, petDto.TutorId);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            return new PetDto(pet.Nome, pet.Porte, pet.Raca, pet.TutorId);
        }

        public TutorDto CreateTutor(CreateTutorDto tutorDto)
        {
            var tutor = new Tutor(tutorDto.Nome, tutorDto.Email, tutorDto.Cep, tutorDto.Password);
            _context.Tutores.Add(tutor);
            _context.SaveChanges();

            return new TutorDto(tutor.Nome, tutor.Email, tutor.Cep);
        }

        public void DeletePet(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTutor(int id)
        {
            throw new NotImplementedException();
        }

        public Pet GetPet(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pet> GetPets()
        {
            var listPets = _context.Pets.ToList();
            return listPets;
        }

        public Tutor GetTutor(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tutor> GetTutores()
        {
            var listTutores = _context.Tutores.ToList();
            return listTutores;
        }

        public void UpdatePet(Pet pet)
        {
            throw new NotImplementedException();
        }

        public void UpdateTutor(Tutor tutor)
        {
            throw new NotImplementedException();
        }
    }
}
