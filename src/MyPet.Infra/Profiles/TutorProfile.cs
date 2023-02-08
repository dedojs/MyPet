using AutoMapper;
using MyPet.Application.Dtos.TutorDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Profiles
{
    public class TutorProfile : Profile
    {
        public TutorProfile()
        {
            CreateMap<CreateTutorDto, Tutor>();
            CreateMap<CreateTutorDto, TutorDto>();
            CreateMap<Tutor, TutorDto>();
            CreateMap<Tutor, TutorDtoEndereco>();
            CreateMap<Tutor, TutorLoginDto>();
            CreateMap<TutorLoginDto, Tutor>();
            CreateMap<Tutor, TutorDtoSimple>();
        }
    }
}
