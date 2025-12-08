using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string DNI { get; set; }
        [Required]
        public string DepartmentId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}
