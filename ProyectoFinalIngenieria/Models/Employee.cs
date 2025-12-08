using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoFinalIngenieria.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; } = string.Empty;

        public required string LastName { get; set; } = string.Empty;

        public required string DNI { get; set; } = string.Empty;

        public required string Email { get; set; }

        public string? Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public virtual EmploymentDetails? EmploymentDetails { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
