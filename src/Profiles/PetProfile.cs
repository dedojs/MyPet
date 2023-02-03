using MyPet.Models.Dtos.PetDtos;
using AutoMapper;
using MyPet.Domain.Entidades;

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
