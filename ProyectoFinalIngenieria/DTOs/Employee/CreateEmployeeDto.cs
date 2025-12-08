using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(20, ErrorMessage = "El DNI no puede exceder 20 caracteres")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "El ID del departamento es obligatorio")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        public string? Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        // Al crear un empleado, OBLIGAMOS a enviar sus detalles laborales iniciales
        [Required]
        public CreateEmploymentDetailsDto EmploymentDetails { get; set; }

    }
}

