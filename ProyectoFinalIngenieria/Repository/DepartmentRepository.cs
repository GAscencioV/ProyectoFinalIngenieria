using Microsoft.EntityFrameworkCore;
using ProyectoFinalIngenieria.Context;
using ProyectoFinalIngenieria.Models;

namespace ProyectoFinalIngenieria.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {

            if (Guid.TryParse(id, out Guid parsedId))
            {
                var department = await _context.Departments.FindAsync(parsedId);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(string id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id.ToString() == id);
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}
