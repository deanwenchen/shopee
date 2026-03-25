using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminAPI.Features.Roles.Services;

namespace AdminAPI.Features.Roles;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Policy = "role:list")]
    public async Task<ActionResult<RoleListResponse>> GetList(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var result = await _roleService.GetListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "role:detail")]
    public async Task<ActionResult<RoleDto>> GetById(int id)
    {
        var result = await _roleService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "role:create")]
    public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleRequest request)
    {
        var result = await _roleService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "role:update")]
    public async Task<ActionResult<RoleDto>> Update(int id, [FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "role:delete")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _roleService.DeleteAsync(id);
        if (!result) return BadRequest(new { message = "角色已被分配，无法删除" });
        return NoContent();
    }

    [HttpPut("{id}/permissions")]
    [Authorize(Policy = "role:assign")]
    public async Task<ActionResult> AssignPermissions(int id, [FromBody] List<int> permissionIds)
    {
        var result = await _roleService.AssignPermissionsAsync(id, permissionIds);
        if (!result) return NotFound();
        return NoContent();
    }
}
