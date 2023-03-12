using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRegister.BusinessAccess.Interfaces;
using SchoolRegister.Models.Dto_s.UserDto_s;

namespace SchoolRegister.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
    {
        await _userService.RegisterUserAsync(registerUserDto);

        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
    {
        var token = await _userService.GenerateToken(loginUserDto);

        return Ok(token);
    }
}
