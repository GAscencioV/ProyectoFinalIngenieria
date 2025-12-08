using Microsoft.EntityFrameworkCore;
using ProyectoFinalIngenieria.Context;
using ProyectoFinalIngenieria.Models;

namespace ProyectoFinalIngenieria.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.EmploymentDetails)
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await _context.Employees
                .Include(e => e.EmploymentDetails)
                .FirstOrDefaultAsync(e => e.Id.ToString() == id);
        }

        public async Task<Employee?> GetByDniAsync(string dni)
        {
            return await _context.Employees
                .Include(e => e.EmploymentDetails)
                .FirstOrDefaultAsync(e => e.DNI == dni);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {

            if (Guid.TryParse(id, out Guid parsedId))
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateDetailsAsync(string employeeId, EmploymentDetails incomingDetails)
        {
            var existingDetails = await _context.Set<EmploymentDetails>()
                                                .FirstOrDefaultAsync(d => d.EmployeeId.ToString() == employeeId);

            if (existingDetails == null)
            {
                throw new KeyNotFoundException($"No se encontraron detalles para el empleado {employeeId}");
            }

            existingDetails.Position = incomingDetails.Position;
            existingDetails.Salary = incomingDetails.Salary;
            existingDetails.PaymentType = incomingDetails.PaymentType;

            await _context.SaveChangesAsync();
        }
    }
}
