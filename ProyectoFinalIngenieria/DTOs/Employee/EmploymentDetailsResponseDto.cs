namespace ProyectoFinalIngenieria.DTOs.Employee
{
    public class EmploymentDetailsResponseDto
    {
        public string Id { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HiringDate { get; set; }
        public string PaymentType { get; set; }
        public string EmployeeId { get; set; }
    }
}
