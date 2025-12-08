using ProyectoFinalIngenieria.DTOs.Department;
using ProyectoFinalIngenieria.DTOs.Employee;
using ProyectoFinalIngenieria.Models;
using ProyectoFinalIngenieria.Repository;

namespace ProyectoFinalIngenieria.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<DepartmentResponseDto> CreateDepartmentAsync(CreateDepartmentDto createDto)
        {

            var newDepartmen = new Department
            {
                Name = createDto.Name,
            };

            await _repository.AddAsync(newDepartmen);

            return MapToDto(newDepartmen);
        }

        public async Task DeleteDepartmentAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            var departments = await _repository.GetAllAsync();

            return departments.Select(e => MapToDto(e));
        }

        public async Task<DepartmentResponseDto?> GetDepartmentByIdAsync(string id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null) return null;
            return MapToDto(department);
        }

        public async Task UpdateDepartmentAsync(string departmentId, CreateDepartmentDto updateDto)
        {
            var entityDetails = await _repository.GetByIdAsync(departmentId);

            if (entityDetails == null) throw new Exception($"No se encontró el departamento con ID: {departmentId}.");

            entityDetails.Name = updateDto.Name;

            await _repository.UpdateAsync(entityDetails);
        }

        private static DepartmentResponseDto MapToDto(Department e)
        {
            return new DepartmentResponseDto
            {
                Id = e.Id.ToString(),
                Name = e.Name,
            };
        }

    }
}
