using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("role_permissions")]
public class RolePermission
{
    [Key]
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;

    [ForeignKey("PermissionId")]
    public Permission Permission { get; set; } = null!;
}
