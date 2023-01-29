using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Entidades;
using AutoMapper;

namespace MyPet.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<CreatePetDto, Pet>();
            CreateMap<Pet, PetDto>();
        }

    }
}
