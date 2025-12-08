using ProyectoFinalIngenieria.Models;

namespace ProyectoFinalIngenieria.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(string id);

        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(string id);
    }
}
