using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalIngenieria.DTOs.Department;
using ProyectoFinalIngenieria.DTOs.Employee;
using ProyectoFinalIngenieria.Services;

namespace ProyectoFinalIngenieria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>> GetAll()
        {
            var departments = await _service.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentResponseDto>> GetById(string id)
        {
            var department = await _service.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound(new { message = $"No se encontró el departamento con ID: {id}" });
            }

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<DepartmentResponseDto>> Create([FromBody] CreateDepartmentDto createDto)
        {
            try
            {
                var createdEmployee = await _service.CreateDepartmentAsync(createDto);

                return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el empleado", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDetails(string id, [FromBody] CreateDepartmentDto detailsDto)
        {
            if (detailsDto == null) return BadRequest("Datos inválidos");

            try
            {
                await _service.UpdateDepartmentAsync(id, detailsDto);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "El departamento no existe." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            // Opcional: Verificar si existe antes de intentar borrar, 
            // aunque el repositorio suele manejar esto o simplemente no hacer nada si no existe.
            var existing = await _service.GetDepartmentByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteDepartmentAsync(id);
            return NoContent();
        }


    }
}
