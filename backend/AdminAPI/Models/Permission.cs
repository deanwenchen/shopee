using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("permissions")]
public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Code { get; set; }

    public int Type { get; set; } // 1=Directory, 2=Menu, 3=Button, 4=API

    public int? ParentId { get; set; }

    [MaxLength(255)]
    public string? Path { get; set; }

    [MaxLength(100)]
    public string? Icon { get; set; }

    [MaxLength(255)]
    public string? ApiPath { get; set; }

    public int Sort { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
