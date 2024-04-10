using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesService.Controllers
{
    [Authorize(Policy = "CanEmployeeRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;

        public EmployeesController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Upload file is required.");

                var employees = _excelImportService.ImportEmployeesFromExcel(file.OpenReadStream());
                // Procesar los datos importados, como guardarlos en la base de datos

                return Ok(new { Count = employees.Count() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"No se cargaron correctamente los datos, {ex.Message}", Count = 0 });
            }
        }
    }
}