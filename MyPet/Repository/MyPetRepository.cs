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

        public Pet CreatePet(Pet pet)
        {
            throw new NotImplementedException();
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

        public Pet GetPets()
        {
            throw new NotImplementedException();
        }

        public Tutor GetTutor(int id)
        {
            throw new NotImplementedException();
        }

        public Tutor GetTutorores()
        {
            throw new NotImplementedException();
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
