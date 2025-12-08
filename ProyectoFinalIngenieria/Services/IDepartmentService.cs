using ProyectoFinalIngenieria.DTOs.Department;
using ProyectoFinalIngenieria.DTOs.Employee;

namespace ProyectoFinalIngenieria.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync();
        Task<DepartmentResponseDto?> GetDepartmentByIdAsync(string id);
        Task<DepartmentResponseDto> CreateDepartmentAsync(CreateDepartmentDto createDto);
        Task UpdateDepartmentAsync(string departmentId, CreateDepartmentDto updateDto);
        Task DeleteDepartmentAsync(string id);
    }
}
