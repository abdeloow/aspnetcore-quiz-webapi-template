using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QuizApi;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;
    public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }
    [HttpPost("register")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
    {
        try
        {
            var result = await _authService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("exists"))
            {
                return Conflict(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginModelDto model)
    {
        try
        {
            var token = await _authService.LoginUserAsync(model);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutUserAsync();
        return NoContent();
    }
}
