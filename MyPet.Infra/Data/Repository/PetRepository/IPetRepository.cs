using MyPet.Application.Dtos.PetDtos;
using MyPet.Domain.Entidades;

namespace MyPet.Infra.Data.Repository.PetRepository;

public interface IPetRepository
{
    // C.R.U.D PET
    Task<IEnumerable<Pet>> GetPets(int? page, int? row, string? orderBy);
    Task<Pet> GetPet(int id);
    Task<Pet> CreatePet(Pet pet);
    Task UpdatePet(Pet pet);
    Task DeletePet(Pet pet);

}
