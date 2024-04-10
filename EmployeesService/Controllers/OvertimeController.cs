using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeesService.Controllers
{
    [Authorize(Policy = "CanOvertimeRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : ControllerBase
    {
        private readonly IOvertimeService _overTimeService;

        public OvertimeController(IOvertimeService overTimeService)
        {
            _overTimeService = overTimeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var overtime = await _overTimeService.GetAllOvertimeAsync();
            return Ok(overtime);
        }

        [HttpPost("ReportTime")]
        public async Task<IActionResult> Post([FromBody] Overtime overTimeDto)
        {
            var overtime = await _overTimeService.AddOvertimeAsync(overTimeDto);
            return CreatedAtAction(nameof(Get), new { id = overtime.Id }, overtime);
        }

        [HttpPut("Authorize/{id}")]
        public async Task<IActionResult> Aprobar(int id)
        {
            var approverId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _overTimeService.ApproveAsync(id, int.Parse(approverId));
            return NoContent();
        }

        [Authorize(Policy = "CanApproveOvertime")]
        [HttpPut("Confirm/{id}")]
        public async Task<IActionResult> Confirmar(int id)
        {
            await _overTimeService.ConfirmAsync(id);
            return NoContent();
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> Rechazar(int id)
        {
            await _overTimeService.RejectAsync(id);
            return NoContent();
        }
    }
}