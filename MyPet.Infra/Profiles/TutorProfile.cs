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
            CreateMap<Tutor, TutorDto>();
            CreateMap<Tutor, TutorDtoEndereco>();
            CreateMap<Tutor, TutorLoginDto>();
        }
    }
}
