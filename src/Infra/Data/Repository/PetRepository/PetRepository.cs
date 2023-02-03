using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Context;
using MyPet.Models.Dtos.PetDtos;

namespace MyPet.Infra.Data.Repository.PetRepository;

public class PetRepository : IPetRepository
{
    private readonly IMyPetContext _context;
    private readonly IMapper _mapper;

    public PetRepository(IMyPetContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PetDto CreatePet(CreatePetDto createPetDto)
    {
        var tutor = _context.Tutores.FirstOrDefault(t => t.TutorId == createPetDto.TutorId);

        if (tutor == null)
        {
            return null;
        }

        var pet = _mapper.Map<Pet>(createPetDto);
        _context.Pets.Add(pet);
        _context.SaveChanges();

        var petDto = _mapper.Map<PetDto>(pet);

        return petDto;
    }

    public void DeletePet(int id)
    {
        var pet = _context.Pets.FirstOrDefault(pet => pet.PetId == id);

        _context.Pets.Remove(pet);
        _context.SaveChanges();
    }

    public PetDto GetPet(int id)
    {
        var pet = _context.Pets.FirstOrDefault(pet => pet.PetId == id);

        if (pet == null)
        {
            return null;
        }

        var petDto = _mapper.Map<PetDto>(pet);
       
        return petDto;
    }

    public PetDtoWithTutor GetPetWithTutor(int id)
    {
        var pet = _context.Pets.FirstOrDefault(pet => pet.PetId == id);

        if (pet == null)
        {
            return null;
        }

        var petDtoWithTutor = _mapper.Map<PetDtoWithTutor>(pet);

        var cep = pet.Tutor.Cep.Insert(5, "-");

        var endereco = _context.Enderecos.FirstOrDefault(t => t.Cep == cep);
        
        petDtoWithTutor.Tutor.Endereco = endereco;

        return petDtoWithTutor;
    }

    public IEnumerable<PetDto> GetPets(int? page, int? row, string? orderBy)
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

        var listPetsFilter = listDataPets.Skip((page.Value - 1) * row.Value).Take(row.Value).ToList();

        var listPetsDto = listPetsFilter.Select(p => _mapper.Map<PetDto>(p));

        return listPetsDto;
    }

    public void UpdatePet(int id, CreatePetDto petDto)
    {
        var pet = _context.Pets.FirstOrDefault(p => p.PetId == id);

        _mapper.Map(petDto, pet);
        _context.SaveChanges();
    }
}
