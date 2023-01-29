using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;
using MyPet.Repository.Context;
using MyPet.Repository.Interfaces;

namespace MyPet.Repository.TutorRepository
{
    public class TutorRepository : ITutorRepository
    {
        private readonly IMyPetContext _context;
        private readonly IMapper _mapper;

        public TutorRepository(IMyPetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TutorDto CreateTutor(CreateTutorDto createTutorDto)
        {
            var tutor = _mapper.Map<Tutor>(createTutorDto);
            _context.Tutores.Add(tutor);
            _context.SaveChanges();

            var tutorDto = _mapper.Map<TutorDto>(tutor);
            
            return tutorDto;
        }

        public void DeleteTutor(int id)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == id);

            _context.Tutores.Remove(tutor);
            _context.SaveChanges();
        }

        public TutorDto GetTutor(int id)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == id);

            if (tutor == null)
            {
                return null;
            }

            var tutorDto = _mapper.Map<TutorDto>(tutor);

            return tutorDto;
        }

        public List<TutorDto> GetTutores()
        {
            var listDataTutores = _context.Tutores.ToList();
            var tutoresDto = new List<TutorDto>();

            foreach (var item in listDataTutores)
            {
                var tutorDto = _mapper.Map<TutorDto>(item);
                tutoresDto.Add(tutorDto);
            }

            return tutoresDto;
        }

        public void UpdateTutor(int id, CreateTutorDto tutorDto)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == id);

            _mapper.Map(tutorDto, tutor);
            _context.SaveChanges();
        }
    }
}
