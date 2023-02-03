﻿using MyPet.Domain.Entidades;
using MyPet.Models.Dtos.PetDtos;
using MyPet.Models.Dtos.TutorDtos;

namespace MyPet.Infra.Data.Repository.TutorRepository
{
    public interface ITutorRepository
    {
        // C.R.U.D TUTOR
        IEnumerable<TutorDto> GetTutores(int? page, int? row, string? orderBy);
        TutorDto GetTutor(int id);
        TutorDto CreateTutor(CreateTutorDto tutorDto);
        void UpdateTutor(int id, CreateTutorDto tutorDto);
        void DeleteTutor(int id);
        Tutor ValidadeLoginTutor(TutorLoginDto tutorLogin);
    }
}