using ProyectoFinalIngenieria.DTOs.Employee;

namespace ProyectoFinalIngenieria.Services
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync();
        Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id);
        Task<EmployeeResponseDto?> GetEmployeeByDniAsync(string dni);
        Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
        Task UpdateEmployeeDetailsAsync(string employeeId, UpdateEmploymentDetailsDto detailsDto);
        Task DeleteEmployeeAsync(string id);
    }
}
