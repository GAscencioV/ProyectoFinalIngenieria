namespace ProyectoFinalIngenieria.Models
{
    public class EmploymentDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HiringDate { get; set; }
        public string PaymentType { get; set; } = "Monthly";

        public Guid EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
