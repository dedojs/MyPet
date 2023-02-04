using AutoMapper;
using MyPet.Domain.Entidades;
using MyPet.Application.Dtos.PetDtos;

namespace MyPet.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<CreatePetDto, Pet>();
            CreateMap<Pet, PetDto>();
            CreateMap<Pet, PetDtoWithTutor>();
        }

    }
}
