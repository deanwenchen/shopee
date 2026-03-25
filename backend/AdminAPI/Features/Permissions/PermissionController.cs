using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminAPI.Features.Permissions.Services;
using AdminAPI.Features.Permissions.DTOs;

namespace AdminAPI.Features.Permissions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet("tree")]
    [Authorize(Policy = "permission:list")]
    public async Task<ActionResult<PermissionTreeResponse>> GetTree()
    {
        var result = await _permissionService.GetTreeAsync();
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Policy = "permission:list")]
    public async Task<ActionResult<PermissionListResponse>> GetList(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var result = await _permissionService.GetListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "permission:detail")]
    public async Task<ActionResult<PermissionDto>> GetById(int id)
    {
        var result = await _permissionService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "permission:create")]
    public async Task<ActionResult<PermissionDto>> Create([FromBody] CreatePermissionRequest request)
    {
        var result = await _permissionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "permission:update")]
    public async Task<ActionResult<PermissionDto>> Update(int id, [FromBody] UpdatePermissionRequest request)
    {
        var result = await _permissionService.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "permission:delete")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _permissionService.DeleteAsync(id);
        if (!result) return BadRequest(new { message = "权限有子项或已被使用，无法删除" });
        return NoContent();
    }
}
