using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminAPI.Features.Admins.Services;

namespace AdminAPI.Features.Admins;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    [Authorize(Policy = "admin:list")]
    public async Task<ActionResult<AdminListResponse>> GetList(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var result = await _adminService.GetListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "admin:detail")]
    public async Task<ActionResult<AdminDto>> GetById(int id)
    {
        var result = await _adminService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "admin:create")]
    public async Task<ActionResult<AdminDto>> Create([FromBody] CreateAdminRequest request)
    {
        var result = await _adminService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "admin:update")]
    public async Task<ActionResult<AdminDto>> Update(int id, [FromBody] UpdateAdminRequest request)
    {
        var result = await _adminService.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "admin:delete")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _adminService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}/status")]
    [Authorize(Policy = "admin:status")]
    public async Task<ActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
    {
        var result = await _adminService.UpdateStatusAsync(id, request.Status);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("{targetAdminId}/reset-password")]
    [Authorize(Policy = "admin:reset-pwd")]
    public async Task<ActionResult> ResetPassword(int targetAdminId, [FromBody] ResetPasswordRequest request)
    {
        var adminId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
        var result = await _adminService.ResetPasswordAsync(adminId, targetAdminId, request.NewPassword);
        if (!result) return NotFound();
        return NoContent();
    }
}
