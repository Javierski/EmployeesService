using Application.Interfaces;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly IAuthService _authService;

        public SecurityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Generate([FromBody] UserCredentials credentials)
        {
            try
            {
                var token = _authService.ValidateUser(credentials.Username, credentials.Role);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"El usuario no existe, {ex.Message}" });
            }
        }
    }
}