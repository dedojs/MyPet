using AutoMapper;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Dtos.TutorDto;
using MyPet.Models.Entidades;

namespace MyPet.Profiles
{
    public class TutorProfile : Profile
    {
        public TutorProfile()
        {
            CreateMap<CreateTutorDto, Tutor>();
            CreateMap<Tutor, TutorDto>();
        }
    }
}
