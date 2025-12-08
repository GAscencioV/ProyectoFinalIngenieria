using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class CreateEmploymentDetailsDto
    {
        [Required]
        public string Position { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser mayor o igual a 0")]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        public string PaymentType { get; set; } = "Monthly";
    }
}
