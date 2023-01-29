using AutoMapper;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;
using MyPet.Repository.Context;
using MyPet.Repository.Interfaces;

namespace MyPet.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly IMyPetContext _context;
        private readonly IMapper _mapper;

        public PetRepository(IMyPetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PetDto CreatePet(CreatePetDto createPetDto)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == createPetDto.TutorId);

            if (tutor == null)
            {
                return null;
            }

            var pet = _mapper.Map<Pet>(createPetDto);
            _context.Pets.Add(pet);
            _context.SaveChanges();

            var petDto = _mapper.Map<PetDto>(pet);

            return petDto;
        }

        public void DeletePet(int id)
        {
            var pet = _context.Pets.FirstOrDefault(pet => pet.PetId == id);

            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        public PetDto GetPet(int id)
        {
            var pet = _context.Pets.FirstOrDefault(pet => pet.PetId == id);

            if (pet == null)
            {
                return null;
            }

            var petDto = _mapper.Map<PetDto>(pet);
           
            return petDto;
        }

        public List<PetDto> GetPets()
        {
            var listDataPets = _context.Pets.ToList();
            var petsDto = new List<PetDto>();

            foreach (var item in listDataPets)
            {
                var petDto = _mapper.Map<PetDto>(item);
                petsDto.Add(petDto);
            }

            return petsDto;
        }

        public void UpdatePet(int id, CreatePetDto petDto)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.PetId == id);

            _mapper.Map(petDto, pet);
            _context.SaveChanges();
        }
    }
}
