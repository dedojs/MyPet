using MyPet.Domain.Entidades;
using MyPet.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MyPet.Infra.Data.Repository.TutorRepository
{
    public class TutorRepository : ITutorRepository
    {
        private readonly MyPetContext _context;

        public TutorRepository(MyPetContext context)
        {
            _context = context;
        }

        public async Task<Tutor> CreateTutor(Tutor tutor)
        {
            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();
            
            return tutor;
        }

        public async Task DeleteTutor(Tutor tutor)
        {
            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task<Tutor> GetTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);

            if (tutor == null)
            {
                return null;
            }

            return tutor;
        }

        public async Task<Tutor> GetTutorByEmail(string email)
        {
            var tutor = await _context.Tutores.FirstOrDefaultAsync(t => t.Email == email);
            if (tutor == null)
            {
                return null;
            }

            return tutor;
        }

        public async Task<IEnumerable<Tutor>> GetTutores(int? page, int? row, string? orderBy)
        {
            if (page == null)
                page = 1;
            if (row == null)
                row = 20;
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "id";

            var listDataTutores = _context.Tutores.OrderBy(t => t.TutorId);

            if (orderBy == "name")
                listDataTutores = _context.Tutores.OrderBy(t => t.Nome);

            var listTutorsFilter = listDataTutores.Skip((page.Value - 1) * row.Value).Take(row.Value);

            return await listTutorsFilter.ToListAsync();
        }

        public async Task UpdateTutor(Tutor tutor)
        {
            _context.Tutores.Update(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task<Tutor> ValidadeLoginTutor(Tutor tutorLogin)
        {
            var tutor = await _context.Tutores.FirstOrDefaultAsync(t => t.Email == tutorLogin.Email && t.Password == tutorLogin.Password);

            if (tutor == null)
                return null;

            return tutor;
        }
    }
}
