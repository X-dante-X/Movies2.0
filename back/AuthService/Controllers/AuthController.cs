using AuthService.Models;
using AuthService.Models.DTO;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseModel>> Register(UserDTO userDto)
        {
            try
            {
                var response = await _userService.Register(userDto);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel loginDto)
        {
            try
            {
                var response = await _userService.Login(loginDto);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
