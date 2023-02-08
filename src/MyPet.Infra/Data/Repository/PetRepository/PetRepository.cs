using Microsoft.EntityFrameworkCore;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Context;

namespace MyPet.Infra.Data.Repository.PetRepository;

public class PetRepository : IPetRepository
{
    private readonly MyPetContext _context;

    public PetRepository(MyPetContext context)
    {
        _context = context;
    }

    public async Task<Pet> CreatePet(Pet pet)
    {
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        return pet;
    }

    public async Task DeletePet(Pet pet)
    {
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }

    public async Task<Pet> GetPet(int id)
    {
        var pet = await _context.Pets.FirstOrDefaultAsync(pet => pet.PetId == id);

        if (pet == null)
        {
            return null;
        }

        return pet;
    }

    public async Task<IEnumerable<Pet>> GetPets(int? page, int? row, string? orderBy)
    {
        if (page == null)
            page = 1;
        if (row == null)
            row = 20;
        if (string.IsNullOrEmpty(orderBy))
            orderBy = "id";

        var listDataPets = _context.Pets.OrderBy(p => p.PetId);

        if (orderBy == "name")
            listDataPets = listDataPets.OrderBy(p => p.Nome);

        var listPetsFilter = listDataPets.Skip((page.Value - 1) * row.Value).Take(row.Value);

        return await listPetsFilter.ToListAsync();
    }

    public async Task UpdatePet(Pet pet)
    {
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    }
}
