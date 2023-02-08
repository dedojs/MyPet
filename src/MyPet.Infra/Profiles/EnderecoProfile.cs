using AutoMapper;
using MyPet.Application.Dtos.EnderecoDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Api.Profiles
{
    public class EnderecoProfile : Profile
    {

        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, EnderecoDto>();
        }

    }
}
