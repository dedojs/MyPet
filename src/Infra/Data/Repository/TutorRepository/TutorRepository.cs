using AutoMapper;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Context;
using MyPet.Infra.Data.Repository.TutorRepository;
using MyPet.Models.Dtos.PetDtos;
using MyPet.Models.Dtos.TutorDtos;


namespace MyPet.Infra.Data.Repository.TutorRepository
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

            var cep = tutor.Cep.Insert(5, "-");
            var endereco = _context.Enderecos.FirstOrDefault(e => e.Cep == cep);
            
            var tutorDto = _mapper.Map<TutorDto>(tutor);

            tutorDto.Endereco = endereco;

            return tutorDto;
        }

        public IEnumerable<TutorDto> GetTutores(int? page, int? row, string? orderBy)
        {
            if (page == null)
                page = 1;
            if (row == null)
                row = 20;
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "id";

            var listDataTutores = _context.Tutores.OrderBy(t => t.TutorId);

            if (orderBy == "name")
                listDataTutores = _context.Tutores.OrderBy(t => t.Nome);

            var listTutorsFilter = listDataTutores.Skip((page.Value - 1) * row.Value).Take(row.Value).ToList();

            var listTutoesDto = listTutorsFilter.Select(t => _mapper.Map<TutorDto>(t));

            return listTutoesDto;
        }

        public void UpdateTutor(int id, CreateTutorDto tutorDto)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == id);

            _mapper.Map(tutorDto, tutor);
            _context.SaveChanges();
        }

        public Tutor ValidadeLoginTutor(TutorLoginDto tutorLogin)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.Email == tutorLogin.Email && t.Password == tutorLogin.Password);

            if (tutor == null) return null;

            return tutor;
        }
    }
}
