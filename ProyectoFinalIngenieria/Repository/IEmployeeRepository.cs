using ProyectoFinalIngenieria.Models;

namespace ProyectoFinalIngenieria.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(string id);
        Task<Employee?> GetByDniAsync(string dni);

        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(string id);
        Task UpdateDetailsAsync(string employeeId, EmploymentDetails details);
    }
}
