using ProyectoFinalIngenieria.DTOs.Employee;
using ProyectoFinalIngenieria.Models;
using ProyectoFinalIngenieria.Repository;

namespace ProyectoFinalIngenieria.Services
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(e => MapToDto(e));
        }

        public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null) return null;
            return MapToDto(employee);
        }

        public async Task<EmployeeResponseDto?> GetEmployeeByDniAsync(string dni)
        {
            var employee = await _repository.GetByDniAsync(dni);
            if (employee == null) return null;
            return MapToDto(employee);
        }

        public async Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
        {
            var existing = await _repository.GetByDniAsync(createDto.DNI);
            if (existing != null)
            {
                throw new InvalidOperationException($"Ya existe un empleado con el DNI {createDto.DNI}");
            }

            var newEmployee = new Employee
            {
                Name = createDto.Name,
                LastName = createDto.LastName,
                DNI = createDto.DNI,
                DepartmentId = createDto.DepartmentId,
                Email = createDto.Email,
                BirthDate = createDto.BirthDate,
                IsActive = true,
                Phone = createDto.Phone,
                EmploymentDetails = new EmploymentDetails
                {
                    Position = createDto.EmploymentDetails.Position,
                    Salary = createDto.EmploymentDetails.Salary,
                    PaymentType = createDto.EmploymentDetails.PaymentType,
                    HiringDate = DateTime.UtcNow,
                }
            };

            await _repository.AddAsync(newEmployee);

            return MapToDto(newEmployee);
        }

        public async Task UpdateEmployeeDetailsAsync(string employeeId, UpdateEmploymentDetailsDto detailsDto)
        {
            var detailsEntity = new EmploymentDetails
            {
                Position = detailsDto.Position,
                Salary = detailsDto.Salary,
                PaymentType = detailsDto.PaymentType,
            };

            await _repository.UpdateDetailsAsync(employeeId, detailsEntity);
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        private static EmployeeResponseDto MapToDto(Employee e)
        {
            return new EmployeeResponseDto
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                LastName = e.LastName,
                DNI = e.DNI,
                DepartmentId = e.DepartmentId,
                BirthDate = e.BirthDate,
                Email = e.Email,
                Phone = e.Phone,
                IsActive = e.IsActive,
                EmploymentDetails = e.EmploymentDetails == null ? null : new EmploymentDetailsResponseDto
                {
                    Position = e.EmploymentDetails.Position,
                    Salary = e.EmploymentDetails.Salary,
                    PaymentType = e.EmploymentDetails.PaymentType,
                    HiringDate = e.EmploymentDetails.HiringDate,
                    Id = e.EmploymentDetails.Id.ToString(),
                }
            };
        }
    }
}
