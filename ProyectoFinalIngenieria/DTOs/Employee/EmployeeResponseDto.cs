using ProyectoFinalIngenieria.DTOs.Department;

namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class EmployeeResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        public EmploymentDetailsResponseDto? EmploymentDetails { get; set; }
        public string DepartmentId { get; set; }

        public DepartmentResponseDto? Department { get; set; }
    }
}
