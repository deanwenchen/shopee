using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("admin_roles")]
public class AdminRole
{
    [Key]
    public int AdminId { get; set; }
    public int RoleId { get; set; }

    [ForeignKey("AdminId")]
    public AdminUser Admin { get; set; } = null!;

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;
}
