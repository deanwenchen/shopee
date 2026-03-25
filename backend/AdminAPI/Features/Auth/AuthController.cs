using Microsoft.AspNetCore.Mvc;
using AdminAPI.Features.Auth.Services;

namespace AdminAPI.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Username, request.Password);
        if (result == null)
            return Unauthorized(new { message = "用户名或密码错误" });

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _authService.LogoutAsync(0);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);
        if (result == null)
            return Unauthorized(new { message = "Token 已失效" });

        return Ok(result);
    }
}
