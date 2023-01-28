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

        public CreatePetDto CreatePet(CreatePetDto petDto)
        {
            var pet = new Pet(petDto.Nome, petDto.Porte, petDto.Raca, petDto.TutorId);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            return new CreatePetDto(pet.Nome, pet.Porte, pet.Raca, pet.TutorId);
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

        public List<PetDto> GetPets()
        {
            var listDataPets = _context.Pets.ToList();
            List<PetDto> pets = new ();

            foreach (var item in listDataPets)
            {
                pets.Add(new PetDto(item.PetId, item.Nome, item.Porte, item.Raca, item.TutorId));
            }

            return pets;
        }

        public Tutor GetTutor(int id)
        {
            throw new NotImplementedException();
        }

        public List<TutorDto> GetTutores()
        {
            var listTutores = _context.Tutores.ToList();
            List<TutorDto> tutores = new();

            foreach (var item in listTutores)
            {
                tutores.Add(new TutorDto(item.TutorId, item.Nome, item.Email, item.Cep));
            }

            return tutores;
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
