using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalIngenieria.DTOs.Employee;
using ProyectoFinalIngenieria.Helpers;
using ProyectoFinalIngenieria.Models.Enums;
using ProyectoFinalIngenieria.Services;

namespace ProyectoFinalIngenieria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _service;

        public EmployeeController(IEmployeeServices service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetAll()
        {
            var employees = await _service.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeResponseDto>> GetById(string id)
        {
            var employee = await _service.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new { message = $"No se encontró el empleado con ID: {id}" });
            }

            return Ok(employee);
        }

        // GET: api/employees/search/{dni}
        // Usamos una sub-ruta para diferenciarlo de la búsqueda por ID
        [HttpGet("search/{dni}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeResponseDto>> GetByDni(string dni)
        {
            var employee = await _service.GetEmployeeByDniAsync(dni);

            if (employee == null)
            {
                return NotFound(new { message = $"No se encontró el empleado con DNI: {dni}" });
            }

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<EmployeeResponseDto>> Create([FromBody] CreateEmployeeDto createDto)
        {
            try
            {
                var createdEmployee = await _service.CreateEmployeeAsync(createDto);

                // Retorna 201 Created y agrega el header 'Location' apuntando al endpoint GetById
                return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (InvalidOperationException ex)
            {
                // Capturamos la excepción de DNI duplicado lanzada por el servicio
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el empleado", details = ex.Message });
            }
        }

        // PUT: api/employees/{id}/details
        // Es un recurso anidado, ideal para actualizar solo una parte del empleado
        [HttpPut("{id}/details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDetails(string id, [FromBody] UpdateEmploymentDetailsDto detailsDto)
        {
            if (detailsDto == null) return BadRequest("Datos inválidos");

            try
            {
                await _service.UpdateEmployeeDetailsAsync(id, detailsDto);

                // 204 No Content es el estándar para actualizaciones exitosas que no devuelven datos
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "El empleado no existe o no tiene detalles asociados." });
            }
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            // Opcional: Verificar si existe antes de intentar borrar, 
            // aunque el repositorio suele manejar esto o simplemente no hacer nada si no existe.
            var existing = await _service.GetEmployeeByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("payment-types")]
        public IActionResult GetPaymentTypes()
        {
            var values = Enum.GetValues(typeof(PaymentType))
                             .Cast<PaymentType>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = EnumHelper.GetDescription(e) // Usa "Mensual" en vez de "Monthly"
                             })
                             .ToList();

            return Ok(values);
        }
    }
}