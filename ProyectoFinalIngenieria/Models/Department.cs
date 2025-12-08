using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ProyectoFinalIngenieria.Models
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        [JsonIgnore]
        public Collection<Employee> Employees { get; set; }
    }
}
