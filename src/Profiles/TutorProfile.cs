using AutoMapper;
using MyPet.Domain.Entidades;
using MyPet.Models.Dtos.PetDtos;
using MyPet.Models.Dtos.TutorDtos;

namespace MyPet.Profiles
{
    public class TutorProfile : Profile
    {
        public TutorProfile()
        {
            CreateMap<CreateTutorDto, Tutor>();
            CreateMap<Tutor, TutorDto>();
            CreateMap<Tutor, TutorDtoSimple>();
            CreateMap<Tutor, TutorLoginDto>();
        }
    }
}
