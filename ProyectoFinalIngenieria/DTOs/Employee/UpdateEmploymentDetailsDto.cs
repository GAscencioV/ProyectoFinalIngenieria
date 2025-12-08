using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class UpdateEmploymentDetailsDto
    {
        [Required]
        public string Position { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        [Required]
        public string PaymentType { get; set; }
    }
}
